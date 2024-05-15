using Microsoft.AspNetCore.Authentication.JwtBearer;
using OnionCQRS.Domain.Entities;
using OnionCQRS.Domain.Model;

namespace OnionCQRS.Domain.Abstracts
{
    public interface ITokenHandler
    {
        /// <summary>
        /// Validate token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ValidateToken(TokenValidatedContext context);
        Task<string> CreateAccessToken(ApplicationUser user);
        Task<string> CreateRefreshToken(ApplicationUser user);
        Task<JwtModel> ValidateRefreshToken(string refreshToken);
    }
}
