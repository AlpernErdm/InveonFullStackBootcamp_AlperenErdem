using LibraryWebApplication.Context;
using LibraryWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class UserRolesController(AppDbContext context, UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Create(Guid userId, string roleName, Guid roleId, CancellationToken cancellationToken)
        {
            //AppUserRole appUserRole = new()
            //{
            //    UserId = userId,
            //    RoleId = roleId,
            //};

            //await context.UserRoles.AddAsync(appUserRole);

            //await context.SaveChangesAsync(cancellationToken);

            AppUser? appUser = await userManager.FindByIdAsync(userId.ToString());
            if (appUser is null)
            {
                return BadRequest(new { Message = "Kullanıcı bulunamadı" });
            }

            IdentityResult result = await userManager.AddToRoleAsync(appUser, roleName);


            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }
            return NoContent();
        }
    }
}
