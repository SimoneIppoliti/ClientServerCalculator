using System;
using System.IO;
using System.Text;

namespace CalculatorServer
{
    public class Packet
    {
        private MemoryStream stream;
        private BinaryWriter writer;

        public Packet()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public Packet(byte command, params object[] elements) : this()
        {
            writer.Write(command);
            foreach (object element in elements)
            {
                if (element is int)
                {
                    writer.Write((int)element);
                }
                else if (element is float)
                {
                    writer.Write((float)element);
                }
                else if (element is byte)
                {
                    writer.Write((byte)element);
                }
                else if (element is char)
                {
                    writer.Write((char)element);
                }
                else if (element is string)
                {
                    byte[] text = Encoding.UTF8.GetBytes((string)element);
                    writer.Write(text);
                }
                else
                {
                    throw new Exception("Unknown type");
                }
            }
        }

        public byte[] GetData()
        {
            return stream.ToArray();
        }
    }
}