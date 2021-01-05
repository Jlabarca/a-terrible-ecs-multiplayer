using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.Command;
using Actors.Command.Components;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using NLog;
using Online;
using Pixeye.Actors;
using Server;
using Server.Common;
using UnityEngine;

namespace Actors.Online
{
    public class OnlineProcessor : Processor, ITickFixed
    {
        static readonly NLog.Logger log = LoggerFactory.GetLogger(nameof(OnlineProcessor));

        private bool Running;
        public event EventHandler<Init> InitReceived;
        private readonly LiteNetLibNetwork network;
        private readonly GameState gameState;
        private readonly List<NetworkMessage> localBuffer = new List<NetworkMessage>();
        private int bufferIndex;
        public OnlineProcessor()
        {
            network = Layer.Get<LiteNetLibNetwork>();

            gameState = Layer.Get<GameState>();

            network.DataReceived += NetworkOnDataReceived;
        }

        public void TickFixed(float dt)
        {
            network.Update();

            if(!Running) return;
            gameState.tick++;

            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            if (localBuffer.Count > bufferIndex)
            {
                var networkMessage = localBuffer[bufferIndex];

                if (networkMessage.PlayTick <= gameState.tick)
                {
                    log.Info($"Executing Input command: {gameState.tick} ServerTick: {networkMessage.Tick} PlayTick: {networkMessage.PlayTick}");

                    foreach (var command in networkMessage.Commands)
                    {
                        switch (command.Tag)
                        {
                            case CommandTag.MoveCommand:
                                Entity.Create().Set((MoveCommand)command);
                                break;
                            case CommandTag.SpawnCommand:
                                Entity.Create().Set((SpawnCommand)command);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    bufferIndex++;
                }
            }
        }

        async void DelayedStart(Init init)
        {
            log.Info("InitStart");
            var time = init.StartTime / 1000f;
            log.Info("1");
            await  Task.Delay(init.StartTime);
            log.Info(time.ToString());
            Running = true;
            log.Info("Started");
            InitReceived?.Invoke(this, init);
        }

        private void NetworkOnDataReceived(byte[] rawData)
        {
            var data = Compressor.Decompress(rawData);

            var reader = new Deserializer(data);
            var messageTag = (MessageTag)reader.GetByte();
            switch (messageTag)
            {
                case MessageTag.Ping:
                    log.Debug("Ping received");
                    network.SendPing();
                    break;
                case MessageTag.Init:
                    var init = new Init();
                    init.Deserialize(reader);
                    UnityEngine.Random.InitState(init.Seed);
                    log.Info($"{init.ActorID} Initializing in {init.StartTime} ms");
                    DelayedStart(init);
                    break;
                case MessageTag.Input:
                    var networkMessage = NetworkMessage.Deserialize(reader);
                    log.Info($"Input received on ClientTick: {gameState.tick} ServerTick: {networkMessage.Tick} PlayTick: {networkMessage.PlayTick}");
                    localBuffer.Add(networkMessage);
                    break;
                }
            }

        // private readonly Dictionary<ushort, Type> _commandFactories = new Dictionary<ushort, Type>();
        // private void GetCommandsDictionary()
        // {
        //     foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        //     {
        //         foreach (var type in assembly.GetTypes().Where(type => type.GetInterfaces().Any(intf => intf.FullName != null && intf.FullName.Equals(typeof(ICommand).FullName))))
        //         {
        //             var tag = ((ICommand)Activator.CreateInstance(type)).Tag;
        //             if (_commandFactories.ContainsKey(tag))
        //             {
        //                 throw new InvalidDataException($"The command tag {tag} is already registered. Every command tag must be unique.");
        //             }
        //             _commandFactories.Add(tag, type);
        //         }
        //     }
        // }
    }
}
