using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Server
{
    public class HttpConnector : IConnector
    {
        const string HostPropertyName = "host";
        const string PortPropertyName = "port";

        private readonly IServiceProvider serviceProvider;

        public HttpConnector(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public IServer Connect(string client_id, string host, int port)
        {
            ValidateInputs(client_id, host, port);
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri( $"https://{host}:{port}/api/");
            return new HttpServer(client_id, client);
        }

        static private void ValidateInputs(string client_id, string host, int port)
        {

            if (string.IsNullOrEmpty(client_id))
            {
                throw new ArgumentNullException(nameof(client_id));
            }
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }
            if (port < 100)
            {
                throw new ArgumentOutOfRangeException(nameof(port));
            }
        }

        public IServer Connect(string client_id, Dictionary<string, string> properties)
        {
            ValidateInputs(client_id, properties);
            return Connect(client_id, properties[HostPropertyName], int.Parse(properties[PortPropertyName]));
        }

        private static void ValidateInputs(string client_id, Dictionary<string, string> properties)
        {
            if (properties == null || properties.Keys.Count == 0)
            {
                throw new System.ArgumentNullException(nameof(properties));
            }
            if (!properties.ContainsKey(HostPropertyName))
            {
                throw new System.ArgumentNullException(nameof(HostPropertyName));
            }
            if (!properties.ContainsKey(PortPropertyName))
            {
                throw new System.ArgumentNullException(nameof(PortPropertyName));
            }
            ValidateInputs(client_id, properties[HostPropertyName], int.Parse(properties[PortPropertyName]));
        }
    }
}