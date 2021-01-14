using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TCPReceiverApp
{
    public partial class MainPage : ContentPage
    {
        private string statusMessage = "Waiting for Socket-Connection . . .";
        public string StatusMessage
        {
            get => statusMessage;
            set 
            {
                statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            Thread socketThread = new Thread(SocketHandler);
            socketThread.Start();
        }

        private void SocketHandler()
        {
            try
            {
                //IP Adresse anpassen
                TcpListener tcp = new TcpListener(IPAddress.Parse("x.x.x.x"), 6555);
                tcp.Start();

                Socket socket = null;

                while (true)
                {

                    if (socket != null)
                    {
                        //Nachdem der Socket geöffnet ist, kann in beide Richtungen Kommuniziert werden
                        //socket.Send();
                        byte[] data = new byte[1024];
                        int receivedDataLength = socket.Receive(data);
                        string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                        StatusMessage = stringData;
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
}
