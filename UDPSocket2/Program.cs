using System.Net;
using System.Net.Sockets;
using System.Text;
namespace UDPSocket2
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            Client client = new Client();
            server.ServerRun();
            client.ClientRun();
        }
    }
}
