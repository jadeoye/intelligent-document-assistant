using System;
using Domain.Enums;

namespace Domain.Models.Base
{
	public abstract class DocumentModel
	{
		public DocumentType DocumentType { get; set; }
    }
}

