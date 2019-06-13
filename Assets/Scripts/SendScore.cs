using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendScore : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void sendInfo()
    {
        Debug.Log("보내는거 실행");
        call.callRankCheck(PlayerController.score);
        Debug.Log("보내버렸다" + PlayerController.score + "를");
    }
}
