using Npgsql;

namespace Playground.Data;

public static class UrlToConnectionString
{
    public static string Convert(string url)
    {
        if (!url.StartsWith("postgres://")) return url;
        
        var uri = new Uri(url);
        var builder = new NpgsqlConnectionStringBuilder
        {
            Database = uri.LocalPath.TrimStart('/'),
            Host = uri.Host,
            Port = uri.Port,
            Username = uri.UserInfo.Split(':')[0],
            Password = uri.UserInfo.Split(':')[1],
            SslMode = SslMode.Allow
        };
        return builder.ConnectionString;

    }
}