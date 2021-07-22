using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Api.Clients;
using WireMock.Server;

namespace WireMock.IntegrationTests
{
    public class IntegrationTestsApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var wireMock = WireMockServer.Start(5000,true);

            builder
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddInMemoryCollection(new KeyValuePair<string, string>[]
                    {
                        new("JsonPlaceHolder:BaseAddress", wireMock.Urls[0])
                    });
                })
                .ConfigureServices(collection => collection
                    .AddSingleton(wireMock)
                    .AddTransient<IToDoClient, ToDoClient>()
                    .AddHttpClient("JsonPlaceHolder", client => client.BaseAddress = new(wireMock.Urls[0])));
        }
    }
}