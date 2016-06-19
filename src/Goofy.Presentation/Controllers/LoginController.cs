using Goofy.Presentation.Configuration.Models;
using Goofy.Security.Services.Abstractions;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Goofy.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly TokenProviderOptions _options;
        private readonly IUserClaimProvider _userClaimsProvider;

        public LoginController(IUserClaimProvider userClaimsProvider, IOptions<TokenProviderOptions> tokenProviderOptions)
        {
            _userClaimsProvider = userClaimsProvider;
            _options = tokenProviderOptions.Value;
        }

        // GET: /<controller>/
        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var userClaims = await _userClaimsProvider.GetUserClaims(loginModel.Username, loginModel.Password);
                if (userClaims == null)
                {
                    return new BadRequestResult();
                }

                var now = DateTime.UtcNow;

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, loginModel.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, await _options.NonceGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64),
                };
                claims.AddRange(userClaims);

                var identity = new ClaimsIdentity(claims, "");
                var handler = new JwtSecurityTokenHandler();
                var rsa = new RSACryptoServiceProvider(2048);
                var rsaParams = rsa.ExportParameters(true);
                var rsaKey = new RsaSecurityKey(rsaParams);

                var secToken = handler.CreateToken(_options.Issuer, _options.Audience, identity, now, DateTime.UtcNow.AddDays(1), now,
                    new SigningCredentials(rsaKey, JwtAlgorithms.RSA_SHA256));

                var encodedJwt = handler.WriteToken(secToken);

                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)_options.Expiration.TotalSeconds
                };

                return new ObjectResult(response);
            }

            return new BadRequestResult();
        }

        /// <summary>
        /// Get this datetime as a Unix epoch timestamp (seconds since Jan 1, 1970, midnight UTC).
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>Seconds since Unix epoch.</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
