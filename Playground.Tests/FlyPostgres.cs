using Npgsql;
using Playground.Data;

namespace Playground.Tests;

public class FlyPostgres
{
    [Test]
    public void CanParseConnectionString()
    {
        var parsed = UrlToConnectionString.Convert("postgres://username:password@dbserver.flycast:5432/playground?sslmode");
        var builder = new NpgsqlConnectionStringBuilder(parsed);
        Assert.Multiple(() =>
        {
            Assert.That(builder.Database, Is.EqualTo("playground"));
            Assert.That(builder.Host, Is.EqualTo("dbserver.flycast"));
            Assert.That(builder.Port, Is.EqualTo(5432));
            Assert.That(builder.Username, Is.EqualTo("username"));
            Assert.That(builder.Password, Is.EqualTo("password"));
            Assert.That(builder.SslMode, Is.EqualTo(SslMode.Allow));
        });
    }
}