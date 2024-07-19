using System;
using Application.Common.Interfaces;
using DocumentAssistant.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocumentAssistant.App
{
    public class Assistant : IAssistant
    {
        private IDocumentRecognizer _documentRecognizer;
        private FileSystemWatcher watcher = new FileSystemWatcher(@"/Users/jadeoye/Downloads");

        private List<string> TrackedFiles = new();
        private List<string> AllowedExtensions = new List<string> { ".pdf", ".png", ".jpg" };

        public Assistant(IDocumentRecognizer documentRecognizer)
        {
            _documentRecognizer = documentRecognizer;

            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.Created += FileCreated;
            watcher.Error += Error;
            watcher.Deleted += FileRemoved;
        }

        private void FileRemoved(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File Removed - " + e.FullPath);
        }

        public void Run()
        {
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Listening for file changes in " + watcher.Path);
            Console.ReadKey();
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            if (!AllowedExtensions.Contains(Path.GetExtension(e.FullPath)))
                Console.WriteLine("File extension not supported yet");
            else
            {
                TrackedFiles.Add(e.FullPath);
                Task.Run(() => _documentRecognizer.Recognize(e.FullPath));
            }
        }

        private void Error(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

