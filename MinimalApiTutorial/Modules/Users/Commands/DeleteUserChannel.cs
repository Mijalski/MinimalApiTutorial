using System.Diagnostics.CodeAnalysis;
using System.Threading.Channels;

namespace MinimalApiTutorial.Modules.Users.Commands;

interface IDeleteUserChannel
{
    ValueTask AddUserToDeleteAsync(DeleteUserDto deleteUserDto, CancellationToken cancellationToken);
    ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken);
    bool TryRead([MaybeNullWhen(false)] out DeleteUserDto deleteUserDto, CancellationToken cancellationToken);
}

class DeleteUserChannel : IDeleteUserChannel
{
    private readonly Channel<DeleteUserDto> _channel;

    public DeleteUserChannel()
    {
        _channel = Channel.CreateUnbounded<DeleteUserDto>();
    }

    public ValueTask AddUserToDeleteAsync(DeleteUserDto deleteUserDto, CancellationToken cancellationToken) =>
        _channel.Writer.WriteAsync(deleteUserDto, cancellationToken);

    public ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken) =>
        _channel.Reader.WaitToReadAsync(cancellationToken);

    public bool TryRead([MaybeNullWhen(false)] out DeleteUserDto deleteUserDto, CancellationToken cancellationToken) =>
        _channel.Reader.TryRead(out deleteUserDto);
}