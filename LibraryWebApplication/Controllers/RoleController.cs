using LibraryWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController(RoleManager<AppRole> roleManager,UserManager<AppUser>userManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Create(string name, CancellationToken cancellationToken)
        {
            AppRole appRole = new()
            {
                Name = name,
            };

            IdentityResult result = await roleManager.CreateAsync(appRole);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {

            var roles = await roleManager.Roles.ToListAsync(cancellationToken);

            return Ok(roles);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, string newName, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            role.Name = newName;
            IdentityResult result = await roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            IdentityResult result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleName, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            IdentityResult result = await userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            IdentityResult result = await userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }
    }
}
