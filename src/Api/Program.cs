using Microsoft.Extensions.FileProviders;
using Presentation.Common.Extensions;
using Serilog;

public class ApplicationInstaller
{
    public static Task<WebApplicationBuilder> InstallApplicationAsync(string[] args, Action<IServiceCollection> options)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        options.Invoke(builder.Services);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        return Task.FromResult(builder);
    }

    public static Task RunAsync(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();

        app.UseExceptionHanlingMiddleware();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        var contentPath = app.Environment.IsDevelopment()
            ? Path.Combine(Directory.GetCurrentDirectory(), "../Api", "wwwroot")
            : Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "_content", "Api");

       app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(contentPath),
        });

        return app.RunAsync();
    }
}


