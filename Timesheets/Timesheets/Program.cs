using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Timesheets
{
    public class Program
    {
        public static void Main(string[] args)
        {
<<<<<<< HEAD
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch
            {
                logger.Error("Error in main");
            }
=======
            CreateHostBuilder(args).Build().Run();
>>>>>>> parent of 5f3582c (пофиксил недочеты)
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
<<<<<<< HEAD
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logger =>
                {
                    logger.SetMinimumLevel(LogLevel.Trace);
                    logger.ClearProviders();
                })
                .UseNLog();
=======
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
>>>>>>> parent of 5f3582c (пофиксил недочеты)
    }
}