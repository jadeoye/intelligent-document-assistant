using System;
using DocumentAssistant.Concretes;

namespace DocumentAssistant.Interfaces
{
	public interface IIntentRecognizer
	{
		Task<RecognizedIntent> RecognizeAsync(string speech);
	}
}

