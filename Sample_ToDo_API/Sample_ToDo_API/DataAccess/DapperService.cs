using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Sample_ToDo_API.DataAccess
{
    public class DapperService : IDapperService
    {
        private readonly IConfiguration _config;

        public DapperService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T>(string procName, DynamicParameters par = null)
        {

            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("Local")))
            {
                try
                {
                    con.Open();
                    return await con.QueryAsync<T>(procName, par, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Dapper exception: {ex.Message}");
                }
                finally
                {
                    con.Dispose();
                }
            }

        }

        //For plain sql command - not a procedure!
        //Returns number of affected rows
        public async Task<int> Execute<T>(string sql, DynamicParameters par = null)
        {
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("Local")))
            {
                try
                {
                    con.Open();
                    return await con.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Dapper exception: {ex.Message}");
                }
                finally
                {
                    con.Dispose();
                }
            }
        }
    }
}
