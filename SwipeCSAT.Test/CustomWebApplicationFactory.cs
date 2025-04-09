
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwipeCSAT.Api;
using AuthorizationOptions = SwipeCSAT.Api.Repositories.AuthorizationOptions;


namespace SwipeCSAT.Test;

public class CustomWebApplicationFactory<TProgram> :  WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.RemoveAll<IAuthorizationHandler>();
           
            services.AddSingleton<IAuthorizationHandler, AllowAnnonymous>();
                
            services.RemoveAll<DbContextOptions<SwipeCsatDbContext>>();
            
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions,MockAuthenticationHandler>("Test",options => { });
            services.AddAuthorization(options =>
            {
                
            });
            /*var connString = "Host=localhost;Database=SwipeCSAT;Username=postgres;Password=An4ous228";*/
            var connString = GetConnectionString();
            if (string.IsNullOrEmpty(connString))
            {
                throw new InvalidOperationException("Could not find connection string in configuration.");
            }

           
            services.AddDbContext<SwipeCsatDbContext>(options =>
            {
                options.UseNpgsql(connString);
            });
            
        });
        
    }

    private static string? GetConnectionString()
    {
        /*var configuration = new ConfigurationBuilder().Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        return connectionString;*/
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Указываем текущую директорию (выходная папка теста)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Загружаем конфиг из appsettings.json
            .Build();
        return configuration.GetConnectionString("DefaultConnection");
    }
}

public class AllowAnnonymous : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements.ToList())
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

public class MockAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public MockAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder  
    ) : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Создаем фейкового пользователя
        var claims = new[] {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Admin") // или другую роль, если нужно
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

    

