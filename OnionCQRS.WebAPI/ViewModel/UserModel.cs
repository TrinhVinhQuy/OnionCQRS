using System.ComponentModel.DataAnnotations;

namespace OnionCQRS.WebAPI.ViewModel
{
    public class UserModel : AccountModel
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
