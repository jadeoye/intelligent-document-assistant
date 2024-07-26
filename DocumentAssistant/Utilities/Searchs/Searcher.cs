using System;
using Application.Interfaces;
using DocumentAssistant.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DocumentAssistant.Utilities.Searchs
{
	public class Searcher : ISearcher
	{
        private readonly IIntentRecognizer _intentRecognizer;
        private readonly IDocumentFinder _documentFinder;

        public Searcher(IIntentRecognizer intentRecognizer, IDocumentFinder documentFinder)
		{
            _documentFinder = documentFinder;
            _intentRecognizer = intentRecognizer;
		}

        public async Task Search(string command)
        {
            var recognizedIntent = await _intentRecognizer.RecognizeAsync(command);
            var documents = await _documentFinder.FindAsync(recognizedIntent);

            if (documents is null || documents.Count == 0)
                Console.WriteLine("Cannot find matching documents for search.");
            else
            {
                foreach (var document in documents)
                {
                    Console.WriteLine("-------------------------------------\n");
                    Console.WriteLine($"Document Name: {document.Name}");
                    Console.WriteLine($"Document Type: {document.Type}");
                    Console.WriteLine($"Document Url: {document.Url}");

                    JObject parsedJson = JObject.Parse(document.Data);
                    string formattedJson = parsedJson.ToString((Newtonsoft.Json.Formatting)Formatting.Indented);

                    Console.WriteLine(formattedJson);
                }
            }

            Console.WriteLine("Press any key to continue...");
        }
    }
}

