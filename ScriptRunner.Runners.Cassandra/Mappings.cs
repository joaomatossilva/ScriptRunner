using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner.Runners.Cassandra
{
    using Common.Data;
    using global::Cassandra.Mapping;

    public class ScriptMappings : Mappings
    {
        public ScriptMappings()
        {
            For<ScriptInfo>()
                .TableName(CassandraRunner.ScriptsTable)
                .PartitionKey(x => x.Id, x => x.Name, x => x.ExecutionDate);
        }
    }
}
