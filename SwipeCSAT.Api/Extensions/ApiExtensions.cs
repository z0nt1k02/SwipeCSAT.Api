using SwipeCSAT.Api.Endpoints;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using SwipeCSAT.Api.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SwipeCSAT.Api.Extensions
{
    public static class ApiExtensions
    {

        public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
        {
            if(app is WebApplication webApp)
            {
                CategoriesEndpoints.MapCategoriesEndpoints(webApp);
                CriterionsEndpoints.MapCriterionsEdnpoints(webApp);
                ProductsEndpoints.MapProductsEndpoints(webApp);
                ReviewsEndpoints.MapReviewsEndpoints(webApp);
                UsersEndpoints.MapUsersEndpoints(webApp);
            }
            
        }

        public static void AddApiAuthentication(this IServiceCollection services,
            IOptions<JwtOptions> jwtOptions)
        {
            
            services.AddAuthentication(options =>{
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)) 
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {

                            context.Token = context.Request .Cookies["tasty-cookies"];
                            return Task.CompletedTask;
                        },
                    };
                });
            services.AddAuthorization();
        }
    }
}
