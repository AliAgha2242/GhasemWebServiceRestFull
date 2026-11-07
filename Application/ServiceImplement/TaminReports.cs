using Application.ServiceContract;
using Dapper;
using Infrastructure.SqlConnectionManagerGroup;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Application.ServiceImplement;

public sealed class TaminReports : ITaminReports
{
    private readonly SqlConnectionManager _connectionManager;

    public TaminReports(
        [FromKeyedServices("Mrs")] SqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async IAsyncEnumerable<dynamic> GetSalesAsync(
        decimal taminSn,
        string fromDate,
        string toDate,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var sql = new Sql
        {
            Text = "rpSpG_ForooshWebService",
            commandType = System.Data.CommandType.StoredProcedure,
            Parameters = new
            {
                TaminKonandehSN0 = taminSn,
                AzTarikh0 = fromDate,
                TaTarikh0 = toDate
            }
        };

        await using var connection = _connectionManager.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        var command = new CommandDefinition(
            commandText: sql.Text,
            parameters: sql.Parameters,
            transaction: sql.transaction,
            commandType: sql.commandType,
            cancellationToken: cancellationToken);

        // Dapper reads rows asynchronously without buffering the complete result.
        // The connection remains alive until enumeration completes or is cancelled.
        await foreach (var row in connection
            .QueryUnbufferedAsync<dynamic>(command)
            .WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return row;
        }
    }
}
