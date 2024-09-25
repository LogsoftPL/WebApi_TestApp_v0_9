using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wms24.Web.Api.TestApp_v0_9.Models;

namespace Wms24.Web.Api.TestApp_v0_9
{
    public class Program
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        public static AppSettings AppSettings { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception has occured when starting an app: {ex.Message}");
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   var configuration = hostContext.Configuration;

                   AppSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
                   if (!(Uri.TryCreate(AppSettings.API_BASEURL, UriKind.Absolute, out Uri outUri) && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps)))
                   {
                       throw new Exception("Wrong uri format.");
                   }
                   HttpClient.BaseAddress = new Uri(AppSettings.API_BASEURL);

                   services.AddSingleton<IConfiguration>(configuration);
                   services.AddHostedService<App>();
               });
        }
    }
}