using System.Net;
using System.Net.Sockets;
using System.Text;
namespace UDPSocket2
{
    class Program
    {
        static void Main(string[] args)
        { 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string? message = "";
            try
            {
                //servers ip and port
                Console.Write("Enter server ip: ");
                string ips = Console.ReadLine();
                Console.Write("Enter server port: ");
                int ports = Int32.Parse(Console.ReadLine());

                //client ip and port
                Console.Write("Enter client ip: ");
                string ip = Console.ReadLine();
                Console.Write("Enter clients port: ");
                int port = Int32.Parse(Console.ReadLine());
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                socket.Bind(iPEndPoint);
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    StringBuilder stringBuilder = new StringBuilder();
                    var size = 0;
                    EndPoint ipendPoint2 = new IPEndPoint(IPAddress.Parse(ips), ports);
                    Console.Write("Enter your message: ");
                    message = Console.ReadLine();
                    if (message != "") socket.SendTo(Encoding.UTF8.GetBytes(message), ipendPoint2);
                    while (socket.Available > 0)
                    {
                        size = socket.ReceiveFrom(buffer, 0, ref ipendPoint2);
                        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, size));
                    } 
                    if(stringBuilder.Length > 0)Console.WriteLine($"Server request => {stringBuilder}");
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
