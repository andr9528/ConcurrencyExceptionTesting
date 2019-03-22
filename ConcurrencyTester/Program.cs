using Microsoft.Extensions.Configuration;
using Startup.Base;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace ConcurrencyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I'm Alive");

            Program Run = new Program();
            Run.Run();

            Console.WriteLine(Environment.NewLine + "Press any key to close...");
            Console.ReadKey();
        }

        private void Run()
        {
            var services = new ServiceCollection();
            var startup = new Startup("Storage");
            startup.ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            var service = provider.GetService<IConcurrencyTesting>();

            service.RunTests();
        }
    }
}
