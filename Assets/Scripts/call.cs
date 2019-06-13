using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class call : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    GameObject rg = GameObject.Find("socket");
    //    rg.GetComponent<RankingGet>().Rankcheck(13f);
    //}

    // Update is called once per frame
//    void Update()
//    {
        
//    }
    public static void callRankCheck(float score)
    {
        GameObject rg = GameObject.Find("socket");
        rg.GetComponent<RankingGet>().Rankcheck(score);
    }

    public static void callInsert(string name, float score)
    {
        GameObject rg = GameObject.Find("socket");
        rg.GetComponent<RankingGet>().Insert(name, score);
    }
}
