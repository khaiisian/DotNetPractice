using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.CustomService
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<M> QueryToList<M> (string query, object ? param = null)
        {
            using IDbConnection db= new SqlConnection(_connectionString);
            List<M> lst = db.Query<M>(query, param).ToList();
            return lst;
        }

        public M? QueryFirstOrDefault<M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Query<M>(query, param).FirstOrDefault();
            return item;
        }

        public int ExecuteQuery (string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, param);
            return result;
        }
    }
}
