using System;
using Application.Concretes;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface IDocumentFinder
	{
		Task<List<DocumentDTO>?> FindAsync(RecognizedIntent recognizedIntent);
	}
}

