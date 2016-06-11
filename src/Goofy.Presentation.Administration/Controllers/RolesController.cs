using Goofy.Domain.Administration.Entity;
using Goofy.Presentation.Administration.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<RoleViewModel> Get()
        {
            return _roleManager.Roles.Select(r => new RoleViewModel { Name = r.Name, Description = r.Description });
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var newRole = new GoofyRole
                {
                    Name = role.Name,
                    Description = role.Description
                };
                var result = await _roleManager.CreateAsync(newRole);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return new ObjectResult(result.Errors);
            }
            return HttpBadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]GoofyRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return new ObjectResult(result.Errors);
            }
            return HttpBadRequest();
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
