using LibraryWebApplication.Models;
using LibraryWebApplication.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController(UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateUserDto request, string password, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Name = request.Name,
                Lastname = request.Lastname,
                Email = request.Email,
                
            };

            IdentityResult result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }

        [HttpGet]
     //   [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var users = await userManager.Users.ToListAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("/{id}")]
     //   [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, UpdateUserDto request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            user.UserName = request.UserName;
            user.Name = request.Name;
            user.Lastname = request.Lastname;
            user.Email = request.Email;

            IdentityResult result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            IdentityResult result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }
    }
}
