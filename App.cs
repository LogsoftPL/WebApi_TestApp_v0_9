using Bogus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Wms24.CourierLink.Database.Data.ModelsApi;
using Wms24.CourierLink.Database.Data.ModelsApi.Responses;
using Wms24.Web.Api.TestApp_v0_9.Models;

namespace Wms24.Web.Api.TestApp_v0_9
{
    public class App : BackgroundService
    {
        public ApiToken ApiToken { get; set; }
        public Timer RefreshApiTokenTimer { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (ApiToken == null)
                {
                    await LoginAsync();
                    ScheduleRefreshApiToken();
                }

                var owners = await GetOwnersAsync();
                var services = await GetShippingServicesAsync();
                var createdEntities = await CreateOrdersAsync(owners, services);

                if (createdEntities != null && createdEntities.EntryIds.Length > 0)
                {
                    var orders = await GetOrdersAsync(100, 5, DateTime.Now.AddMinutes(-5));

                    foreach (var order in orders)
                    {
                        Console.WriteLine("=======================");
                        Console.WriteLine(order.MarketPlaceDocNr);
                        Console.WriteLine(order.CreationDate);
                        Console.WriteLine("Bought items: ");
                        foreach (var item in order.Items)
                        {
                            Console.WriteLine($"- {item.OrderedQuantity} x {item.ProductName} {item.PriceBrutto} PLN");
                        }
                        Console.WriteLine("=======================");
                    }
                }

                await Task.Delay(5000);
            }
        }

        private void ScheduleRefreshApiToken()
        {
            DateTime now = DateTime.Now;
            DateTime scheduledTime = ApiToken.ValidTo.AddHours(-1);

            if (now > scheduledTime)
            {
                scheduledTime = scheduledTime.AddHours(24);
            }

            TimeSpan timeToGo = scheduledTime - now;

            Console.WriteLine($"Api token will refresh at {scheduledTime}.");

            RefreshApiTokenTimer = new Timer(async x =>
            {
                await LoginAsync();
                ScheduleRefreshApiToken();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private async Task LoginAsync()
        {
            try
            {
                var body = new LoginPost()
                {
                    Email = Program.AppSettings.API_LOGIN,
                    Password = Program.AppSettings.API_PASSWORD,
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await Program.HttpClient.PostAsync("api/v0.9/auth/login", httpContent);

                xResLogin data = (xResLogin)await response.Content.ReadFromJsonAsync(typeof(xResLogin));

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(data.Message);

                    ApiToken = new ApiToken()
                    {
                        Token = data.Token,
                        ValidTo = DateTime.Now.AddHours(24)
                    };

                    Program.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiToken.Token);
                }
                else
                {
                    throw new Exception(data.Message);
                }
            }
            catch
            {
                throw;
            }
        }

        private async Task<List<xOwner>> GetOwnersAsync()
        {
            try
            {
                var response = await Program.HttpClient.GetAsync("api/v0.9/owners");

                if (response.IsSuccessStatusCode)
                {
                    List<xOwner> owners = (List<xOwner>)await response.Content.ReadFromJsonAsync(typeof(List<xOwner>));
                    return owners;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}.");
                }
            }
            catch
            {

                throw;
            }
        }

        private async Task<List<xOrder>> GetOrdersAsync(int orderStatus, int limit, DateTime from)
        {
            try
            {
                var response = await Program.HttpClient.GetAsync($"api/v0.9/orders?limit={limit}&orderStatus={orderStatus}&createdDateFrom={from}");

                if (response.IsSuccessStatusCode)
                {
                    List<xOrder> orders = (List<xOrder>)await response.Content.ReadFromJsonAsync(typeof(List<xOrder>));
                    return orders;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}.");
                }
            }
            catch
            {

                throw;
            }
        }

        private async Task<List<xShippingService>> GetShippingServicesAsync()
        {
            try
            {
                var response = await Program.HttpClient.GetAsync("api/v0.9/services");

                if (response.IsSuccessStatusCode)
                {
                    List<xShippingService> services = (List<xShippingService>)await response.Content.ReadFromJsonAsync(typeof(List<xShippingService>));
                    return services;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}.");
                }
            }
            catch
            {

                throw;
            }
        }

        private async Task<xResNewEntries> CreateOrdersAsync(List<xOwner> owners, List<xShippingService> services)
        {
            try
            {
                //Generate fake data -----------------------------
                Random random = new Random();
                var ownerIndex = random.Next(owners.Count());
                var ownerToken = owners[ownerIndex].Token;

                var serviceIndex = random.Next(services.Count());
                var service = services[serviceIndex];

                var xOrderItemsFaker = new Faker<xOrderItem>()
                                    .RuleFor(x => x.ProductName, f => f.Commerce.ProductName())
                                    .RuleFor(x => x.OrderedQuantity, f => f.Random.Number(1, 5))
                                    .RuleFor(x => x.ProductCode, f => f.Lorem.Word() + f.Random.Number(1, 99))
                                    .RuleFor(x => x.PriceBrutto, f => Double.Parse(f.Commerce.Price(5, 500, 2)))
                                    .RuleFor(x => x.TaxRate, f => 23.00)
                                    .RuleFor(x => x.BoughtDate, f => DateTime.Now.AddSeconds(-f.Random.Number(1, 10000)));

                var xAddressFaker = new Faker<xAddress>()
                                    .RuleFor(x => x.Name, f => f.Person.FullName)
                                    .RuleFor(x => x.City, f => f.Address.City())
                                    .RuleFor(x => x.ZipCode, f => f.Address.ZipCode())
                                    .RuleFor(x => x.Country, f => f.Address.CountryCode())
                                    .RuleFor(x => x.ContactEmail, f => f.Person.Email)
                                    .RuleFor(x => x.ContactPhone, f => f.Person.Phone)
                                    .RuleFor(x => x.Street, f => f.Address.StreetName())
                                    .RuleFor(x => x.BuildingNumber, f => f.Address.BuildingNumber());

                var xOrderFaker = new Faker<xOrderBody>()
                                    .RuleFor(x => x.OwnerToken, ownerToken)
                                    .RuleFor(x => x.Priority, f => f.Random.Number(1, 10))
                                    .RuleFor(x => x.WantInvoice, false)
                                    .RuleFor(x => x.CreationDate, f => DateTime.Now.AddSeconds(-f.Random.Number(1, 10000)))
                                    .RuleFor(x => x.Items, f => xOrderItemsFaker.Generate(f.Random.Number(1, 3)).ToList())
                                    .RuleFor(x => x.ReceiverAddress, f => xAddressFaker.Generate(1).First())
                                    .RuleFor(x => x.MarketPlaceDocNr, f => f.Random.Uuid().ToString())
                                    .RuleFor(x => x.ShippingServiceSourceExternalName, f => service.Name)
                                    .RuleFor(x => x.ShippingServiceExternalSourceID, f => service.CourierService);

                var orders = xOrderFaker.GenerateBetween(1, 3);
                //end generate fake data ------------------------

                var payload = JsonConvert.SerializeObject(orders);
                var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
                var message = await Program.HttpClient.PostAsync("api/v0.9/orders", httpContent);

                xResNewEntries response = (xResNewEntries)await message.Content.ReadFromJsonAsync(typeof(xResNewEntries));
                if (message.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.Message);
                    return response;
                }
                else
                {
                    throw new Exception(response.Message);
                }

            }
            catch
            {

                throw;
            }
        }

    }
}
