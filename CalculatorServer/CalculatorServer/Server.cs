using System;
using System.Net;
using System.Collections.Generic;

namespace CalculatorServer
{
    public class Server
    {
        private delegate void GameCommand(byte[] data, EndPoint sender);
        private Dictionary<byte, GameCommand> commandsTable;
        private ITransport transport;

        public Server(ITransport transport)
        {
            this.transport = transport;
            commandsTable = new Dictionary<byte, GameCommand>();
            commandsTable[0] = Add;
            commandsTable[1] = Subtract;
            commandsTable[2] = Multiply;
            commandsTable[3] = Divide;
        }

        public void Run()
        {
            Console.WriteLine("Server started");
            while (true)
            {
                SingleStep();
            }
        }

        public void SingleStep()
        {
            EndPoint sender = transport.CreateEndPoint();
            byte[] data = transport.Recv(256, ref sender);
            if (data != null)
            {
                byte command = data[0];
                if (commandsTable.ContainsKey(command))
                {
                    commandsTable[command](data, sender);
                }
            }
        }

        private void Add(byte[] data, EndPoint sender)
        {
            float left = BitConverter.ToSingle(data, 1);
            float right = BitConverter.ToSingle(data, 5);
            Console.WriteLine("Command: {0} - Values: {1}, {2}", data[0], left, right);
            float result = left + right;
            Packet packet = new Packet(0, result);
            Send(packet, sender);
        }

        private void Subtract(byte[] data, EndPoint sender)
        {
            float left = BitConverter.ToSingle(data, 1);
            float right = BitConverter.ToSingle(data, 5);
            Console.WriteLine("Command: {0} - Values: {1}, {2}", data[0], left, right);
            float result = left - right;
            Packet packet = new Packet(0, result);
            Send(packet, sender);
        }

        private void Multiply(byte[] data, EndPoint sender)
        {
            float left = BitConverter.ToSingle(data, 1);
            float right = BitConverter.ToSingle(data, 5);
            Console.WriteLine("Command: {0} - Values: {1}, {2}", data[0], left, right);
            float result = left * right;
            Packet packet = new Packet(0, result);
            Send(packet, sender);
        }

        private void Divide(byte[] data, EndPoint sender)
        {
            float left = BitConverter.ToSingle(data, 1);
            float right = BitConverter.ToSingle(data, 5);
            Console.WriteLine("Command: {0} - Values: {1}, {2}", data[0], left, right);
            Packet packet;
            if (right != 0)
            {
                float result = left / right;
                packet = new Packet(0, result);
            }
            else
            {
                packet = new Packet(0, "/!\\ Division by 0 is not allowed.");
            }
            Send(packet, sender);
        }

        public bool Send(Packet packet, EndPoint receiver)
        {
            return transport.Send(packet.GetData(), receiver);
        }
    }
}
