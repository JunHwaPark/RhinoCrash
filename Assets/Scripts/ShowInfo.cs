using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using System;
using System.Data;
using System.IO;

public class ShowInfo : MonoBehaviour
{
    public Text Name1Text;
    public Text Name2Text;
    public Text Name3Text;
    public Text Name4Text;
    public Text Name5Text;

    public Text Score1Text;
    public Text Score2Text;
    public Text Score3Text;
    public Text Score4Text;
    public Text Score5Text;
    public Text[,] texts = new Text[5, 2];

    string fileName;
    string userName;

    void Start()
    {
        userName = (System.Security.Principal.WindowsIdentity.GetCurrent().Name).Split('\\')[1];
        string rankPath = "C:/Users/" + userName + "/RhinoCrash";
        
        fileName = rankPath + "/ranking.txt";

        FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader streamReader = new StreamReader(fileStream);
        //Debug.Log("C:/Users/" + userName + "/RhinoCrash/ranking.txt");
        string sindex = streamReader.ReadLine();
        int index;
        if (sindex != null)
            index = int.Parse(sindex);
        else
            index = 0;
        Debug.Log("index is " + index.ToString());
        string str;

        texts[0, 0] = Name1Text;
        texts[0, 1] = Score1Text;
        texts[1, 0] = Name2Text;
        texts[1, 1] = Score2Text;
        texts[2, 0] = Name3Text;
        texts[2, 1] = Score3Text;
        texts[3, 0] = Name4Text;
        texts[3, 1] = Score4Text;
        texts[4, 0] = Name5Text;
        texts[4, 1] = Score5Text;

        //if ((str = streamReader.ReadLine()) != null)
        //{
        //    texts[0, 0].text = str.Substring(0, 3).ToString();
        //    texts[0, 1].text = str.Substring(4).ToString();
        //}

        for(int i =0; (str = streamReader.ReadLine()) != null; i++)
        {
            texts[i, 0].text = str.Substring(0, 3).ToString();
            texts[i, 1].text = str.Substring(4).ToString();
        }

        streamReader.Close();
        fileStream.Close();
    }

    //public void ShowRank()
    //{

    //    //while ((str=streamReader.ReadLine())!=null)
    //    //{
    //    //    texts[0, i] = str.Substring(0, 2);
    //    //    texts[1, i] = str.Substring(4);
    //    //}

    //    //if ((str = streamReader.ReadLine()) != null)
    //    //{
    //    //    texts[0, 0].text = str.Substring(0, 2).ToString();
    //    //    texts[0, 1].text = str.Substring(4).ToString();
    //    //}


    //    streamReader.Close();
    //    fileStream.Close();
        
    //}
}
