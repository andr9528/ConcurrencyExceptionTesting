using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.EntityFramework;
using System;
using System.IO;
using System.Text;

namespace Startup.Base
{
    public abstract class StartupBase : IStartupEF
    {
        private IStartupEF _startupEf = null;

        public StartupBase(string connectionStringName = "")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            if (connectionStringName != "")
            {
                _startupEf = new StartupEf(Configuration, connectionStringName); 
            }
        }

        public IConfiguration Configuration { get; protected set; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_startupEf != null)
            {
                _startupEf.ConfigureServices(services); 
            }
        }
    }
}
