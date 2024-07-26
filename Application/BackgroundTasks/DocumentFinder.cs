using System;
using Application.Concretes;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.BackgroundTasks
{
	public class DocumentFinder : IDocumentFinder
	{
		private readonly IReadOnlyAppDbContext _rawContext;

		public DocumentFinder(IReadOnlyAppDbContext rawContext)
		{
			_rawContext = rawContext;
		}

        public async Task<List<DocumentDTO>?> FindAsync(RecognizedIntent recognizedIntent)
        {
			try
			{
				string sql = BuildSqlQuery(recognizedIntent);

				var documents = await _rawContext.QueryMultipleAsync<Document>(sql, new
				{
					DocumentType = (int)recognizedIntent.DocumentType,
					recognizedIntent.Name
				});

				return documents?.Select(x => new DocumentDTO
				{
					Data = x.Data,
					Name = x.Name,
					Url = x.Url,
					Type = x.Type
				}).ToList();

			}
			catch (Exception ex)
			{
				throw;
			}
        }

		private string BuildSqlQuery(RecognizedIntent recognizedIntent)
		{
			string sql = "SELECT * FROM Documents WHERE Type = @DocumentType ";

			if (recognizedIntent.NameType.Equals("first name", StringComparison.OrdinalIgnoreCase))
				sql += "AND JSON_VALUE(Data, '$.FirstName') LIKE @Name ";

            if (recognizedIntent.NameType.Equals("last name", StringComparison.OrdinalIgnoreCase))
                sql += "AND JSON_VALUE(Data, '$.LastName') LIKE @Name ";

			return sql;
        }

    }
}

