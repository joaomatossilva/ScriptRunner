using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner
{
    using Common;

    public class ConsoleWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteError(string message, Exception ex = null)
        {
            Console.WriteLine(message);
            if (ex != null)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
