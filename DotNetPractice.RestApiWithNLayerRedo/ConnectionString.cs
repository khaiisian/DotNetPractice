using System.Data.SqlClient;

namespace DotNetPractice.RestApiWithNLayerRedo
{
    public static class ConnectionString
    {
        public static SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetSelfStudy",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
        };
    }
}
