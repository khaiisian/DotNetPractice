using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNetPractice.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<M> QueryList<M>(string query, params AdoParameters[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            if(parameters is not null && parameters.Length > 0)
            {
                //foreach (var param in parameters)
                //{
                //    cmd.Parameters.AddWithValue(param.Name, param.Value);
                //}

                cmd.Parameters.AddRange(parameters.Select(param => new SqlParameter(param.Name, param.Value)).ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<M> lst = JsonConvert.DeserializeObject<List<M>>(json)!;
            return lst;

            // Personal notes
            //DeserializeObject is a generic method, needs to be told what type it should deserialize into.
            // Without <List<M>>, the method does not know what type to convert the JSON into, leading to a compile-time error.
        }

        public M QueryFirstOrDefault<M>(string query, params AdoParameters[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(param => new SqlParameter(param.Name, param.Value)).ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<M> lst = JsonConvert.DeserializeObject<List<M>>(json)!;
            return lst[0];
        }

        public int Execute(string query, params AdoParameters[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {
                foreach(var param in parameters)
                {
                    if(param is not null)
                    {

                    }
                }
                cmd.Parameters.AddRange(parameters.Select(param => new SqlParameter(param.Name, param.Value)).ToArray());
            }

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }


    }

    public class AdoParameters
    {
        public AdoParameters(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
     }
}
