using System;
namespace DocumentAssistant.App.Interfaces
{
	public interface IDocumentRecognizer
	{
		Task Recognize(string path);
	}
}

