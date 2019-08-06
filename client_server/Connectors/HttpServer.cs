using System;
using System.IO;
using System.Net.Http;

namespace Server
{
    internal class HttpServer : IServer
    {
        private readonly string client_id;
        private readonly HttpClient client;

        public HttpServer(string client_id, HttpClient client)
        {
            this.client_id = client_id;
            this.client = client;
        }

        public void Send(string data)
        {
            using (var request = MakeRequest(data))
            {
                Console.WriteLine($"{client_id}: sending {request.Method} request: {client.BaseAddress}/{request.RequestUri}");
                using (var response = client.SendAsync(request).GetAwaiter().GetResult())
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        public void SendFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
            using (var content = new StreamContent(File.OpenRead(path)))
            {
                using (var response = client.PostAsync($"send_file?client_id={client_id}", content).GetAwaiter().GetResult())
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        private HttpRequestMessage MakeRequest(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new System.ArgumentNullException(nameof(data));
            }
            return new HttpRequestMessage(HttpMethod.Get, $"send?client_id={client_id}&data={data}");
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}