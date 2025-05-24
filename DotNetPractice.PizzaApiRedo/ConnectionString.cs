using System.Data.SqlClient;

namespace DotNetPractice.PizzaApiRedo
{
    public static class ConnectionString
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "PizzaProjects",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };
    }
}
