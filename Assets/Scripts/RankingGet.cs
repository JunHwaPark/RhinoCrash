﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;

public class RankingGet : MonoBehaviour
{
    NetworkStream m_Stream;
    TcpClient m_Client;
    StreamReader m_Read;
    StreamWriter m_Write;
    BinaryReader binaryReader;
    private Thread m_thReader;

    bool m_bConnect;

    string fileName;

    private void Start()
    {
        //Execute in windows. bring user's name
        string userName = (System.Security.Principal.WindowsIdentity.GetCurrent().Name).Split('\\')[1];
        string rankPath = "C:/Users/" + userName + "/RhinoCrash";
        DirectoryInfo di = new DirectoryInfo(rankPath);
        if (!di.Exists)
            di.Create();
        fileName = rankPath + "/ranking.txt";

        Connect();
    }

    public void ShowName()
    {
        FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader streamReader = new StreamReader(fileStream);
        //Debug.Log("C:/Users/" + userName + "/RhinoCrash/ranking.txt");
        int index = int.Parse(streamReader.ReadLine());
        //Debug.Log(index.ToString());
        streamReader.Close();
        fileStream.Close();
        if (!m_bConnect)
        {
            Debug.LogWarning("Not connected to server");
            return;
        }
        m_Write.WriteLine("Filecheck");
        m_Write.WriteLine(index.ToString());
        m_Write.Flush();
    }

    public void Connect()
    {
        m_Client = new TcpClient();

        try
        {
            m_Client.Connect(IPAddress.Parse("127.0.0.1"), 7777);
        }
        catch
        {
            m_bConnect = false;
            return;
        }
        m_bConnect = true;

        m_Stream = m_Client.GetStream();

        m_Read = new StreamReader(m_Stream);
        m_Write = new StreamWriter(m_Stream);
        binaryReader = new BinaryReader(m_Stream);

        m_thReader = new Thread(new ThreadStart(Receive));
        m_thReader.Start();
    }

    private void Receive()
    {
        string receive = null;

        while (m_bConnect)
        {
            receive = m_Read.ReadLine();
            if (receive.Equals("Latest"))
            {
                //Insert("PJH", 30.1222f);
                Rankcheck(13f);
                //Rank file in client is latest version. Change Scene to ranking.
            }
            else if (receive.Equals("Old"))
                Receive_old();
            //else if (receive.Equals("Select"))
            //    Receive_Select();
            //else if (receive.Equals("Datail"))
            //    Receive_Datail();
            //else if (receive.Equals("Download"))
            //    Receive_Download();
        }
    }

    private void Receive_old()
    {
        Debug.Log("Receive_old");
        File.Delete(fileName);
        FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

        BinaryWriter bw = new BinaryWriter(fileStream);

        byte[] buf = binaryReader.ReadBytes(1024);
        bw.Write(buf);

        bw.Close();
        fileStream.Close();
    }

    public void Rankcheck(float score)
    {
        m_Write.WriteLine("Rankcheck");
        m_Write.WriteLine(score.ToString());
        m_Write.Flush();

        Debug.Log(m_Read.ReadLine());
    }

    public void Insert(string name, float score)
    {
        m_Write.WriteLine("Insert");
        m_Write.WriteLine(name);
        m_Write.WriteLine(score.ToString());
        m_Write.Flush();
    }
}
