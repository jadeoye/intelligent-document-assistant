using System;
using Application.Interfaces;
using DocumentAssistant.Interfaces;

namespace DocumentAssistant.Utilities.Listeners
{
	public class MicrophoneListener : IMicrophoneListener
	{
        private readonly ISpeechRecognizer _speechRecognizer;
        private readonly ISearcher _searcher;

		public MicrophoneListener(ISpeechRecognizer speechRecognizer, ISearcher searcher)
		{
            _searcher = searcher;
            _speechRecognizer = speechRecognizer;
		}

        public void Listen()
        {
            Console.WriteLine("Press Enter to start listening ...");

            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Listening ...");

                var command = _speechRecognizer.RecognizeAsync().Result?.Trim();

                if (!string.IsNullOrEmpty(command))
                    _searcher.Search(command);
                else
                    Console.WriteLine("Please check for Microphone Permissions");
            }
            else
                Console.WriteLine("Exiting ...");

            Console.ReadKey();
        }
    }
}

