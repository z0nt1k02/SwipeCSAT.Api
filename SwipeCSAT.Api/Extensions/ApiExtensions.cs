﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SwipeCSAT.Api.Authorization;
using SwipeCSAT.Api.Endpoints;
using SwipeCSAT.Api.Enums;
using SwipeCSAT.Api.Infrastructure;
using SwipeCSAT.Api.Interfaces;
using SwipeCSAT.Api.Services;

namespace SwipeCSAT.Api.Extensions;

public static class ApiExtensions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        if (app is WebApplication webApp)
        {
            webApp.MapCategoriesEndpoints();
            webApp.MapCriterionsEdnpoints();
            webApp.MapProductsEndpoints();
            webApp.MapReviewsEndpoints();
            webApp.MapUsersEndpoints();
        }
    }

    public static void AddApiAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["tasty-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddScoped<IPermissionService,PermissionService>();
        services.AddSingleton<IAuthorizationHandler,PermissionAuthorizationHandler>();

        services.AddAuthorization();
    }

    public static IEndpointConventionBuilder RequirePermissions<TBuilder>(this TBuilder builder,
        params Permission[] permissions) where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy =>
            policy.AddRequirements(new PermissionRequirment(permissions)));

    }
    
}