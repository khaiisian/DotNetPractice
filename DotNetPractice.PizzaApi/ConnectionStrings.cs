using System.Data.SqlClient;

namespace DotNetPractice.PizzaApi
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "PizzaProjects",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };
    }
}
