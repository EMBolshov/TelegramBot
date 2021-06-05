using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot.Api.Models;
using TelegramBot.Api.Services;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddSingleton<IBotDbRepository>(_ => new BotDbRepository(Configuration.GetConnectionString("BotDbContext")))
                .Configure<BotSettings.Options>(Configuration.GetSection(BotSettings.Options.Name))
                .AddSingleton<BotSettings>()
                .AddSingleton<IBotService, BotService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}