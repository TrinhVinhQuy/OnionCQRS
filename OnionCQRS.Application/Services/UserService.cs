using Microsoft.AspNetCore.Identity;
using OnionCQRS.Application.Abstracts;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Entities;

namespace OnionCQRS.Application.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public Task<ApplicationUser> CheckLogin(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
