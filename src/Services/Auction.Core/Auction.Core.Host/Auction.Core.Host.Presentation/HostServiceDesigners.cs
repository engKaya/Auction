using Auction.Core.Host.Service.HostExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Auction.Core.Host.Presentation
{
    public static class HostServiceDesigners
    {
        public static void ConfigureHostServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = false;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never; 
                });

            var secretkey = "TooImportantSecretKey2024Auction!!";
            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretkey));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signinKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
                //options.Events 
            }); 

            services
                .FindConfigurersAndExecute()
                .FindAssemblyBuildersAndExecute()
                .SetControllers();
        } 
    }
}
