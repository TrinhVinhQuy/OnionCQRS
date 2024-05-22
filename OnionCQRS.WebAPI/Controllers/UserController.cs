using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Entities;
using OnionCQRS.Domain.Model;
using OnionCQRS.Domain.Setting;
using OnionCQRS.WebAPI.ViewModel;

namespace OnionCQRS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public UserController(UserManager<ApplicationUser> userManager,
            PasswordHasher<ApplicationUser> passwordHasher,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }
        [HttpPost]
        public async Task<IActionResult> Register(CancellationToken cancellationToken, [FromBody] UserModel userVM)
        {
            if (userVM is null)
            {
                return BadRequest("Invalid Data");
            }
            var user = new ApplicationUser
            {
                Address = userVM.Address,
                Fullname = userVM.Fullname,
                UserName = userVM.Username,
                PhoneNumber = userVM.PhoneNumber,
                Email = userVM.Email,
                PasswordHash = userVM.Password,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                //Send Email for Confirm
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string url = Url.Action("ConfirmEmail", "User", new { memberKey = user.Id, tokenReset = token }, Request.Scheme);

                string body = await _emailService.GetTemplate("Templates\\ConfirmEmail.html");
                body = string.Format(body, user.Fullname, url);

                await _emailService.SendEmailAsync(cancellationToken, new EmailRequest
                {
                    To = user.Email,
                    Subject = "Confirm Email For Register",
                    Content = body
                });

                return Ok(true);
            }
            else
                return BadRequest(result.Errors);
        }
    }
}
