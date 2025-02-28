using System.Data;
using System.Data.SqlClient;
using System.Text;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = ".";
stringBuilder.InitialCatalog = "DotNetSelfStudy";
stringBuilder.UserID = "sa";
stringBuilder.Password = "sa@123";
SqlConnection sqlconnection = new SqlConnection(stringBuilder.ConnectionString);

sqlconnection.Open();
Console.WriteLine("Connection Open");

string query = "select * from Blog_tbl";
SqlCommand cmd = new SqlCommand(query, sqlconnection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);
sqlconnection.Close();

foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine("BlogID => " + dr["BlogId"]);
    Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
    Console.WriteLine("BlogContent => " + dr["BlogContent"]);
    Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
    Console.WriteLine("-----------------------------------------------------");
}

Console.ReadKey();