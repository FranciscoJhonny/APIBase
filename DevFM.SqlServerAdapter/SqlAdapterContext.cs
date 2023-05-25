using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.SqlServerAdapter
{
    public class SqlAdapterContext
    {
        private readonly string connectionString;
        public SqlAdapterContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IDbConnection dbConnection;
        public IDbConnection Connection
        {
            get
            {
                if (dbConnection == null)
                    dbConnection = new SqlConnection(connectionString);

                return dbConnection;
            }
        }

    }
}

