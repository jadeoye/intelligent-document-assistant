using System;
using Application.Common.Interfaces;
using DocumentAssistant.App.Interfaces;
using DocumentAssistant.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocumentAssistant.App
{
    public class Assistant : IAssistant
    {
        private IIntentRecognizer _intentRecognizer;
        private IDocumentRecognizer _documentRecognizer;
        private FileSystemWatcher watcher = new FileSystemWatcher(@"/Users/jadeoye/Downloads/identity-documents");

        private List<string> TrackedFiles = new();
        private List<string> AllowedExtensions = new List<string> { ".pdf", ".png", ".jpg" };

        public Assistant(IDocumentRecognizer documentRecognizer, IIntentRecognizer intentRecognizer)
        {
            _intentRecognizer = intentRecognizer;
            _documentRecognizer = documentRecognizer;

            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.Created += FileCreated;
            watcher.Error += Error;
            watcher.Deleted += FileRemoved;
        }

        private void FileRemoved(object sender, FileSystemEventArgs e)
        {

        }

        public void Run()
        {
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Listening for file changes in " + watcher.Path);

            var recognizedIntent = Task.Run(async () =>
            await _intentRecognizer.RecognizeAsync(speech: "Get me all passports having the first name as Jeremiah"));

            Console.ReadKey();
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            if (!AllowedExtensions.Contains(Path.GetExtension(e.FullPath)))
                Console.WriteLine("File extension not supported yet");
            else
            {
                TrackedFiles.Add(e.FullPath);
                Task.Run(() => _documentRecognizer.RecognizeAsync(e.FullPath));
            }
        }

        private void Error(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

