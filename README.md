# integration-tests-with-WireMock
POC of integration testing with dotnet 5 and WireMock

[WireMock](https://github.com/WireMock-Net/WireMock.Net) its a library for easly mocking external API's. So I've made a proof of concept on how to use it, so when our microservices has to make an external HTTP call, we can have integrated tests for this features.
Please, remember that this tool is useful to test external API that are NOT developed for us, we shouldn't have microservices in our ecosystem comunicating through HTTP calls.


## Basic Usage

```CShrap
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
```
