namespace ScriptRunner.Common
{
    using Data;
    using System.Collections.Generic;

    public interface IRunner
    {
        void Init();

        IEnumerable<ScriptInfo> GetExecuted();

        void Execute(ScriptInfo scriptInfo, string data);
    }
}
