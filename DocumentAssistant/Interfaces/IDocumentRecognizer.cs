using System;
namespace DocumentAssistant.App.Interfaces
{
	public interface IDocumentRecognizer
	{
		Task RecognizeAsync(string path);
	}
}

