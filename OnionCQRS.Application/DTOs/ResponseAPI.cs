
namespace OnionCQRS.Application.DTOs
{
    public class ResponseAPI
    {
        public string message { get; set; }
        public bool success { get; set; }
        public object? data { get; set; }
    }
}
