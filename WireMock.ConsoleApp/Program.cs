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
            var foo = new Foo
            {
                Prop1 = 1,
                Prop2 = nameof(Foo)
            };

            var wireMockServer = WireMockServer.Start(port: 5000, ssl: false);
            
            wireMockServer.Given(Request.Create()
                    .WithPath("/demo")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithBodyAsJson(body: foo,
                                    encoding: Encoding.UTF8,
                                    indented: true));
            
            Console.WriteLine($"Server started at {wireMockServer.Urls[0]}");
            
            Console.ReadKey();
            wireMockServer.Stop();
            wireMockServer.Dispose();
        }
    }
}
