using Application.ServiceContract;
using Application.ServiceImplement;
using Infrastructure.SqlConnectionManagerGroup;

namespace GhasemWebServiceRestFull.Dependency;

public static class DiRegister
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var taminConnectionString =
            configuration.GetConnectionString("TaminConnectionString")
            ?? throw new ArgumentException("Tamin connection string is not configured.");

        var mrsConnectionString =
            configuration.GetConnectionString("MrsConnectionString")
            ?? throw new ArgumentException("Mrs connection string is not configured.");

        // The managers only hold immutable connection-string state and create
        // a new pooled SqlConnection per operation, so singleton lifetime is safe.
        services.AddKeyedSingleton(
            "Tamin",
            new SqlConnectionManager(new CSConfig(taminConnectionString)));

        services.AddKeyedSingleton(
            "Mrs",
            new SqlConnectionManager(new CSConfig(mrsConnectionString)));

        services.AddKeyedScoped<ITaminReports, TaminReports>("Mrs");

        return services;
    }
}
