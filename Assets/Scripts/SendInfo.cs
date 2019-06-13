using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendInfo : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public void sendInfoAndScore()
    {
        Debug.Log("점수, 이름 보내는거 실행");
        call.callInsert(RankingRegistration.Name,PlayerController.score);
        Debug.Log("보내버렸다" + "이름은 " + RankingRegistration.Name + "점수는 " + PlayerController.score + "를");
    }
}
