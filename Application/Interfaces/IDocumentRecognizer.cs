using System;
namespace Application.Interfaces
{
	public interface IDocumentRecognizer
	{
		Task RecognizeAsync(string path);
	}
}

