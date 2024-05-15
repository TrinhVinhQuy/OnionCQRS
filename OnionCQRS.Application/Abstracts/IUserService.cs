
using OnionCQRS.Domain.Entities;

namespace OnionCQRS.Application.Abstracts
{
    public interface IUserService
    {
        Task<ApplicationUser> CheckLogin(string username, string password);
    }
}
