using DotNetPractice.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using System.Text;

Console.WriteLine("Hello, World!");

//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource = ".";
//stringBuilder.InitialCatalog = "DotNetSelfStudy";
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "sa@123";
//SqlConnection sqlconnection = new SqlConnection(stringBuilder.ConnectionString);

////sqlconnection.Open();
////Console.WriteLine("Connection Open");
////sqlconnection.Close();

//string query = "select * from Blog_tbl";
//SqlCommand cmd = new SqlCommand(query, sqlconnection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//adapter.Fill(dt);

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("BlogID => " + dr["BlogId"]);
//    Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
//    Console.WriteLine("BlogContent => " + dr["BlogContent"]);
//    Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
//    Console.WriteLine("-----------------------------------------------------");
//}


//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("new title", "new content", "new author");
//adoDotNetExample.Update(6, "updated title", "updated content", "updated author");
//adoDotNetExample.Delete(6);
//adoDotNetExample.Edit(1000);
//Console.ReadKey();


//AdoDotNetRedo adoDotNetRedo = new AdoDotNetRedo();
//adoDotNetRedo.Edit(1);
//adoDotNetRedo.Create("new Title", "new Content", "new Author");
//adoDotNetRedo.Update(7, "updated Title", "updated Content", "updated Author");
//adoDotNetRedo.Delete(7);
//adoDotNetRedo.Read();



//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

//DapperRedo dapperRedo = new DapperRedo();
//dapperRedo.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();

Console.ReadKey();