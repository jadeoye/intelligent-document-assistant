using System;
using DocumentAssistant.Interfaces;

namespace DocumentAssistant.Listeners
{
	public class KeyboardListener : IKeyboardListener
	{
        private readonly ISearcher _searcher;

		public KeyboardListener(ISearcher searcher)
		{
            _searcher = searcher;
		}

        public void Listen()
        {
            while (true)
            {
                Console.Write("Enter a filter (Press enter to search): ");

                var command = Console.ReadLine();

                if (!string.IsNullOrEmpty(command))
                    _searcher.Search(command);

                Console.ReadKey();
            }
        }
    }
}

