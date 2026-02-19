using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Tests;

[TestFixture]
public class ApiIntegrationTests : IDisposable
{
    private WebApplicationFactory<Program> factory;
    private HttpClient client;

    [OneTimeSetUp]
    public void Setup()
    {
        factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
        });
        client = factory.CreateClient();
    }

    [Test]
    public async Task Evaluate_Should_Return_US_For_Sample_Order()
    {
        var order = new
        {
            orderId = "1245101",
            publisherNumber = "99999",
            publisherName = "BookWorld Ltd",
            orderMethod = "POD",
            shipments = new[]
            {
                new
                {
                    shipTo = new { isoCountry = "US" }
                }
            },
            items = new[]
            {
                new
                {
                    sku = "PB-001",
                    printQuantity = 10,
                    components = new[]
                    {
                        new
                        {
                            code = "Cover",
                            attributes = new { bindTypeCode = "PB" }
                        }
                    }
                }
            }
        };

        var json = JsonConvert.SerializeObject(order);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/evaluate", content);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        responseBody.Should().Contain("US");

        Dispose();
    }

    public void Dispose()
    {
        factory.Dispose();
        client.Dispose();
    }
}
