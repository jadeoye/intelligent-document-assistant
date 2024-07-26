using System;
using System.Data;
using Application.Interfaces;
using Dapper;
using Infrastructure.Configurations.Utilities;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistence
{
	public class ReadOnlyAppDbContext : IReadOnlyAppDbContext
	{
		private readonly IDbConnection _connection;

		public ReadOnlyAppDbContext(ReadOnlyDbConfiguration readOnlyDbConfiguration)
		{
			_connection = new SqlConnection(readOnlyDbConfiguration.ConnectionString);
		}

		public async Task<List<T>> QueryMultipleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
		{
			var query = await _connection.QueryMultipleAsync(sql, param, transaction);

			return query.Read<T>().ToList();
		}
 	}
}

