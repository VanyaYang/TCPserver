using System.Net;
using System.Net.Sockets;

namespace TCPserver
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            //var server = new Serv();
            var client = new Client();
            //server.StartingServer();
            client.StartingClient();
            
            
        }
       
        
    }
}
