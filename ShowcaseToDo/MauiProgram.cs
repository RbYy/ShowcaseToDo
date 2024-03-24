using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using ShowCaseToDo.Models;
using ShowCaseToDo.Services;
using ShowCaseToDo.Services.DataBaseService;

namespace ShowCaseToDo
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			builder.Services.AddMauiBlazorWebView();
			builder.Services.AddFluentUIComponents();

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif
         /*+************************************************/
         // Use sqlite db to store data
            ConfigureServices(builder.Services);

         // Use json file to store data
            //builder.Services.AddScoped<IDataAccessService<Item>, FileStorage<Item>>();
         /*+************************************************/

            return builder.Build();
		}

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext<Item>>(options =>
                options.UseSqlite($"Data Source={Path.Combine(FileSystem.AppDataDirectory, "YourAppDatabase.db3")}"));

            // Resolve the DbContext and ensure the database is created
            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext<Item>>();
                context.EnsureDatabaseCreated();
            }
            services.AddScoped<IDataAccessService<Item>, SqlDataAccessService<Item>>();
        }
    }
}
