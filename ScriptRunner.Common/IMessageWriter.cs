using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner.Common
{
    public interface IMessageWriter
    {
        void Write(string message);
        void WriteError(string message, Exception ex = null);
    }
}
