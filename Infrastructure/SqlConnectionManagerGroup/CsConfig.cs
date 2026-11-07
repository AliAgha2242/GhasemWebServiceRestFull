
using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace Infrastructure.SqlConnectionManagerGroup
{
    public class CSConfig
    {
        public string Password { get; set; }
        public string UserId { get; set; }
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public int TimeOutMiliSecond { get; set; }
        public bool TrustServerCertificate { get; set; }
        public bool MultipleActiveResultSets { get; set; }
        public SqlLoginType? sqlLoginType { get; set; }
        public string Connectionstring { get; set; }
        public CSConfig() { }
        public CSConfig(OwnServerInfo serverInfo)
        {
            Password = serverInfo.Password;
            UserId = serverInfo.UserId;
            DbName = serverInfo.DataBaseName;
            ServerName = serverInfo.ServerName;
            TimeOutMiliSecond = serverInfo.TimeOutMiliSecond;
            TrustServerCertificate = serverInfo.TrustServerCertificate;
            MultipleActiveResultSets = serverInfo.MultipleActiveResultSets;
            sqlLoginType = serverInfo.sqlLoginType;
            CsGenerate();
        }


        public CSConfig(string cs)
        {
            var builder = new SqlConnectionStringBuilder(cs);

            Password = builder.Password;
            UserId = builder.UserID;
            ServerName = builder.DataSource;
            DbName = builder.InitialCatalog;
            TimeOutMiliSecond = builder.ConnectTimeout * 1000;
            TrustServerCertificate = builder.TrustServerCertificate;
            MultipleActiveResultSets = builder.MultipleActiveResultSets;
            sqlLoginType = builder.IntegratedSecurity
                ? SqlLoginType.WindowsAuthentication
                : null;
            Connectionstring = cs;
        }
       
        private string CsGenerate()
        {
            string Cs;
            if (sqlLoginType == SqlLoginType.WindowsAuthentication)
            {
                Cs = @$"server={ServerName};initial catalog={DbName};Trusted_Connection={TrustServerCertificate};TrustServerCertificate={TrustServerCertificate.ToString()};Connect Timeout={TimeOutMiliSecond};";
            }
            else
            {
                Cs = @$"server={ServerName};initial catalog={DbName};user id={UserId};password={Password};
                           TrustServerCertificate={TrustServerCertificate};MultipleActiveResultSets={MultipleActiveResultSets};Connect Timeout={TimeOutMiliSecond};";
            }

            //string Cs = @$"data source={ServerName};initial catalog={DbName};user id={UserId};password={Password};Trusted_Connection={TrustServerCertificate};MultipleActiveResultSets={MultipleActiveResultSets};Connect Timeout={TimeOutMiliSecond};";
            Connectionstring = Cs;
            return Cs;
        }
    }
}
