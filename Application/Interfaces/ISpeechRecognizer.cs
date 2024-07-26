using System;
namespace Application.Interfaces
{
	public interface ISpeechRecognizer
	{
		Task<string> RecognizeAsync();
	}
}

