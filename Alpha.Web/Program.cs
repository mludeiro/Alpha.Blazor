using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Alpha.Web;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddControllers();
        builder.Services.AddOcelot();
        builder.Configuration.AddJsonFile("ocelot.json");

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
        app.UseAuthorization();

        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.UseOcelot();

        app.Run();
    }
}