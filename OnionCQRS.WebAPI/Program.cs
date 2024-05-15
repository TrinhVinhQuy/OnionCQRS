using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using OnionCQRS.Application;
using OnionCQRS.Persistence.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapper();
builder.Services.AddApplication();
//builder.Services.AddJWT();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterTokenBear(builder.Configuration);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sample.WebApiRestful",
        Version = "v1",
        Description = "This is Swagger WebAPI Restful",

    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Description = "Please input your token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
    
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "MyPolicy", builder =>
        {
            builder.AllowAnyOrigin().
                    AllowAnyMethod().
                    AllowAnyHeader();
        });
});
var app = builder.Build();

app.AutoMigration().GetAwaiter().GetResult();

app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
