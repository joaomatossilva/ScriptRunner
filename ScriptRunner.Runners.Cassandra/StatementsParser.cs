using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner.Runners.Cassandra
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public class StatementsParser
    {
        //TODO: this is ignore the users creation since on local, the authentication is offline
        private static readonly Regex IgnorePatterns = new Regex("CREATE ROLE|GRANT");


        public IEnumerable<string> Parse(string data)
        {
            var statements = Regex.Split(data, @";\W*$", RegexOptions.Multiline);
            return statements.Where(IsValidStatment);
        }

        private bool IsValidStatment(string statement)
        {
            if (string.IsNullOrWhiteSpace(statement))
            {
                return false;
            }

            if (IgnorePatterns.Match(statement).Success)
            {
                return false;
            }

            return true;
        }
    }
}
