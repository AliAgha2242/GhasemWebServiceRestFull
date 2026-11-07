using Application.ServiceContract;
using Dapper;
using GhasemWebServiceRestFull.Dependency;
using Infrastructure.SqlConnectionManagerGroup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;

namespace Application.ServiceImplement
{
    public class TaminReports : ServiceContract.ITaminReports
    {
        private readonly SqlConnectionManager connectionManager;

        public TaminReports([FromKeyedServices("Mrs")] SqlConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }
        //public async ValueTask<ApiResult<List<IDictionary<string,object>>>> GetSalesAsync(decimal TaminSn, string fromDate, string toDate)
        //{
        //    try
        //    {
        //        Sql sql = new Sql()
        //        {
        //            Text = "rpSpG_ForooshWebService",
        //            commandType = System.Data.CommandType.StoredProcedure,
        //            Parameters = new { @TaminKonandehSN0 = TaminSn, @AzTarikh0 = fromDate, @TaTarikh0 = toDate }
        //        };
                
        //        //var result = (await connectionManager.QueryAsync(sql)).Cast<IDictionary<string, object>>().ToList();
        //        var res =  connectionManager.Connection.Query<dynamic>(sql.Text,param:sql.Parameters,buffered:false).AsQueryable().Take(2000);
        //        var a = res.Cast<IDictionary<string, object>>().ToList();
        //        return ApiResult<List<IDictionary<string, object>>>.Success(a);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResult<List<IDictionary<string, object>>>.Failed(null, ex);
        //    }
        //}

        async ValueTask<IEnumerable<dynamic>> ITaminReports.GetSalesAsync(decimal TaminSn, string fromDate, string toDate)
        {
            try
            {
                Sql sql = new Sql()
                {
                    Text = "rpSpG_ForooshWebService",
                    commandType = System.Data.CommandType.StoredProcedure,
                    Parameters = new { @TaminKonandehSN0 = TaminSn, @AzTarikh0 = fromDate, @TaTarikh0 = toDate }
                };

                //var result = (await connectionManager.QueryAsync(sql)).Cast<IDictionary<string, object>>().ToList();
                var res = await connectionManager.QueryAsync<dynamic>(sql,buffered: false);
                var a = res.AsEnumerable();
                return a;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
