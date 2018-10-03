namespace ScriptRunner
{
    using System;
    using System.IO;
    using System.Linq;
    using Common;
    using Common.Data;
    using Microsoft.Extensions.Configuration;

    public class Engine
    {
        private readonly IRunner runner;
        private readonly IMessageWriter messageWriter;
        private readonly IConfiguration configuration;

        public Engine(IRunner runner, IMessageWriter messageWriter, IConfiguration configuration)
        {
            this.runner = runner;
            this.messageWriter = messageWriter;
            this.configuration = configuration;
        }

        public void Run(string basePath)
        {
            messageWriter.Write("Reading current state");
            var executedScripts = runner.GetExecuted().ToDictionary(x => x.Name);

            var monitorPath = Path.Combine(basePath, GetMonitorPath());
            messageWriter.Write($"Reading path {monitorPath}");
            var files = Directory.GetFiles(monitorPath)
                .OrderBy(x => x);

            foreach (var file in files)
            {
                var scriptName = Path.GetFileNameWithoutExtension(file);
                if (executedScripts.ContainsKey(scriptName))
                {
                    continue;
                }

                messageWriter.Write($"Processing {scriptName}");
                var scriptInfo = new ScriptInfo
                {
                    Id = Guid.NewGuid(),
                    ExecutionDate = DateTimeOffset.UtcNow,
                    Name = scriptName
                };

                //Assuming the files are not specially huge in size
                var data = File.ReadAllText(file);
                runner.Execute(scriptInfo, data);
            }
        }

        private string GetMonitorPath()
        {
            //TODO: Refactor this
            return "deploy\\CQLScripts";
        }
    }
}
