using System;
using Application.Concretes;

namespace Application.Interfaces
{
	public interface IIntentRecognizer
	{
		Task<RecognizedIntent> RecognizeAsync(string speech);
	}
}

