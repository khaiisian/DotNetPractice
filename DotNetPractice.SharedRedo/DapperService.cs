using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.SharedRedo
{
    public class DapperService
    {
        private readonly string _connectionString;
        public DapperService(string connection)
        {
            _connectionString = connection;
        }
        public List<T> QueryToList<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            List<T> lst = db.Query<T>(query, param).ToList();
            return (lst);
        }

        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Query<T>(query, param).FirstOrDefault();
            return (item!);
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Execute(query, param);
            return (item!);
        }
    }
}
