using System;

namespace Commands
{
    [Serializable]
    public class Command
    {
        internal long executionTimeInMillis;
        internal virtual void Execute()
        {
            throw new NotImplementedException();
        }

        internal virtual void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
