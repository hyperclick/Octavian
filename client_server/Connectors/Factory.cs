using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Server
{
    public static class Factory
    {
        private static IServiceProvider serviceProvider;

        static Factory()
        {
            var services = new ServiceCollection();
            services.AddTransient<HttpConnector>();
            serviceProvider = services.AddHttpClient().BuildServiceProvider();
            services.AddSingleton(serviceProvider);

        }
        public static IServer Create()
        {
            return serviceProvider
                .GetService<HttpConnector>()
                .Connect(GetClientId(), "localhost", 5001);
        }
        private static string GetClientId()
        {
            var process = System.Diagnostics.Process.GetCurrentProcess();

            return $"{Environment.MachineName}_{process.Id}_{process.StartTime}";
        }
    }
}
