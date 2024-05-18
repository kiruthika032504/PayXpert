using Microsoft.Extensions.Configuration;
using PayXpert.exception;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.util
{
    public class DbConnUtil
    {
        private static IConfiguration configuration;

        //Create a Constructor
        static DbConnUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                configuration = builder.Build();
            }
            catch(DatabaseConnectionException dae)
            {
                Console.WriteLine($"Database connection failed. {dae.Message}");
            }
        }

        public static string GetConnectionString()
        {
            return configuration.GetConnectionString("LocalConnectionString");
        }
    }
}
