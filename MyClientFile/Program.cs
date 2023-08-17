// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Net.Sockets;

class Client
{
    static void Main()
    {
        string serverIp = "192.168.3.232"; // Indirizzo IP del server
        int serverPort = 5550;

        using (TcpClient client = new TcpClient(serverIp, serverPort))
        {
            using (NetworkStream networkStream = client.GetStream())
            {
                Console.Write("Inserisci il percorso completo del file exe da inviare: ");
                string filePath ="program.exe";
                byte[] fileData = File.ReadAllBytes(filePath);
                networkStream.Write(fileData, 0, fileData.Length);
            }
        }

        Console.WriteLine("File inviato al server.");

        using (TcpClient client = new TcpClient(serverIp, serverPort))
        {
            using (NetworkStream networkStream = client.GetStream())
            {
                byte[] requestBytes = new byte[1024];
                int bytesRead = networkStream.Read(requestBytes, 0, requestBytes.Length);
                string receivedFileName = "program.inv"; // Nome del file ricevuto

                using (FileStream fileStream = new FileStream(receivedFileName, FileMode.Create))
                {
                    fileStream.Write(requestBytes, 0, bytesRead);
                }

                Console.WriteLine("File ricevuto: " + receivedFileName);
            }
        }
    }
}
