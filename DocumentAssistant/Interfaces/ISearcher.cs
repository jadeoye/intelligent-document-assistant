using System;
namespace DocumentAssistant.Interfaces
{
	public interface ISearcher
	{
		Task Search(string command);
	}
}

