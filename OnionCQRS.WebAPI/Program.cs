using OnionCQRS.Application;
using OnionCQRS.Persistence.Configuration;
using OnionCQRS.WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapper();
builder.Services.AddApplication();
//builder.Services.AddJWT();
builder.Services.AddCustomCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.AutoMigration().GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
