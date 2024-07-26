using System;
using System.Data;

namespace Application.Interfaces
{
	public interface IReadOnlyAppDbContext
	{
		Task<List<T>> QueryMultipleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
	}
}

