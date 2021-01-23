using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IPedgeProject.Data.AccessData
{
  public abstract class DbConnection
  {
    private readonly IConfiguration _config;
    private readonly string _connectionStringName;
    public string ConnectionString { get; }

    public DbConnection(
        IConfiguration config,
        string connectionStringName
        )
    {
      _config = config;
      _connectionStringName = connectionStringName;
      ConnectionString = _config[$"ConnectionStrings:{_connectionStringName}"];
    }

    internal SqlConnection Create()
    {
      return new SqlConnection(_config[$"ConnectionStrings:{_connectionStringName}"]);
    }
    //return the number of affected rows, usually used to execute: instert, update, delete
    public async Task<int> Execute(string sql, object param = null)
    {
      using (var sqlConn = Create())
      {
        await sqlConn.OpenAsync();
        var affectedRows = await sqlConn.ExecuteAsync(sql, param);
        return affectedRows;
      }
    }
    //query no return?
    public async Task Query(string sql, object param = null)
    {
      using (var sqlConn = Create())
      {
        await sqlConn.OpenAsync();
        await sqlConn.QueryAsync(sql, param);
      }
    }
    // query return the data list
    public async Task<IEnumerable<T>> Query<T>(string sql, object param = null)
    {
      using (var sqlConn = Create())
      {
        await sqlConn.OpenAsync();
        return (await sqlConn.QueryAsync<T>(sql, param)).ToArray();
      }
    }
    // It can execute a query and map the first result and throws an exception if there is not exactly one element in the sequence.
    public async Task<T> QuerySingle<T>(string sql, object param = null)
    {
      using (var sqlConn = Create())
      {
        await sqlConn.OpenAsync();
        return await sqlConn.QuerySingleAsync<T>(sql, param);
      }
    }
    // It can execute a query and map the first result, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
    public async Task<T> QuerySingleOrDefault<T>(string sql, object param = null)
    {
      using (var sqlConn = Create())
      {
        await sqlConn.OpenAsync();
        return await sqlConn.QuerySingleOrDefaultAsync<T>(sql, param);
      }
    }
    internal async Task StoredProcedure(string storedProcedureName, object param = null)
    {
      using (var sqlConn = Create())
      {
        await sqlConn.ExecuteAsync(storedProcedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: 300);
      }
    }
    //return the result, usually used to execute: SP
    internal async Task<IEnumerable<T>> StoredProcedure<T>(string storedProcedureName, object param = null)
    {
      using (var sqlConn = Create())
      {
        return await sqlConn.QueryAsync<T>(storedProcedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: 300);
      }
    }
    //execute multiple queries within the same command and map results.
    public async Task<List<object>> StoredProcedureMultiple(string storedProcedureName, object param = null)
    {
      using (var sqlConn = Create())
      {
        List<object> resultList = new List<object>();
        await sqlConn.OpenAsync();
        var multiResult = await sqlConn.QueryMultipleAsync(storedProcedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: 300);
        while (!multiResult.IsConsumed)
        {
          var result = multiResult.Read().ToList();
          if (result != null)
          {
            if (result.Count > 1)
            {
              resultList.Add(result);
            }
            else
            {
              resultList.Add(result);
            }
          }
        }
        return resultList;
      }
    }
  }
}
