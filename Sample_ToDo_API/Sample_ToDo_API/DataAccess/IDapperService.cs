using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample_ToDo_API.DataAccess
{
    public interface IDapperService
    {
        Task<IEnumerable<T>> LoadData<T>(string procName, DynamicParameters par = null);
        Task<int> Execute<T>(string sql, DynamicParameters par = null);
    }
}