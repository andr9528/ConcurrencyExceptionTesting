using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Startup.Base;

namespace ConcurrencyTester
{
    internal class Startup : StartupBase
    {
        public Startup(string connectionStringName = "Storage") : base(connectionStringName)
        {

        }

        new public void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddTransient<IConcurrencyTesting, ConcurrencyTesting>();
        }
    }
}
