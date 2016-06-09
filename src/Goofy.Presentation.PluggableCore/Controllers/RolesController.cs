using Goofy.Security.UserModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Route("administration/roles")]
    public class RolesController : Controller
    {
        private readonly RoleManager<GoofyRole> _roleManager;

        public RolesController(RoleManager<GoofyRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<GoofyRole> Get()
        {
            return _roleManager.Roles;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GoofyRole value)
        {
            var result = await _roleManager.CreateAsync(value);
            if (result.Succeeded)
            {
                return Ok();
            }
            return new ObjectResult(result.Errors);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]GoofyRole role)
        {
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return new ObjectResult(result.Errors);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return Ok();
            return new ObjectResult(result.Errors);
        }
    }
}
