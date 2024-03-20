using System.Text;
using event_scheduler.api.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace event_scheduler.api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration configuration)
 => services.AddDbContext<RepositoryContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgresLocal")));

    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetSection("JwtConfig:Issuer").Value,
                ValidAudience = configuration.GetSection("JwtConfig:Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration.GetSection("JwtConfig:Secret").Value))
            };
        });
    }

}