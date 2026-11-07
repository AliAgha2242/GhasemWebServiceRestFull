using Infrastructure.SqlConnectionManagerGroup;

namespace GhasemWebServiceRestFull.Dependency
{
    public static class DiRegister
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            CSConfig taminConfig = new CSConfig(configuration.GetConnectionString("TaminConnectionString")??throw new ArgumentException("connection String Is Not Valid"));
            CSConfig mrsConfig = new CSConfig(configuration.GetConnectionString("MrsConnectionString") ?? throw new ArgumentException("connection String Is Not Valid"));
            services.AddKeyedSingleton("taminCs",taminConfig);
            services.AddKeyedSingleton("mrsCs", mrsConfig);
            services.AddKeyedScoped<SqlConnectionManager>("Tamin",(serviceprovider,p)=>new SqlConnectionManager(taminConfig));
            services.AddKeyedScoped<SqlConnectionManager>("Mrs", (serviceprovider, p) => new SqlConnectionManager(mrsConfig));

            return services;
        }
    }
}
