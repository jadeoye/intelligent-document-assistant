using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Domain.Converters;
using Domain.Enums;
using Domain.Entities.Models;
using Domain.Models.Base;

namespace Domain.Entities
{
	public class Document
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string Url { get; private set; }
        public DocumentType Type { get; set; }
        public string Data { get; private set; }

		[NotMapped]
		public DocumentModel DocumentModel
		{
			get => JsonSerializer.Deserialize<DocumentModel>(Data, new JsonSerializerOptions { Converters = { new DocumentConverter(Type) } });
			set => Data = JsonSerializer.Serialize(value, new JsonSerializerOptions { Converters = { new DocumentConverter(Type) } });
		}

		public static Document Create(string path, DocumentModel document)
		{
			return new Document
			{
				Name = Path.GetFileName(path),
				Url = path,
				Type = document.Type,
				DocumentModel = document
			};
		}
    }
}

