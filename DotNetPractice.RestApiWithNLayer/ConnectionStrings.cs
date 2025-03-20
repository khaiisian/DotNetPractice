using System.Data.SqlClient;

namespace DotNetPractice.RestApiWithNLayer
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetSelfStudy",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
        };
    }
}
