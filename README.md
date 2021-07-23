
# Integration Tests with WireMock.NET

A sample project for test external API calls within a dotnet 5 project using WireMock.

[WireMock](https://github.com/WireMock-Net/WireMock.Net) its a library for easly mocking external API's. So I've made a proof of concept on how to use it, so when our microservices has to make an external HTTP call, we can have integrated tests for this features.
Please, remember that this tool is useful to test external API that are NOT developed for us, we shouldn't have microservices in our ecosystem comunicating through HTTP calls.

## Console App Test Sample

### Code

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
        var foo = new Foo
        {
            Prop1 = 1,
            Prop2 = nameof(Foo)
        };

        var wireMockServer = WireMockServer.Start(port: 5000, ssl: true);

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
```

### Request

```bash
curl http://localhost:5000/demo
```

### Response

```json
{
  "Prop1": 1,
  "Prop2": "Foo"
}
```
