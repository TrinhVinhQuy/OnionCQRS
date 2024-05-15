using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnionCQRS.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
    }
}
