using GhasemWebServiceRestFull.Dependency;
using Model;

namespace Application.ServiceContract
{
    public interface IAuthentication
    {
        ValueTask<ApiResult<TaminKonanadehLoginModel>> GetUserInfoAsync(string token);

    }
}
