using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherAPI.Helpers;
using WeatherAPI.Shared.CustomEntities;

var builder = WebApplication.CreateBuilder(args);

AppSettings appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

string connectionString = appSettings.ConnectionString;
string secretKey = appSettings.SecretKey;

byte[] secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);

// Figure out if there is something wrong with this configuration and that is why authorized endpoints don't work

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

DependencyInjectionHelper.InjectDbContext(builder.Services, connectionString);
DependencyInjectionHelper.InjectRepository(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
