using MinimalApiTutorial.Shared;

namespace MinimalApiTutorial.Modules.Users.Commands;

class DeleteUserHostedService : BackgroundService
{
    private readonly IDeleteUserChannel _deleteUserChannel;
    private readonly IServiceScopeFactory<DeleteHandler> _deleteHandlerScopeFactory;

    public DeleteUserHostedService(IDeleteUserChannel deleteUserChannel,
        IServiceScopeFactory<DeleteHandler> deleteHandlerScopeFactory)
    {
        _deleteUserChannel = deleteUserChannel;
        _deleteHandlerScopeFactory = deleteHandlerScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await _deleteUserChannel.WaitToReadAsync(cancellationToken);
            if (!_deleteUserChannel.TryRead(out var deleteUser, cancellationToken))
            {
                Console.WriteLine("Something went wrong!");
                continue;
            }

            Console.WriteLine($"Deleting user: {deleteUser.Id}");

            var deleteHandler = _deleteHandlerScopeFactory.Get();
            await deleteHandler.HandleAsync(deleteUser, cancellationToken);
        }
    }
}