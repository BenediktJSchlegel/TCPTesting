using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;

class MainClass
{
    public static void Main()
    {

        try
        {
            TcpListener tcp = new TcpListener(IPAddress.Parse("192.168.1.8"), 6555);
            tcp.Start();

            Socket socket = null;

            while (true)
            {

                if (socket != null)
                {
                    Console.WriteLine("Awaiting Data. . .");

                    byte[] data = new byte[1024];
                    int receivedDataLength = socket.Receive(data);
                    string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                    Console.WriteLine(stringData);
                }
                else
                {
                    socket = tcp.AcceptSocket();
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }

    }
}

