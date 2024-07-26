using System;
using Application.Interfaces;
using DocumentAssistant.App.Interfaces;
using DocumentAssistant.Interfaces;

namespace DocumentAssistant.App
{
    public class Assistant : IAssistant
    {
       
        private readonly IDocumentRecognizer _documentRecognizer;
        private readonly IMicrophoneListener _microphoneListener;
        private readonly IKeyboardListener _keyboardListener;

        private FileSystemWatcher watcher = new FileSystemWatcher(@"/Users/jadeoye/Downloads/identity-documents");

        private List<string> TrackedFiles = new();
        private List<string> AllowedExtensions = new List<string> { ".pdf", ".png", ".jpg" };

        public Assistant(IDocumentRecognizer documentRecognizer, ISpeechRecognizer speechRecognizer,
            IKeyboardListener keyboardListener, IMicrophoneListener microphoneListener)
        {
            _documentRecognizer = documentRecognizer;
            _microphoneListener = microphoneListener;
            _keyboardListener = keyboardListener;

            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.Created += FileCreated;

            watcher.EnableRaisingEvents = true;
        }

        public void Run()
        {
            Console.WriteLine("Listening for file changes in " + watcher.Path);

            Console.WriteLine("Select an option for Command Input:\n1. Use Microphone\n2. Use Keyboard");

            var commandType = Console.ReadLine()?.Trim();

            if (commandType == "1") _microphoneListener.Listen();
            else if (commandType == "2") _keyboardListener.Listen();
            else Console.WriteLine("Invalid Command Input");
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            if (!AllowedExtensions.Contains(Path.GetExtension(e.FullPath)))
                Console.WriteLine("File extension not supported yet");
            else
            {
                TrackedFiles.Add(e.FullPath);
                Task.Run(async () => await _documentRecognizer.RecognizeAsync(e.FullPath));
            }
        }
    }
}

