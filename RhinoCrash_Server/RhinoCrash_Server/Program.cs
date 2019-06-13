//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Net.Sockets;
//using System.Threading;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Net;
//using System.Xml.Serialization;

//namespace Application_Software_3rdPractice
//{
//    public partial class Server
//    {
//        public static string ip, port;
//        public Thread m_thServer = null;
//        //panel_data.ShapeDataTable shapeRows = new panel_data.ShapeDataTable();
//        public DataTable dataTable;// = new DataTable("ScoreInfo");

//        //DataRow dr;
//        //Bitmap bmp = new Bitmap("C:/Users/junhwa/source/repos/Application_Software_3rdPractice/Client/bin/Debug/abc.bmp");

//        TcpListener m_listener;
//        bool m_bStop;
//        int index = 0;
//        //public List<ServerThread> serverThreads = new List<ServerThread>();
//        public ServerThread[] serverThreads = new ServerThread[10];

//        public static void Main()
//        {
//            Server server = new Server();
//            server.dataTable = new DataTable("Rank");
//            server.dataTable.Columns.Add("Name", typeof(string));
//            server.dataTable.Columns.Add("Score", typeof(float));
//            try
//            {
//                server.dataTable.ReadXml("Rank.xml");
//            }
//            catch (FileNotFoundException)
//            {
//                server.dataTable.WriteXml("Rank.xml");
//            }
//            Console.WriteLine(server.dataTable.Columns.Count);
//            //DataRow dt = server.dataTable.NewRow();
//            //dt[0] = "PJH"; dt[1] = 12.45124;
//            //server.dataTable.Rows.Add(dt);
//            server.Start();

//            string str;
//            do
//            {
//                str = Console.ReadLine();
//            } while (!str.Equals("exit"));
//            foreach(var item in server.serverThreads)
//            {
//                server.ServerThreadExit(item);
//            }
//            server.dataTable.WriteXml("Rank.xml");
//            return;
//        }

//        public void Start()
//        {
//            for (int i = 0; i < 10; i++)
//                serverThreads[i] = new ServerThread(this);

//            this.m_thServer = new Thread(new ThreadStart(ServerStart));
//            this.m_thServer.Start();
//        }

//        public void ServerStart()
//        {
//            m_listener = new TcpListener(IPAddress.Any, 7777);
//            m_listener.Start();

//            m_bStop = true;

//            while (m_bStop)
//            {
//                TcpClient hClient = m_listener.AcceptTcpClient();
//                if (hClient.Connected)
//                {
//                    for (int i = 0; i < 10; i++)
//                    {
//                        if (!serverThreads[i].m_bConnect)
//                        {
//                            index = i; break;
//                        }
//                    }
//                    serverThreads[index].m_bConnect = true;
//                    serverThreads[index].m_Stream = hClient.GetStream();
//                    serverThreads[index].m_Read = new StreamReader(serverThreads[index].m_Stream);
//                    serverThreads[index].m_Write = new StreamWriter(serverThreads[index].m_Stream);
//                    serverThreads[index].m_bRead = new BinaryReader(serverThreads[index].m_Stream);
//                    serverThreads[index].m_bWrite = new BinaryWriter(serverThreads[index].m_Stream);

//                    serverThreads[index].m_thReader = new Thread(new ThreadStart(serverThreads[index].Receive));
//                    serverThreads[index].m_thReader.Start();
//                }
//            }
//        }

//        public void ServerThreadExit(ServerThread st)
//        {
//            if (!st.m_bConnect)
//                return;

//            st.m_Read.Close();
//            st.m_Write.Close();

//            st.m_Stream.Close();
//            st.m_thReader.Abort();
//        }
//    }

//    public class ServerThread
//    {
//        public NetworkStream m_Stream;
//        public StreamReader m_Read;
//        public StreamWriter m_Write;
//        public BinaryWriter m_bWrite;
//        public BinaryReader m_bRead;
//        public BinaryFormatter bf;
//        public MemoryStream ms;
//        public bool m_bConnect = false;
//        private Server server;
//        public Thread m_thReader = null;

//        public ServerThread(Server server)
//        {
//            this.server = server;
//        }

//        public void Receive()
//        {
//            Console.WriteLine("new client");
//            string Request;
//            while (m_bConnect)
//            {
//                Request = m_Read.ReadLine();
//                if (Request.Equals("RankCheck"))
//                {
//                    float num = float.Parse(m_Read.ReadLine());
//                    DataColumn dataColumn = new DataColumn();
//                    DataRow dataRow = new DataRow();

//                    foreach(var item in dataRow.)
//                    {
//                        item.
//                    }
//                }
//                else if (Request.Equals("Insert"))
//                {

//                }
//                else if (Request.Equals("Filecheck"))
//                {
//                    int num = int.Parse(m_Read.ReadLine());
//                    if(server.dataTable.Rows.Count == num)
//                    {
//                        m_Write.WriteLine("Latest");
//                    }
//                    else
//                    {
//                        m_Write.WriteLine("Old");
//                        m_Write.Flush();
//                        XmlSerializer xmlSerializer = new XmlSerializer(server.dataTable.GetType());
//                        xmlSerializer.Serialize(m_Write, server.dataTable);
//                    }
//                    m_Write.Flush();
//                }
//                else if (Request.Equals("Disconnect"))
//                {
//                    m_bConnect = false;
//                    break;
//                }
//            }
//            server.ServerThreadExit(this);
//        }
//    }
//}

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

        string fileName = "./ranking.txt";
        string allScoreFile = "./Allranking.txt";

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
            Console.WriteLine(clientIndex);
            FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            string sindex = streamReader.ReadLine();
            int index;
            if (sindex != null)
                index = int.Parse(sindex);
            else
                index = 0;
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
            Console.WriteLine("Insert :" + clientName + " " + clientScore);
            FileStream fileStream = new FileStream(allScoreFile, FileMode.Append, FileAccess.Write);
            //StreamReader streamReader = new StreamReader(fileStream);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(clientName + " " + clientScore);

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
                Console.WriteLine(set);
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
            string sindex = streamReader.ReadLine();
            int index;
            if (sindex != null)
                index = int.Parse(sindex);
            else
                index = 0;
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
            sw.Flush();
            sw.Close(); fs.Close();
        }
    }
}