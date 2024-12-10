﻿using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.GS.DPSHost.AppConfiguration;

namespace Microsoft.GS.DPSHost.AppConfiguration
{
    public class AppConfiguration
    {
        public static void Config(IHostApplicationBuilder builder)
        {
            //Read ServiceConfiguration files - appsettings.json / appsettings.Development.json
            //builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);


            //Read AppConfiguration with managed Identity
            if (Environment.GetEnvironmentVariable("APP_CONFIG_ENDPOINT") != null)
            {
                builder.Configuration.AddAzureAppConfiguration(options =>
                {
                    options.Connect(new Uri(Environment.GetEnvironmentVariable("APP_CONFIG_ENDPOINT")), new DefaultAzureCredential());
                });
            }
            else
            {
                Console.WriteLine("APP_CONFIG_ENDPOINT is not set");
            }

            //Read ServiceConfiguration
            builder.Services.Configure<AIServices>(builder.Configuration.GetSection("Application:AIServices"));
            builder.Services.Configure<Services>(builder.Configuration.GetSection("Application:Services"));
        }


    }
}
