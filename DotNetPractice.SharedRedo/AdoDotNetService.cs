using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.SharedRedo
{
    public class AdoDotNetService
    {
        private readonly SqlConnection _connection;

        public AdoDotNetService(string connection)
        {
            _connection = new SqlConnection(connection);
        }

        public List<T> QueryToList<T>(string query, params ParamArray[]? paramArray)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);

            if (paramArray is not null && paramArray.Length > 0)
            {
                cmd.Parameters.AddRange(paramArray.Select(x=> new SqlParameter(x.Name, x.Value)).ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            _connection.Close();

            var json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;
            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params ParamArray[]? paramArray)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);

            if (paramArray is not null && paramArray.Length > 0)
            {
                cmd.Parameters.AddRange(paramArray.Select(x => new SqlParameter(x.Name, x.Value)).ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            _connection.Close();

            var json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;

            if(lst.Count == 0)
            {
                return default;
            }
            return lst[0];
        }

        public int Execute(string query, params ParamArray[] paramArray)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            
            cmd.Parameters.AddRange(paramArray.Select(x=>new SqlParameter(x.Name, x.Value)).ToArray());

            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            return result;
        }


        public int PatchExecute(string query, ParamArray[] paramArray)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);

            foreach (var param in paramArray)
            {
                if(param is not null)
                {
                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
            }

            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            return result;
        }
    }

    public class ParamArray
    {
        public ParamArray(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
