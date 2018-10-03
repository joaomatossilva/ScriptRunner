using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner.Common.Data
{
    public class ScriptInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset ExecutionDate { get; set; }
    }
}
