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

		public async Task Execute(string sql, object param = null)
		{
			using (var sqlConn = Create())
			{
				await sqlConn.OpenAsync();
				await sqlConn.ExecuteAsync(sql, param);
			}
		}

		public async Task Query(string sql, object param = null)
		{
			using (var sqlConn = Create())
			{
				await sqlConn.OpenAsync();
				await sqlConn.QueryAsync(sql, param);
			}
		}

		public async Task<IEnumerable<T>> Query<T>(string sql, object param = null)
		{
			using (var sqlConn = Create())
			{
				await sqlConn.OpenAsync();
				return (await sqlConn.QueryAsync<T>(sql, param)).ToArray();
			}
		}

		public async Task<T> QuerySingle<T>(string sql, object param = null)
		{
			using (var sqlConn = Create())
			{
				await sqlConn.OpenAsync();
				return await sqlConn.QuerySingleAsync<T>(sql, param);
			}
		}

		public async Task<T> QuerySingleOrDefault<T>(string sql, object param = null)
		{
			using (var sqlConn = Create())
			{
				await sqlConn.OpenAsync();
				return await sqlConn.QuerySingleOrDefaultAsync<T>(sql, param);
			}
		}

		internal async Task<IEnumerable<T>> StoredProcedure<T>(string storedProcedureName, object param = null)
		{
			using (var sqlConn = Create())
			{
				return await sqlConn.QueryAsync<T>(storedProcedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: 300);
			}
		}

		internal async Task StoredProcedure(string storedProcedureName, object param = null)
		{
			using (var sqlConn = Create())
			{
				await sqlConn.ExecuteAsync(storedProcedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: 300);
			}
		}
	}
}
