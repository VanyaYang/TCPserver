using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTCP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Start();
            if (Start().IsCompleted)
            {
                string path = "C:\\Users\\Chuvya\\Desktop\\ррор\\LogServerTCP.txt";
                StreamWriter str = new StreamWriter(path);
                
                if (File.Exists(path))
                {
                    str.WriteLine("Подключение утсановлено");
                }
                else
                {
                    File.Create(path);
                    str.WriteLine("Подключение установлено!");
                    str.Close();
                }
            }
        }
        static List<TcpClient> clients = new List<TcpClient>();

        static async Task Start()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 7777);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                clients.Add(client);
                _ = HandleClient(client);

            }
        }
        //public void StartingServer()
        //{
        //    Start();
        //}
        static async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    int byteRead = await stream.ReadAsync(buffer);
                    string message = Encoding.UTF8.GetString(buffer, 0, byteRead);

                    Console.WriteLine("Message me " + message);
                    //if (message != "")
                    //{
                    //    message += " Вы ввели";
                    BroadcastMessage(message, client);
                    //}
                }
            }
            catch
            {
                clients.Remove(client);
                client.Close();
            }
        }

        static void BroadcastMessage(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            foreach (var client in clients)
            {
                if (client != sender)
                {
                    NetworkStream stream = client.GetStream();
                    stream.WriteAsync(data, 0, data.Length);
                }
            }
        }
    }
}
