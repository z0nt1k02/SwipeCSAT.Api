using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Services;

namespace SwipeCSAT.Api.Endpoints
{
    public static class UsersEndpoints
    {
        
        public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/users");

            group.MapPost("/registration", async (RegisterUserRequest request,UserService userService) =>
            {
                await userService.Register(request.UserName,request.Email,request.Password);
                return Results.Ok();
            });

            group.MapPost("/login", async (LoginUserRequest request,UserService userService,HttpContext context) =>
            {
                var token = await userService.Login(request.Email,request.Password);
                context.Response.Cookies.Append("tasty-cookies",token);
                return Results.Ok(token);
            });

            return group;
        }

        
    }
}
