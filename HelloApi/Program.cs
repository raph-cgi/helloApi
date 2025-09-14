using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddTransient<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider, Microsoft.AspNetCore.Mvc.ApiExplorer.DefaultApiVersionDescriptionProvider>();
                });
                webBuilder.UseStartup<Startup>();
            });
}