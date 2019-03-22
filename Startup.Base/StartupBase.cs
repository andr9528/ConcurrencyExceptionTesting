using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.EntityFramework;
using System;

namespace Startup.Base
{
    public class StartupBase : IStartupEF
    {
        private IStartupEF _startupEf = null;

        public StartupBase(IConfiguration configuration, string connectionStringName = "")
        {
            Configuration = configuration;

            if (connectionStringName != "")
            {
                _startupEf = new StartupEf(Configuration, connectionStringName); 
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_startupEf != null)
            {
                _startupEf.ConfigureServices(services); 
            }
        }
    }
}
