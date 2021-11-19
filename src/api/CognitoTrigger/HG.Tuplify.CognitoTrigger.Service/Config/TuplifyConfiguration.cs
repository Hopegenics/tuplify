using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HG.Tuplify.CognitoTrigger.Service.Config
{
    internal static class TuplifyConfiguration
    {
        private static IConfiguration _configuration;

        internal static void ConfigureSettings()
        {
            if (_configuration != null)
            {
                return;
            }

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.secrets.json", optional: true)
                .Build();
        }

        public static T Get<T>(string key) where T : class
        {
            if(_configuration == null)
            {
                throw new ArgumentNullException(key);
            }

            return _configuration.GetValue<T>(key);
        }

        public static string GetConnectionString()
        {
            if (_configuration == null)
            {
                throw new ArgumentNullException(nameof(_configuration));
            }

            var server = _configuration.GetValue<string>("TuplifyDbSettings:Server");
            var port = _configuration.GetValue<string>("TuplifyDbSettings:Port");
            var database = _configuration.GetValue<string>("TuplifyDbSettings:Database");
            var user = _configuration.GetValue<string>("TuplifyDbSettings:User");
            var password = _configuration.GetValue<string>("TuplifyDbSettings:Password");

            return $"Server={server};port={port};Database={database};User={user};Password={password}";
        }
    }
}
