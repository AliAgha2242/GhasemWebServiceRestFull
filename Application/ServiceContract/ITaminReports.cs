namespace Application.ServiceContract;

public interface ITaminReports
{
    IAsyncEnumerable<dynamic> GetSalesAsync(
        decimal taminSn,
        string fromDate,
        string toDate,
        CancellationToken cancellationToken = default);
}
