using Application.ServiceContract;
using GhasemWebServiceRestFull.Dependency;
using Infrastructure.SqlConnectionManagerGroup;
using Model;

namespace Application.ServiceImplement
{
    public class Authentication : IAuthentication
    {
        //private readonly SqlConnectionManager sqlConnection;

        //public Authentication(SqlConnectionManager sqlConnection)
        //{
        //    this.sqlConnection = sqlConnection;
        //}

        public ValueTask<ApiResult<TaminKonanadehLoginModel>> GetUserInfoAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
