using Microsoft.Data.SqlClient;

namespace Infrastructure.SqlConnectionManagerGroup;

/// <summary>
/// Creates independent SQL connections. The manager itself is stateless and safe
/// to share between requests; each caller owns and disposes the connection it creates.
/// </summary>
public sealed class SqlConnectionManager
{
    private readonly string _connectionString;

    public SqlConnectionManager(CSConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        if (string.IsNullOrWhiteSpace(config.Connectionstring))
        {
            throw new ArgumentException(
                "A valid SQL connection string is required.",
                nameof(config));
        }

        _connectionString = config.Connectionstring;
    }

    public SqlConnection CreateConnection() => new(_connectionString);
}
