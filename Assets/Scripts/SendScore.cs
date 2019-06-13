using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendScore : MonoBehaviour
{
    public void sendInfo()
    {
        Debug.Log("보내는거 실행");

        Debug.Log("했다" + PlayerController.score + "를");
    }
}
