using System;
using Application.Interfaces;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;

namespace Application.BackgroundTasks
{
	public class SpeechRecognizer : ISpeechRecognizer
	{
        private SpeechConfig _speechConfig;

		public SpeechRecognizer(IConfiguration configuration)
		{
            if (configuration is null)
                throw new ArgumentNullException("Configuration");

            _speechConfig = SpeechConfig.FromSubscription(configuration["SpeechRecognizerApiKey"], configuration["SpeechRecognizerRegion"]);
		}

        public async Task<string> RecognizeAsync()
        {
            try
            {
                string recognizedCommand = string.Empty;
                var recognizedCommandTask = new TaskCompletionSource<string>();

                using (var recognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(_speechConfig))
                {
                    recognizer.Recognized += (s, e) =>
                    {
                        var result = e.Result;

                        if(result.Reason == ResultReason.RecognizedSpeech)
                        {
                            recognizedCommand = result.Text;
                            recognizedCommandTask.TrySetResult(recognizedCommand);
                        }
                    };

                    recognizer.Recognizing += (s, e) =>
                    {
                        recognizedCommand = e.Result.Text;
                    };

                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                    Console.WriteLine("Press Enter to stop ...");

                    while(!recognizedCommandTask.Task.IsCompleted)
                        if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
                            break;

                    await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);

                    return await recognizedCommandTask.Task.ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

