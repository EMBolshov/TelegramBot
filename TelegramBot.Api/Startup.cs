using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using TelegramBot.Api.Models;
using TelegramBot.Api.Services;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //TODO: Do something with this shitty logger configuration
            //IDK why it does not work with GetConnectionString
            string connectionString = "User ID=nahageixgwxfnl;Password=a166c28381eb09c7e144be5bc8225a5b4c9c310677923829b196c63ff93c3761;Host=ec2-52-214-178-113.eu-west-1.compute.amazonaws.com;Port=5432;Database=d3hftda68mlok7;SSL Mode=Require;Trust Server Certificate=true";

            //IDK why it does not work with appSettings
            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {"Id", new IdAutoIncrementColumnWriter()},
                {"Message", new RenderedMessageColumnWriter()},
                {"Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar)},
                {"Timestamp", new TimestampColumnWriter()}
            };

            var logger = new LoggerConfiguration()
                .WriteTo.PostgreSQL(connectionString, "Logs", columnWriters)
                .CreateLogger();

            Configuration = configuration;
            Log.Logger = logger;
            
            //Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddSingleton<IBotDbRepository>(_ =>
                    new BotDbRepository(Configuration.GetConnectionString("BotDbContext")))
                .Configure<BotSettings.Options>(Configuration.GetSection(BotSettings.Options.Name))
                .AddSingleton<BotSettings>()
                .AddSingleton<IBotService, BotService>()
                .AddLogging(lb => lb.AddSerilog( dispose: true));
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