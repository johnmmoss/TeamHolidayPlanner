using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Web.AcceptanceTests
{
    public class Database
    {
        private readonly string connectionString;
        private readonly string filePath;

        public Database(string connectionString, string filePath)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Data file does not exist", "filePath");
            }
        }

        public void Reset()
        {
            var sql = File.ReadAllText(filePath);

            using (var conn = new SqlConnection(connectionString))
            {
                var serverConnection = new ServerConnection(conn);
                var server = new Server(serverConnection);

                server.ConnectionContext.ExecuteNonQuery(sql);
            }
        }
    }
}
