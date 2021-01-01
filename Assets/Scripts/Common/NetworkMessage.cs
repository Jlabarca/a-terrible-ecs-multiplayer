using System;
using System.Collections.Generic;
using System.Linq;
using Actors.Command;
using Actors.Command.Components;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using Server.Common;

namespace Actors.Online
{
    public class NetworkMessage
    {
        public uint Tick { get; }
        public uint PlayTick { get; }
        public byte ActorId { get; }
        public List<ICommand> Commands { get; }

        public NetworkMessage(uint tick)
        {
            Tick = tick;
            ActorId = 0;
            Commands = new List<ICommand>();
        }

        public NetworkMessage(uint tick, byte actorId, List<ICommand> commands)
        {
            Tick = tick;
            ActorId = actorId;
            Commands = commands;
        }

        public NetworkMessage(uint tick, uint playTick, byte actorId, List<ICommand> commands)
        {
            Tick = tick;
            PlayTick = playTick;
            ActorId = actorId;
            Commands = commands;
        }

        public override string ToString()
        {
            return $"{ActorId}  >>  {Tick} | {PlayTick}: {Commands.GetType().FullName}";
        }

        public byte[] ToBytes()
        {
            var writer = new Serializer();
            writer.Put((byte)MessageTag.Input);
            writer.Put(Tick);
            writer.Put(PlayTick);
            writer.Put(Commands.Count());
            writer.Put(ActorId);
            foreach (var command in Commands)
            {
                writer.Put((byte) command.Tag);
                command.Serialize(writer);
            }

            return Compressor.Compress(writer);
        }

        public static NetworkMessage Deserialize(Deserializer reader)
        {

            var tick = reader.GetUInt();
            var playTick = reader.GetUInt(); //LagCompensation
            var countCommands = reader.GetInt();
            var actorId = reader.GetByte();
            var commands = new List<ICommand>();

            for (int i = 0; i < countCommands; i++)
            {
                var tag = reader.GetByte();
                switch ((CommandTag) tag)
                {
                    case CommandTag.MoveCommand:
                        commands.Add(MoveCommand.Deserialize(reader));
                        break;
                    case CommandTag.SpawnCommand:
                        commands.Add(SpawnCommand.Deserialize(reader));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return new NetworkMessage(tick, playTick, actorId, commands);
        }
    }
}
