using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace API.Tests.Middleware
{
    public class ApiKeyMiddlewareTests
    {
        private TestServer CreateServer()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services => { })
                .Configure(app =>
                {
                    app.UseMiddleware<ApiKeyMiddleware>();
                    app.Run(async context => { await context.Response.WriteAsync("OK"); });
                });
            return new TestServer(builder);
        }

        [Fact]
        public async Task Retorna401_SeApiKeyAusente()
        {
            using var server = CreateServer();
            var client = server.CreateClient();
            var response = await client.GetAsync("/calculadora/teste");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("API Key não fornecida", content);
        }

        [Fact]
        public async Task Retorna401_SeApiKeyVazia()
        {
            using var server = CreateServer();
            var client = server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/calculadora/teste");
            request.Headers.Add("API-Key", "");
            var response = await client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains(content, new[] { "API Key inválida", "API Key não fornecida" });
        }

        [Fact]
        public async Task Retorna401_SeApiKeyInvalida()
        {
            using var server = CreateServer();
            var client = server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/calculadora/teste");
            request.Headers.Add("API-Key", "Invalida");
            var response = await client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("API Key inválida", content);
        }

        [Fact]
        public async Task PermiteAcesso_SeApiKeyValida()
        {
            using var server = CreateServer();
            var client = server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/calculadora/teste");
            request.Headers.Add("API-Key", "Genesis-API-Key-2025");
            var response = await client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("OK", content);
        }

        [Fact]
        public async Task NaoBloqueiaRotaForaCalculadora()
        {
            using var server = CreateServer();
            var client = server.CreateClient();
            var response = await client.GetAsync("/outra-rota");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("OK", content);
        }
    }
} 