using System.Net;
using System.Net.Sockets;
using System.Text;
namespace UDPSocket2
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                socket.Bind(iPEndPoint);
                while (true)
                {
                    var size = 0;
                    byte[] buffer = new byte[1024];
                    StringBuilder stringBuilder = new StringBuilder();
                    EndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                    do
                    {
                        size = socket.ReceiveFrom(buffer, 0, ref endPoint);
                        stringBuilder.Append(Encoding.UTF8.GetString(buffer));
                    } while (socket.Available > 0);
                    if (((IPEndPoint)endPoint) != null) Console.Write($"FROM: {((IPEndPoint)endPoint).Address}:{((IPEndPoint)endPoint).Port} >>> ");
                    global::System.Console.WriteLine(stringBuilder);
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