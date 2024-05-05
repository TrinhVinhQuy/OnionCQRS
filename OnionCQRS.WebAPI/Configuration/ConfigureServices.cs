using Microsoft.IdentityModel.Tokens;

namespace OnionCQRS.WebAPI.Configuration
{
    public static class ConfigureServices
    {
        // Thêm phương thức này để cấu hình CORS
        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500")
                            .WithMethods()
                            .AllowAnyHeader();
                    });
            });
        }
    }
}
