using System;
using System.Collections.Generic;
using System.Linq;
using Actors.Command;
using Actors.Online;
using LiteNetLib;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using Server.Common;

namespace Online
{
    public class LiteNetLibNetwork : INetwork
    {
        private readonly EventBasedNetListener listener = new EventBasedNetListener();

        private NetManager client;

        public event Action<byte[]> DataReceived;

        public bool Connected => client.FirstPeer?.ConnectionState == ConnectionState.Connected;

        private GameState gameState;

        public void Start(GameState gState)
        {
            gameState = gState;
            listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                DataReceived?.Invoke(dataReader.GetRemainingBytes());
                dataReader.Recycle();
            };

            client = new NetManager(listener)
            {
                DisconnectTimeout = 30000
            };

            client.Start();
            Connect("127.0.0.1", 9050);

        }

        public void Connect(string serverIp, int port)
        {
            client.Connect(serverIp, port, "SomeConnectionKey");
        }

        public void Send(byte[] data)
        {
            client.FirstPeer.Send(data, DeliveryMethod.ReliableOrdered);
        }

        public void Send(NetworkMessage networkMessage)
        {

            Send(networkMessage.ToBytes());
        }

        public void Update()
        {
            client.PollEvents();
        }

        public void Stop()
        {
            client.Stop();
        }

        public void Send(ICommand command)
        {
            Send(new NetworkMessage(gameState.tick, gameState.tick, 0, new List<ICommand> {command}));
        }

        public void SendPing()
        {
            var writer = new Serializer();
            writer.Put((byte)MessageTag.Ping);
            Send(Compressor.Compress(writer));
        }
    }
}
