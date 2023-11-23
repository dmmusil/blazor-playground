using Npgsql;
using Playground.Data;

namespace Playground.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var parsed = UrlToConnectionString.Convert("postgres://dmusil_playground:password@dmusil-playground-db.flycast:5432/dmusil_playground?sslmode");
        var builder = new NpgsqlConnectionStringBuilder(parsed);
        Assert.That(builder.Database, Is.EqualTo("dmusil_playground"));
        Assert.That(builder.Host, Is.EqualTo("dmusil-playground-db.flycast"));
        Assert.That(builder.Port, Is.EqualTo(5432));
        Assert.That(builder.Username, Is.EqualTo("dmusil_playground"));
        Assert.That(builder.Password, Is.EqualTo("password"));
        Assert.That(builder.SslMode, Is.EqualTo(SslMode.Allow));
    }
}