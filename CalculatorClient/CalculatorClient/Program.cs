using System;
using System.Collections.Generic;
using System.Net;

namespace CalculatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("192.168.1.123", 9999);
            client.Run();
        }
    }
}
