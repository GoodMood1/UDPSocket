using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPSocket2
{
     class Client
    {
        public void ClientRun()
        {
            //servers ip and port
            Console.Write("Enter ip: ");
            string ips = Console.ReadLine();
            Console.WriteLine("Enter port: ");
            int ports = Int32.Parse(Console.ReadLine());

            //client ip and port
            Console.Write("Enter ip: ");
                string ip = Console.ReadLine();
                Console.WriteLine("Enter port: ");
                int port = Int32.Parse(Console.ReadLine());
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                socket.Bind(iPEndPoint);
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    StringBuilder stringBuilder = new StringBuilder();
                    var size = 0;
                    EndPoint ipendPoint2 = new IPEndPoint(IPAddress.Parse(ips), ports);
                    string? message = Console.ReadLine();
                    if (message != null) socket.SendTo(Encoding.UTF8.GetBytes(message), ipendPoint2);
                    else socket.SendTo(Encoding.UTF8.GetBytes("Hello my server!"), ipendPoint2);
                    do
                    {
                        size = socket.ReceiveFrom(buffer, 0, ref ipendPoint2);
                        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, size));
                    } while (socket.Available > 0);
                    Console.WriteLine($"Server request => {stringBuilder}");
                }
            }
            catch (Exception ex)
            {
                global::System.Console.WriteLine(ex.Message);
            }
            finally
            {
                socket?.Shutdown(SocketShutdown.Both);
                socket?.Close();

            }
        }
        }
}
