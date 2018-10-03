using System;

namespace ScriptRunner.Runners.Cassandra
{
    using System.Collections.Generic;
    using Common;
    using Common.Data;
    using global::Cassandra;
    using global::Cassandra.Mapping;
    using Microsoft.Extensions.Configuration;

    //TODO: This class needs serious work.

    public class CassandraRunner : IRunner
    {
        public const string ScriptsTable = "sr_executed_scripts";
        private readonly IConfiguration configuration;
        private readonly StatementsParser statementsParser;

        private ISession session;
        private IMapper mapper;

        public CassandraRunner(IConfiguration configuration)
        {
            this.configuration = configuration;
            MappingConfiguration.Global.Define<ScriptMappings>();
            statementsParser = new StatementsParser();
        }

        public void Init()
        {
            var options = configuration.GetSection("CassandraConfiguration").Get<CassandraOptions>();

            var cluster = Cluster.Builder()
                .AddContactPoints(options.ContactPoints)
                .Build();

            session = cluster.Connect(options.Keyspace);
            mapper = new Mapper(session);
            CreateTableIfDoesntExist();
        }

        public IEnumerable<ScriptInfo> GetExecuted()
        {
            return mapper.Fetch<ScriptInfo>();
        }

        public void Execute(ScriptInfo scriptInfo, string data)
        {
            foreach (var statement in statementsParser.Parse(data))
            {
                session.Execute(statement);
            }
            mapper.Insert(scriptInfo);
        }

        private void CreateTableIfDoesntExist()
        {
            session.Execute(createTableTemplate.Replace("#tableName#", ScriptsTable));
        }

        private const string createTableTemplate = @"CREATE TABLE IF NOT EXISTS #tableName#(
	id UUID,
    name TEXT,
    ExecutionDate TIMESTAMP,
    PRIMARY KEY ((id, name, executiondate))
);";
    }
}
