using Microsoft.Data.SqlClient;

namespace DotNetPractice.MVCApp
{
    public static class ConnectionString
    {
        public static SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetSelfStudy",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };
    }
}
