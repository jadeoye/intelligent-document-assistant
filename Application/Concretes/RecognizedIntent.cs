using System;
using Domain.Enums;

namespace Application.Concretes
{
	public class RecognizedIntent
	{
		public DocumentType? DocumentType { get; set; }
		public string? NameType { get; set; }
		public string? Name { get; set; }
	}
}

