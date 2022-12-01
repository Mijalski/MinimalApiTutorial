using Microsoft.AspNetCore.Mvc;
using MinimalApiTutorial.Modules.Users.Commands;
using MinimalApiTutorial.Modules.Users.Jwts;
using MinimalApiTutorial.Modules.Users.Passwords;
using MinimalApiTutorial.Modules.Users.Queries;

namespace MinimalApiTutorial.Modules.Users;

static class UsersModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services) =>
        services.AddTransient<GetByNameHandler>()
            .AddTransient<CreateHandler>()
            .AddTransient<LogInHandler>()
            .AddTransient<DeleteHandler>()
            .AddTransient<IUserPasswordService, UserBCryptPasswordService>()
            .AddTransient<IJwtTokenGeneratorService, JwtTokenGeneratorService>();

    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/users",
            async (HttpContext context, GetByNameHandler handler, [FromQuery] string name) =>
            {
                if (string.IsNullOrEmpty(name))
                {
                    return Results.BadRequest();
                }

                var user = await handler.GetUserOrDefault(name, context.RequestAborted);
                return user is null ? Results.NotFound() : Results.Ok(user);
            })
            .WithName("GetUserByName")
            .RequireAuthorization()
            .Produces<UserDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Users");

        endpoints.MapPost("/users",
            async (HttpContext context, CreateUserDto command, CreateHandler handler) =>
            {
                await handler.HandleAsync(command, context.RequestAborted);

                return Results.NoContent();
            })
            .WithName("CreateUser")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Users");

        endpoints.MapPut("/users/login",
                async (HttpContext context, LogInDto command, LogInHandler handler) =>
                {
                    var token = await handler.HandleAsync(command, context.RequestAborted);

                    return string.IsNullOrEmpty(token) ? Results.BadRequest() : Results.Ok(token);
                })
            .WithName("Login")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<string>()
            .WithTags("Users");


        endpoints.MapDelete("/users",
                async (HttpContext context, DeleteHandler handler) =>
                {
                    await handler.HandleAsync(context.RequestAborted);
                    return Results.NoContent();
                })
            .WithName("DeleteUser")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Users");

        return endpoints;
    }
}
