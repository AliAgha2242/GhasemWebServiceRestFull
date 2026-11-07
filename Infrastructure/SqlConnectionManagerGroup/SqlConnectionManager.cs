using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.SqlConnectionManagerGroup
{
    public class SqlConnectionManager
    {

        public IDbConnection Connection { get; set; }
        public CSConfig SConfig { get; private set; }
        public SqlConnectionManager(CSConfig sConfig)
        {
            SConfig = sConfig;
            using (IDbConnection Conn = new SqlConnection(SConfig.Connectionstring))
            {
                Connection = Conn;
                Connection.Open();
            }
            Connection.ConnectionString = SConfig.Connectionstring;

        }
        public dynamic QuerySingle(Sql sql)
        {
            return Connection.QuerySingle(sql.Text, sql.Parameters,transaction:sql.transaction, commandType: sql.commandType);
        }
        public async Task<dynamic> QuerySingleAsync(Sql sql)
        {
            return await Connection.QuerySingleAsync(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }

        public T QuerySingle<T>(Sql sql)
        {
            return Connection.QuerySingle<T>(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }
        public async Task<T> QuerySingleAsync<T>(Sql sql)
        {
            return await Connection.QuerySingleAsync<T>(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType );

        }



        public dynamic? QuerySingleOrDefault(Sql sql)
        {
            return Connection.QuerySingleOrDefault(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }
        public async Task<dynamic?> QuerySingleOrDefaultAsync(Sql sql)
        {
            return await Connection.QuerySingleOrDefaultAsync(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }

        public T? QuerySingleOrDefault<T>(Sql sql)
        {
            return Connection.QuerySingleOrDefault<T>(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }
        public async Task<T?> QuerySingleOrDefaultAsync<T>(Sql sql)
        {
            return await Connection.QuerySingleOrDefaultAsync<T>(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }




        public IEnumerable<T> Query<T>(Sql sql)
        {
            return Connection.Query<T>(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }
        public IEnumerable<dynamic> Query(Sql sql)
        {
            return Connection.Query(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }





        public async Task<IEnumerable<T>> QueryAsync<T>(Sql sql)
        {
            return await Connection.QueryAsync<T>(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }

        public async Task<IEnumerable<dynamic>> QueryAsync(Sql sql)
        {
            return await Connection.QueryAsync(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }

        public int Execute(Sql sql)
        {
            return Connection.Execute(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }
        public async Task<int> ExecuteAsync(Sql sql)
        {
            return await Connection.ExecuteAsync(sql.Text, sql.Parameters, transaction: sql.transaction, commandType: sql.commandType);

        }
        public async Task<IQueryable<T>> QueryAsync<T>(Sql sql,bool buffered=true)
        {
            return await Task.Run<IQueryable<T>>(() =>
            {
                return Connection.Query<T>(sql: sql.Text, param: sql.Parameters, transaction: sql.transaction, buffered: buffered, commandType: sql.commandType).AsQueryable();
            });
        }
        public IQueryable<T> Query<T>(Sql sql, bool buffered = true)
        {
            return Connection.Query<T>(sql: sql.Text, param: sql.Parameters, transaction: sql.transaction, buffered: buffered, commandType: sql.commandType).AsQueryable();
        }

    }
}
