using System.Data.SqlClient;

namespace DotNetPractice.RestApiRedo
{
    internal class ConnectionString
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
            InitialCatalog = "DotNetSelfStudy",
        };
    }
}
