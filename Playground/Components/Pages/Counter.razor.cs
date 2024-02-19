using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Playground.Components.Pages;

public partial class Counter
{
    private HubConnection? _connection;
    private int? _currentCount;

    protected override async Task OnInitializedAsync()
    {
        var absoluteUri = Nav.ToAbsoluteUri(CounterHub.Path);
        absoluteUri = EnsureHttpsAndPreserveNonStandardPort(absoluteUri);
        _connection = new HubConnectionBuilder()
            .WithUrl(absoluteUri)
            .Build();
        _connection.On<int>("NewValue", i =>
        {
            _currentCount = i;
            InvokeAsync(StateHasChanged);
        });
        await _connection.StartAsync();
        await _connection.SendAsync("OnConnect");
    }

    /// <summary>
    /// When behind a reverse proxy that terminates TLS, the absolute URI from NavManager
    /// uses http as the scheme. Attempting to connect via insecure http fails. Use HTTPS and
    /// preserve the port if we're running locally with some port other than 443.
    /// </summary>
    /// <param name="absoluteUri"></param>
    /// <returns></returns>
    private static Uri EnsureHttpsAndPreserveNonStandardPort(Uri absoluteUri)
    {
        var port = absoluteUri.Port is 443 or 80 ? 443 : absoluteUri.Port;
        absoluteUri = new UriBuilder(absoluteUri) {Scheme = "https", Port = port}.Uri;
        return absoluteUri;
    }

    private Task IncrementCount() =>
        _connection is not null
            ? _connection.SendAsync("OnIncrement")
            : Task.CompletedTask;
}