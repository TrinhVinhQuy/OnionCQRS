using System.ComponentModel.DataAnnotations;

namespace OnionCQRS.WebAPI.ViewModel
{
    public class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
