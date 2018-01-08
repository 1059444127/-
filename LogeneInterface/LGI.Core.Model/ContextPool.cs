using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Net.Mime;
using LGI.Core.Utils;

namespace LGI.Core.Model
{
    public class ContextPool
    {
        private static PathnetEntities _db = null;

        public static PathnetEntities GetContext()
        {
            if (_db == null)
            {
                IniFiles f =new IniFiles(AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"\\sz.ini");
                var host = f.ReadString("sqlserver", "host", "");
                if(string.IsNullOrEmpty(host))
                    host = f.ReadString("sqlserver", "server", "localhost");
                var uid = f.ReadString("sqlserver", "userId", "pathnet");
                var pwd = f.ReadString("sqlserver", "password", "4s3c2a1p");

                _db = PathnetEntities.ConnectToSqlServer(host, "pathnet", uid, pwd, false);
                //   _db.Database.Connection.ConnectionString = "Data Source=LDYXPS15;Initial Catalog=pathnet;User ID=pathnet;Password=4s3c2a1p";
            }

            //logging
            _db.Database.Log += log =>
            {
                Logger.Info($"[{DateTime.Now.ToString("u")}]linq to sql:\r\n{log}");
                Console.WriteLine($"[{DateTime.Now.ToString("u")}]linq to sql:\r\n{log}");
            };

            return _db;
        }
    }

    public partial class PathnetEntities
    {
        private PathnetEntities(string connStr)
            : base(connStr)
        {
        }

        public static PathnetEntities ConnectToSqlServer(string host, string catalog, string user, string pass,
            bool winAuth)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = host,
                InitialCatalog = catalog,
                PersistSecurityInfo = true,
                IntegratedSecurity = winAuth,
                MultipleActiveResultSets = true,
                UserID = user,
                Password = pass,
            };

            // assumes a connectionString name in .config of MyDbEntities
            var entityConnectionStringBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlBuilder.ConnectionString,
                Metadata = "res://*/",
            };


            return new PathnetEntities(entityConnectionStringBuilder.ConnectionString);
        }
    }
}