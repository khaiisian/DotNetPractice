using System.Data.SqlClient;

namespace DotNetPractice.PizzaApiWithMultiplePizzas
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "MultiplePizzaOrdersProject",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };
    }
}
