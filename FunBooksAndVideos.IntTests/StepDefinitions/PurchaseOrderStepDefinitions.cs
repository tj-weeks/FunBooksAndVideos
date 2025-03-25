using FunBooksAndVideos.Enums;
using FunBooksAndVideos.OrderItems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;

namespace FunBooksAndVideos.IntTests.StepDefinitions
{
    [Binding]
    public sealed class PurchaseOrderStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;

        public PurchaseOrderStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var host = TestStartup.CreateHostBuilder(new string[] { })
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<TestStartup>();
                })
                .Start();

            _client = host.GetTestClient();
            // _client.BaseAddress = new Uri("https://localhost:7160");
        }

        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://go.reqnroll.net/doc-hooks#tag-scoping

            //TODO: implement logic that has to run before executing each scenario
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            // Example of ordering the execution of hooks
            // See https://go.reqnroll.net/doc-hooks#hook-execution-order

            //TODO: implement logic that has to run before executing each scenario
        }

        [Given("a purchase order")]
        public void GivenAPurchaseOrder()
        {
            var purchaseOrder = new PurchaseOrderItem
            {
                CustomerId = 1,
                Id = 1,
                TotalPrice = 100,
                PurchaseItems = new List<IPurchaseItem>
                {
                    new ProductItem
                    {
                        Id = 1,
                        Name = "Product",
                        Price = 100,
                        ProductType = Product.Type.Book
                    },
                    new MembershipItem
                    {
                        Id = 1,
                        Name = "Membership",
                        Price = 100,
                        MembershipType = Membership.Type.BookClub
                    }
                }
            };

            _scenarioContext.Set(purchaseOrder, "PurchaseOrder");
        }

        [When("the purchase order is processed")]
        public async Task WhenThePurchaseOrderIsProcessed()
        {
            var purchaseOrder = _scenarioContext.Get<PurchaseOrderItem>("PurchaseOrder");
            var jsonContent = new StringContent(JsonSerializer.Serialize(purchaseOrder), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/PurchaseOrder", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            _scenarioContext.Set(responseString, "Response");
        }

        [Then("the response should contain '(.*)'")]
        public void ThenTheResponseShouldContain(string expectedResponse)
        {
            var response = _scenarioContext.Get<string>("Response");
            Assert.Contains(expectedResponse, response);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}