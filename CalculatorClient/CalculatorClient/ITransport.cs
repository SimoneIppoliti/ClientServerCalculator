using System.Net;

namespace CalculatorClient
{
    public interface ITransport
    {
        void Bind(string address, int port);
        bool Send(byte[] data, EndPoint receiver);
        byte[] Recv(int bufferSize, ref EndPoint sender);
        EndPoint CreateEndPoint();
    }
}