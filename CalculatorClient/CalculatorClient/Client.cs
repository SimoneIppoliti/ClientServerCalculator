using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClient
{
    public class Client
    {
        private Socket socket;
        private IPEndPoint endPoint;

        public Client(string address, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            endPoint = new IPEndPoint(IPAddress.Parse(address), port);
        }

        public void Run()
        {
            Console.WriteLine("Client started");
            while (true)
            {
                SingleStep();
            }
        }

        public void SingleStep()
        {
            for (int i = 0; i < 100; i++)
            {
                EndPoint sender = new IPEndPoint(0, 0);
                byte[] data = Recv(256, ref sender);
                if (data != null)
                {
                    float result = BitConverter.ToSingle(data, 1);
                    Console.WriteLine("Result: {0}", result);
                }
            }
            Console.WriteLine("Type the operation command:");
            Console.WriteLine("0 - Add");
            Console.WriteLine("1 - Subtract");
            Console.WriteLine("2 - Multiply");
            Console.WriteLine("3 - Divide");
            Console.Write("Command: ");
            byte command = byte.Parse(Console.ReadLine());
            if (command < 0 || command > 3)
            {
                Console.WriteLine("Command not valid.");
                return;
            }
            Console.Write("First number: ");
            string left = Console.ReadLine();
            Console.Write("Second number: ");
            string right = Console.ReadLine();
            float leftNum = float.Parse(left);
            float rightNum = float.Parse(right);
            Packet packet = new Packet(command, leftNum, rightNum);
            socket.SendTo(packet.GetData(), endPoint);
        }

        public byte[] Recv(int bufferSize, ref EndPoint sender)
        {
            int rlen = -1;
            byte[] data = new byte[bufferSize];
            try
            {
                rlen = socket.ReceiveFrom(data, ref sender);
                if (rlen <= 0)
                    return null;
            }
            catch
            {
                return null;
            }
            byte[] trueData = new byte[rlen];
            Buffer.BlockCopy(data, 0, trueData, 0, rlen);
            return trueData;
        }
    }
}
