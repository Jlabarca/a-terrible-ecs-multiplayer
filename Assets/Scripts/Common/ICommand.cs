using Lockstep.Core.Logic.Serialization;
using Server.Common;

namespace Actors.Command
{
    /**
     * Commands are serializable to b
     */
    public interface ICommand : ISerializable
    {
        CommandTag Tag { get; }
    }
}
