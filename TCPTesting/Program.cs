using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class MainClass
{
    public static void Main()
    {
        IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.1.8"), 6555);

        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            server.Connect(ip);
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server: " + e);
            return;
        }

        while (true)
        {
            string input = Console.ReadLine();
            server.Send(Encoding.ASCII.GetBytes(input));

            Console.WriteLine("Sent:");
            Console.WriteLine(Environment.NewLine);
        }

    }
}

