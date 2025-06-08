using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Newtonsoft.Json;

namespace DotNetPractice.CustomService
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> QueryList<T>(string query, AdoDotNetParameter[]? adoDotNetParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            Console.WriteLine("Connection Open");
            connection.Close();
            //string query = "select * from Blog_tbl";
            SqlCommand cmd = new SqlCommand(query, connection);

            if(adoDotNetParameters != null && adoDotNetParameters.Length>0)
            {
                var paramArray = adoDotNetParameters.Select(x=> new SqlParameter(x.Name, x.Value)).ToArray();
                cmd.Parameters.AddRange(paramArray);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            string jsonStr = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(jsonStr)!;
            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, AdoDotNetParameter[]? adoDotNetParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            SqlCommand cmd = new SqlCommand(query, connection);

            if(adoDotNetParameters!=null && adoDotNetParameters.Length > 0)
            {
                var paramArray = adoDotNetParameters.Select(x=>new SqlParameter(x.Name, x.Value)).ToArray();
                cmd.Parameters.AddRange(paramArray);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            string jsonArray = JsonConvert.SerializeObject(dt);
            var item = JsonConvert.DeserializeObject<T>(jsonArray)!;
            return item;
        }

        public int QueryExecute(string query, AdoDotNetParameter[]? adoDotNetParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if(adoDotNetParameters!=null && adoDotNetParameters.Length > 0)
            {
                var paramArray = adoDotNetParameters.Select(x=> new SqlParameter(x.Name, x.Value)).ToArray();
                cmd.Parameters.AddRange(paramArray);
            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public AdoDotNetParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
