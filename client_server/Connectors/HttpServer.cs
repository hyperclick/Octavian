using System;
using System.IO;
using System.Net.Http;

namespace Server
{
    internal class HttpServer : IServer
    {
        private string client_id;
        private HttpClient client;

        public HttpServer(string client_id, HttpClient client)
        {
            this.client_id = client_id;
            this.client = client;
        }
        public void Send(string data)
        {
            var request = MakeRequest(data);
            Console.WriteLine($"{client_id}: sending {request.Method} request: {client.BaseAddress}/{request.RequestUri}");
            var t = client.SendAsync(request);
            var response = t.GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
        }

        public void SendFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
            var content = new System.Net.Http.StreamContent(File.OpenRead(path));
            var response = client.PostAsync($"send_file?client_id={client_id}", content).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
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
    }
}