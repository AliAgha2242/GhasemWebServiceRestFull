using GhasemWebServiceRestFull.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ServiceContract
{
    public interface ITaminReports
    {
        ValueTask<IEnumerable<dynamic>> GetSalesAsync(decimal TaminSn, string fromDate, string toDate);
    }
}
