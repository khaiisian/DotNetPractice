using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        //<M> => a generic type, object=> can be any data type
        public List<M> QueryList<M>(string query, object ? param = null){
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = db.Query<M>(query, param).ToList();
            return lst;
        }

        public M QueryFirstOrDefault<M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Query<M>(query, param).FirstOrDefault();
            return item!; // ! => ensuring that item will never be null
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection( _connectionString);
            int result = db.Execute(query, param);
            return result;
        }
    }
}
