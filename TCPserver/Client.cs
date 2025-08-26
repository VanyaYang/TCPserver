using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPserver
{
    internal class Client
    {
        
        
        static async Task Connect ()
        {
            TcpClient client = new TcpClient();
            string path = "127.0.0.1";

            await client.ConnectAsync(path, 7777);

            Console.WriteLine("Connected Done...");

            _=Task.Run(() => ReceiveMessage(client));

            NetworkStream stream = client.GetStream();
            while (true)
            {
                string input = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(input);
                await stream.WriteAsync(data);
            }            
        }
        public void StartingClient()
        {
            Connect();
        }
        static async Task ReceiveMessage(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            while (true)
            {
                int byteRead = await stream.ReadAsync(buffer);
                string message = Encoding.UTF8.GetString(buffer, 0, byteRead);
                Console.WriteLine($"[Server]: {message}");
            }
        }
    }
}
