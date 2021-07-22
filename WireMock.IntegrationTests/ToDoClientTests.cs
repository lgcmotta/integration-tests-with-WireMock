using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Api;
using WireMock.Api.Clients;
using WireMock.Api.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace WireMock.IntegrationTests
{
    public class ToDoClientTests : IClassFixture<IntegrationTestsApplicationFactory<Startup>>
    {
        private readonly IntegrationTestsApplicationFactory<Startup> _factory;
        private readonly Fixture _fixture = new();
        private readonly WireMockServer _wireMockServer;

        public ToDoClientTests(IntegrationTestsApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _wireMockServer = _factory.Services.GetRequiredService<WireMockServer>();
        }
        
        [Fact]
        public async Task GetToDo_Success()
        {
            var toDo = _fixture.Build<ToDo>()
                .With(toDoFixture => toDoFixture.Id, 1)
                .With(toDoFixture => toDoFixture.UserId, 1)
                .With(toDoFixture => toDoFixture.Title, "Title")
                .With(toDoFixture => toDoFixture.Completed, false)
                .Create();

            var toDoClient = _factory.Services.GetRequiredService<IToDoClient>();

            _wireMockServer.Given(Request.Create()
                    .WithPath("/todos/1"))
                .RespondWith(Response.Create()
                    .WithBodyAsJson(toDo));

            var toDoResponse = await toDoClient.GetToDo(1);

            toDoResponse.Should().BeEquivalentTo(toDo);
        }

        [Fact]
        public async Task GetToDo_HttpClient_Success()
        {
            var toDo = _fixture.Build<ToDo>()
                .With(toDoFixture => toDoFixture.Id, 1)
                .With(toDoFixture => toDoFixture.UserId, 1)
                .With(toDoFixture => toDoFixture.Title, "Title")
                .With(toDoFixture => toDoFixture.Completed, false)
                .Create();

            var httpClientFactory = _factory.Services.GetRequiredService<IHttpClientFactory>();

            var httpClient = httpClientFactory.CreateClient("JsonPlaceHolder");
            
            _wireMockServer.Given(Request.Create()
                    .WithPath("/todos/1"))
                .RespondWith(Response.Create()
                    .WithBodyAsJson(toDo)
                    .WithStatusCode(HttpStatusCode.OK));

            var response = await httpClient.GetAsync("/todos/1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseToDo = await response.Content.ReadFromJsonAsync<ToDo>();

            responseToDo.Should().BeEquivalentTo(toDo);
        }

        [Fact]
        public void GetToDos_Success()
        {
            var toDo = _fixture.Build<IEnumerable<ToDo>>().Create();
        }
    }
}
