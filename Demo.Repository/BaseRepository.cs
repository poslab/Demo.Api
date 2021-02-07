using Dapper;
using Demo.Repository.Interfaces;
using Demo.SharedKernel.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public class BaseRepository<T, TSelectModel> : IBaseRepository<T, TSelectModel>
    {
        protected readonly string _connectionString;
        protected readonly string _modelName;
        public BaseRepository(IOptions<ConnectionStrings> connectionStringsOption) : this(connectionStringsOption.Value.DemoDB) { }
        public BaseRepository(string connectionString) : base()
        {
            _connectionString = connectionString;
            _modelName = typeof(T).Name;
        }
        public async Task<int> InsertAsync(T item)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // Returns ID of inserted record (Use OUTPUT Inserted.<ObjectID> in stored procedure)
                return await conn.QuerySingleAsync<int>($"{_modelName}_Insert", item, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> UpdateAsync(T item)
        {
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    // Returns number of records updated
            //    return await conn.ExecuteAsync($"{_modelName}_Update", item, commandType: CommandType.StoredProcedure);
            //}

            // Dummy Code
            await Task.Delay(0);
            return 1;
        }

        public async Task<int> DeleteAsync(object whereClause)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // Returns number of records deleted
                return await conn.ExecuteAsync($"{_modelName}_Delete", whereClause, commandType: CommandType.StoredProcedure);
            }
        }
        public virtual async Task<List<T>> GetAsync(TSelectModel whereClause)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return (await conn.QueryAsync<T>($"{_modelName}_Select", whereClause, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<T>($"{_modelName}_SelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        public virtual async Task<T> GetSingleRecordAsync(TSelectModel whereClause)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return (await conn.QueryAsync<T>($"{_modelName}_Select", whereClause, commandType: CommandType.StoredProcedure)).ToList().FirstOrDefault();
            }
        }
    }
}
