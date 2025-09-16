using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using HelloApi;
using HelloApi.Data;
using Microsoft.EntityFrameworkCore;






public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // DI Context
        services.AddDbContext<HelloApiContext>(options => options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

        // DI repository
        services.AddScoped<ITPersonRepository, TPersonRepository>();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader(); // les routes utilisent v{version}
        });


        // Très important pour Swagger multi-versions
        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";                // v1, v2, v2.1, etc.
            o.SubstituteApiVersionInUrl = true;          // remplace {version:apiVersion} dans les routes
        });

        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations(); // Active les attributs Swagger*
        });

        // Ajoute la config Swagger qui génère un doc par version
        services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            foreach (var desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                                        $"HelloApi {desc.GroupName.ToUpperInvariant()}");
            }
            options.RoutePrefix = string.Empty; // Optionnel : SwaggerUI à la racine /
        });
    }


}