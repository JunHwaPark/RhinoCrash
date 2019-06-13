using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour
{

    Renderer rend;
    public static int blackState = 0; //이거 그냥 상황판단할라고
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0f);
        transform.position = new Vector3(0.015f, 0.036f, 0);
    }

    public void ChangeTransperancy()
    {

        //Debug.Log(rend.material.color.a.ToString());
        //if (rend.material.color.a == 0f)
        //{
        //    rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0.5f);
        //    Debug.Log("change transperancy 0.5");
        //}
        //else
        //{
        //    rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0f);
        //    Debug.Log("change transperancy 0");
        //} //이게 맞는 코드

        if (blackState == 0)
        {
            if (rend.material.color.a == 0f)
            {
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0.5f);
                Debug.Log("change transperancy 0.5");
                Debug.Log("black State는" + blackState);
                blackState = 1;
                
            }
            Debug.Log("바뀐 건" + blackState);

        }
        else if (blackState == 1)
        {
            if (rend.material.color.a == 0.5f)
            {
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0f);
                Debug.Log("change transperancy 0");
                Debug.Log("black State는" + blackState);
                blackState = 0;
                
            }
            Debug.Log("바뀐 건" + blackState);
        }
    }
}