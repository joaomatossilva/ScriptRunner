using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner.Runners.Cassandra
{
    public class CassandraOptions
    {
        public string ContactPoints { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool WithSsl { get; set; }

        public string ConsistencyLevel { get; set; }

        public string ReplicationKeys { get; set; }

        public string ReplicationValues { get; set; }

        public string Keyspace { get; set; }

        public int TemporaryInsertTtl { get; set; }
    }
}
