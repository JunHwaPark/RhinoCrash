using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System;

public class Server
{
    private Thread m_thReader;
    private Thread m_thServer;

    TcpListener m_listener;
    NetworkStream m_Stream;
    StreamReader m_Read;
    StreamWriter m_Write;
    BinaryWriter binaryWriter;


    bool m_bStop;
    bool m_bConnect;

    string fileName = "./rank/ranking.txt";

    public void main()
    {
        m_thServer = new Thread(new ThreadStart(ServerStart));
        m_thServer.Start();

        string input;
        while (true)
        {
            input = Console.ReadLine();
            if (input.Equals("exit"))
                break;
        }
        ServerStop();   //exit server
        return;
    }

    private void ServerStop()
    {
        m_listener.Stop();
        m_thServer.Abort();

        if (!m_bConnect)
            return;

        m_Read.Close();
        m_Write.Close();

        m_Stream.Close();
        m_thReader.Abort();
    }

    private void ServerStart()
    {
        m_listener = new TcpListener(IPAddress.Any, 7777);
        m_listener.Start();

        m_bStop = true;

        while (m_bStop)
        {
            TcpClient hClient = m_listener.AcceptTcpClient();

            if (hClient.Connected)
            {
                m_bConnect = true;
                Console.WriteLine("Connected Client");

                m_Stream = hClient.GetStream();
                m_Read = new StreamReader(m_Stream);
                m_Write = new StreamWriter(m_Stream);
                binaryWriter = new BinaryWriter(m_Stream);

                m_thReader = new Thread(new ThreadStart(Receive));
                m_thReader.Start();
            }
        }
    }

    private void Receive()
    {
        while (m_bConnect)
        {
            string Request = m_Read.ReadLine();
            if (Request.Equals("Filecheck"))
            {
                Console.WriteLine("Client request Rankfile check..");
                //Receive_root();
            }
            else if (Request.Equals("Rankcheck"))
            {
                Console.WriteLine("Client request Rank check..");
                //Receive_Select();
            }
            else if (Request.Equals("Insert"))
            {
                Console.WriteLine("Client request Insert his(her) rank..");
                //Receive_Datail();
            }
        }
    }

    private void Receive_Filecheck()
    {
        int clientIndex = int.Parse(m_Read.ReadLine());
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        StreamReader streamReader = new StreamReader(fileStream);
        //Debug.Log("C:/Users/" + userName + "/RhinoCrash/ranking.txt");
        int index = int.Parse(streamReader.ReadLine());
        Console.WriteLine("Index of rank file is " + index.ToString());

        fileStream.Close();
        streamReader.Close();
        if(clientIndex == index)
        {
            Console.WriteLine("Client's file is latest rank file.");
            m_Write.WriteLine("Latest");
            //Rankcheck(12f);
        }
        else
        {
            Console.WriteLine("Client's file is old rank file.");
            m_Write.WriteLine("Old");
            Update();
        }
    }

    private void Update()
    {
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fileStream);

        binaryWriter.Write(br.ReadBytes(1024));
        binaryWriter.Flush();

        Console.WriteLine("Send rank file.");

        fileStream.Close();
        br.Close();
    }
}
