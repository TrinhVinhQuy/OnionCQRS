﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnionCQRS.Application.Abstracts;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Entities;
using OnionCQRS.Domain.Model;
using OnionCQRS.WebAPI.ViewModel;

namespace OnionCQRS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ITokenHandler _tokenHandler;
        private IUserService _userService;

        public AuthenticationController(ITokenHandler tokenHandler, IUserService userService)
        {
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountModel accountModel)
        {
            var user = await _userService.CheckLogin(accountModel.Username, accountModel.Password);

            if(user == null)
            {
                return BadRequest("The username or password is incorrect.");
            }

            if (!user.EmailConfirmed)
            {
                return BadRequest("Your account is inactive.");
            }


            string accessToken = await _tokenHandler.CreateAccessToken(user);
            string refreshToken = await _tokenHandler.CreateRefreshToken(user);

            return Ok(new JwtModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel token)
        {
            if (token == null)
                return BadRequest("Could not get refresh token");

            return Ok(await _tokenHandler.ValidateRefreshToken(token.RefreshToken));
        }
    }
}
