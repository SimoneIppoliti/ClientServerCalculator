using System;
using System.Net;

namespace CalculatorServer
{
    public class Client
    {
        private EndPoint endPoint;
        private Packet packetToSend;
        private Server server;

        public Client(EndPoint endPoint, Server server)
        {
            this.endPoint = endPoint;
            this.server = server;
        }

        public void Process()
        {
            if (packetToSend != null)
            {
                if (server.Send(packetToSend, endPoint))
                {
                    Console.WriteLine("Result packet sent.");
                }
            }
        }

        public void SetPacket(Packet packet)
        {
            packetToSend = packet;
        }
    }
}
