using System.Net;

namespace CalculatorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TransportIPv4 transport = new TransportIPv4();
            transport.Bind("192.168.1.123", 9999);
            Server server = new Server(transport);
            server.Run();
        }
    }
}
