using System;
namespace Infrastructure.Configurations.Utilities
{
	public class ReadOnlyDbConfiguration
	{
        public string ConnectionString { get; }

        public ReadOnlyDbConfiguration(string value)
        {
            ConnectionString = value;
        }
    }
}

