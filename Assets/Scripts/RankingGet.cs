﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using System;

public class RankingGet : MonoBehaviour
{
    NetworkStream m_Stream; //네트워크 스트림
    TcpClient m_Client;
    StreamReader m_Read;
    StreamWriter m_Write;
    private Thread m_thReader;
    public DataTable dataTable;
    string fileName;
    string userName;

    public static string checkRank; //이거 만들어준거

    bool m_bConnect; //서버 접속 플래그
                     //m_bConnect, m_Client - client member

    //string fileName;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameObject rg = GameObject.Find("socket");
        if(rg == gameObject)
        {
            Debug.Log("ddddddddddd");
        }
        else
        {
            return;
        }
        //Execute in windows.bring user's name
        userName = (System.Security.Principal.WindowsIdentity.GetCurrent().Name).Split('\\')[1];
        string rankPath = "C:/Users/" + userName + "/RhinoCrash";
        DirectoryInfo di = new DirectoryInfo(rankPath);
        if (!di.Exists)
            di.Create();
        fileName = rankPath + "/ranking.txt";
        DontDestroyOnLoad(gameObject);
        Connect();
    }

    private void OnApplicationQuit()
    {
        //끝내는 코드
        m_Write.WriteLine("Disconnect");
        m_Write.Flush();
        if (!m_bConnect)
            return;

        m_bConnect = false;

        m_Read.Close();
        m_Write.Close();
        m_Stream.Close();
        m_thReader.Abort();
    }

    public void Disconnect()
    {
        m_Write.WriteLine("Disconnect");
        m_Write.Flush();
        if (!m_bConnect)
            return;

        m_bConnect = false;

        m_Read.Close();
        m_Write.Close();
        m_Stream.Close();
        m_thReader.Abort();
    }

    public void ShowName()
    {
        FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader streamReader = new StreamReader(fileStream);
        Debug.Log("C:/Users/" + userName + "/RhinoCrash/ranking.txt");
        string sindex = streamReader.ReadLine();
        int index;
        if (sindex != null)
            index = int.Parse(sindex);
        else
            index = 0;
        Debug.Log("index is " + index.ToString());
        streamReader.Close();
        fileStream.Close();
        if (!m_bConnect)
        {
            Debug.LogWarning("Not connected to server");
            return;
        }
        m_Write.WriteLine("Filecheck");
        m_Write.WriteLine(index);
        m_Write.Flush();
    }

    public void Connect()
    {
        m_Client = new TcpClient();

        try
        {
            m_Client.Connect(IPAddress.Parse("127.0.0.1"), 7777);
        }
        catch (Exception e)
        {
            m_bConnect = false;
            Debug.Log("error_abcde");
            Debug.Log(e.Message);
            return;
        }
        Debug.Log("connect to server");
        m_bConnect = true;

        m_Stream = m_Client.GetStream();

        m_Read = new StreamReader(m_Stream);
        m_Write = new StreamWriter(m_Stream);
    }

    private void Receive_old()
    {
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fileStream);

        string str;
        while(!(str = m_Read.ReadLine()).Equals("end"))
        {
            sw.WriteLine(str);
        }
        sw.Flush();
        sw.Close(); fileStream.Close();
        Debug.Log("Update rank file");
    }

    public void Rankcheck(float score)
    {
        ShowName();
        string status = m_Read.ReadLine();
        if (status.Equals("Old"))
        {
            Receive_old();
        }
        m_Write.WriteLine("Rankcheck");
        m_Write.WriteLine(score.ToString());
        m_Write.Flush();

        string str = m_Read.ReadLine();

        checkRank = str;
        Debug.Log(str);
        return;
    }

    public void Insert(string name, float score)
    {
        ShowName();
        string status = m_Read.ReadLine();
        if (status.Equals("Old"))
        {
            Receive_old();
        }
        m_Write.WriteLine("Insert");
        m_Write.WriteLine(name);
        m_Write.WriteLine(score.ToString());
        m_Write.Flush();
    }
}
