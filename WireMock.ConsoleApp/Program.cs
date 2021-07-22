using System;
using System.Text;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMock.ConsoleApp
{
    class Foo
    {
        public int Prop1 { get; set; }

        public string Prop2 { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var wiremock = WireMockServer.Start(5000,true);

            wiremock.Given(Request.Create().WithPath("/demo").UsingGet())
                .RespondWith(Response.Create().WithBodyAsJson(new Foo { Prop1 = 1, Prop2 = nameof(Foo) }, Encoding.UTF8, true));
            
            Console.WriteLine($"Server started at {wiremock.Urls[0]}");
            
            Console.ReadKey();
            wiremock.Stop();
            wiremock.Dispose();
        }
    }
}
