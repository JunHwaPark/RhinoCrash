using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System;

namespace RhinoCrash_Server
{
    class Set
    {
        public string Name { set; get; }
        public float Score { set; get; }

        public static Set GetTmpSet(string name, float score)
        {
            Set tmp = new Set();
            tmp.Name = name; tmp.Score = score;
            return tmp;
        }
    }

    public class Server
    {
        public Thread m_thReader = null;
        public Thread m_thServer = null;

        TcpListener m_listener;
        NetworkStream m_Stream;
        StreamReader m_Read;
        StreamWriter m_Write;


        bool m_bStop;
        bool m_bConnect;

        string fileName = "./rank/ranking.txt";
        string allScoreFile = "./rank/Allranking.txt";

        public static void Main()
        {
            Server server = new Server();

            server.m_thServer = new Thread(new ThreadStart(server.ServerStart));
            server.m_thServer.Start();

            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (input.Equals("exit"))
                    break;
            }
            for (int i = 0; i < 4; i++)
                server.ServerStop();   //exit server
            return;
        }

        public void ServerStop()
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

        public void ServerStart()
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
                    Receive_Filecheck();
                }
                else if (Request.Equals("Rankcheck"))
                {
                    Console.WriteLine("Client request Rank check..");
                    Receive_Rankcheck();
                }
                else if (Request.Equals("Insert"))
                {
                    Console.WriteLine("Client request Insert his(her) rank..");
                    Receive_Insert();
                }
                else if (Request.Equals("Disconnect"))
                {
                    Console.WriteLine("Client disconnected");
                    return;
                }
            }
        }

        private void Receive_Filecheck()
        {
            int clientIndex = int.Parse(m_Read.ReadLine());
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            int index = int.Parse(streamReader.ReadLine());
            Console.WriteLine("Index of rank file is " + index.ToString());

            fileStream.Close();
            streamReader.Close();
            if (clientIndex == index)
            {
                Console.WriteLine("Client's file is latest rank file.");
                m_Write.WriteLine("Latest");
                m_Write.Flush();
            }
            else
            {
                Console.WriteLine("Client's file is old rank file.");
                m_Write.WriteLine("Old");
                m_Write.Flush();
                SendFile();
            }
        }

        private void Receive_Rankcheck()
        {
            List<float> scoreList = new List<float>();
            float clientScore = float.Parse(m_Read.ReadLine());
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            streamReader.ReadLine();
            string rank;
            while ((rank = streamReader.ReadLine()) != null)
            {
                float rankScore = float.Parse(rank.Substring(4));
                //Console.WriteLine(rankScore.ToString());
                scoreList.Add(rankScore);
            }
            scoreList.Add(clientScore);
            scoreList.Sort();
            //Console.WriteLine(scoreList.IndexOf(clientScore) + 1);
            streamReader.Close();
            fileStream.Close();
            m_Write.WriteLine((scoreList.IndexOf(clientScore) + 1).ToString());
            m_Write.Flush();
        }

        private void Receive_Insert()
        {
            string clientName = m_Read.ReadLine();
            float clientScore = float.Parse(m_Read.ReadLine());
            FileStream fileStream = new FileStream(allScoreFile, FileMode.Append, FileAccess.Write);
            //StreamReader streamReader = new StreamReader(fileStream);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(clientName + " " + clientScore.ToString());

            streamWriter.Close();
            fileStream.Close();
            //Console.WriteLine(scoreList.IndexOf(clientScore) + 1);
            RemakeFile();
        }

        private void SendFile()
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fileStream);

            string str;
            while ((str = sr.ReadLine()) != null)
                m_Write.WriteLine(str);
            m_Write.WriteLine("end");
            m_Write.Flush();
            Console.WriteLine("Send latest rank file.");

            sr.Close();
            fileStream.Close();
        }

        private void RemakeFile()
        {
            FileStream fs = new FileStream(allScoreFile, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fs);
            List<Set> setList = new List<Set>();
            string set;
            while ((set = streamReader.ReadLine()) != null)
            {
                //Console.WriteLine(set);
                setList.Add(Set.GetTmpSet(set.Substring(0, 3), float.Parse(set.Substring(4))));
            }
            setList.Sort(delegate (Set A, Set B)
            {
                if (A.Score > B.Score) return 1;
                else return -1;
            });
            streamReader.Close(); fs.Close();

            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fs);
            int index = int.Parse(streamReader.ReadLine());
            streamReader.Close(); fs.Close();

            Set[] sets = setList.ToArray();

            fs = new FileStream(fileName, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(index + 1);

            for (int i = 0; i < (setList.Count < 5 ? setList.Count : 5); i++)
            {
                string tmpString = sets[i].Name + " " + sets[i].Score.ToString();
                Console.WriteLine(tmpString);
                sw.WriteLine(tmpString);
            }
            sw.Close(); fs.Close();
        }
    }
}