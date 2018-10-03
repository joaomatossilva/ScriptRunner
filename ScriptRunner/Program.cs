using System;

namespace ScriptRunner
{
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Runners.Cassandra;

    class Program
    {
        static void Main(string[] args)
        {
            string path = ".";
            if (args.Length > 0)
            {
                path = args[0];
            }

            var appSettings = Directory.GetFiles(path, "appsettings.json", SearchOption.AllDirectories)
                .FirstOrDefault();

            var baseFolder = Path.GetDirectoryName(appSettings);

            var builder = new ConfigurationBuilder()
                .SetBasePath(baseFolder)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var writer = new ConsoleWriter();
            writer.Write("Hello.. Shall we begin?");

            var runner = new CassandraRunner(configuration);
            try
            {
                writer.Write("Initializing...");
                runner.Init();
            }
            catch (Exception ex)
            {
                writer.WriteError("Error While initializing runner", ex);
                return;
            }

            var engine = new Engine(runner, writer, configuration);
            try
            {
                writer.Write("Starting our engine");
                engine.Run(path);
            }
            catch (Exception ex)
            {
                writer.WriteError("Error on engine", ex);
                return;
            }
        }
    }
}
