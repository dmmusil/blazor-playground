using Microsoft.AspNetCore.SignalR;

namespace Playground;

public class CounterHub : Hub
{
    public const string Path = "/ws/counter";

    private int _value = 0;

    public Task OnIncrement() => Clients.All.SendAsync("NewValue", Interlocked.Increment(ref _value));
    public Task OnConnect() => Clients.Caller.SendAsync("NewValue", _value);
}