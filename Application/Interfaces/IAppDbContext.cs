using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
	public interface IAppDbContext
	{
		DbSet<Document> Documents { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

