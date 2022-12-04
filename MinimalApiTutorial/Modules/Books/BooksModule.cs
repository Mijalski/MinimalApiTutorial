using MinimalApiTutorial.Modules.Books.Commands;
using MinimalApiTutorial.Modules.Books.Queries;

namespace MinimalApiTutorial.Modules.Books;

static class BooksModule
{
    public static IServiceCollection AddBooksModule(this IServiceCollection services) =>
        services.AddTransient<GetAll>()
            .AddTransient<CreateHandler>();

    public static IEndpointRouteBuilder MapBooksEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/books",
            async (HttpContext context, GetAll handler) =>
            {
                var books = await handler.HandleAsync(context.RequestAborted);
                return Results.Ok(books);
            })
            .WithName("GetBooks")
            .Produces<IEnumerable<BookDto>>()
            .WithTags("Books");

        endpoints.MapPost("/books",
            async (HttpContext context, CreateBookDto command, CreateHandler handler) =>
            {
                await handler.HandleAsync(command, context.RequestAborted);

                return Results.NoContent();
            })
            .WithName("CreateBook")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Books");

        return endpoints;
    }
}