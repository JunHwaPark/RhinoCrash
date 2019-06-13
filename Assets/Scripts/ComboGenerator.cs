using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboGenerator : MonoBehaviour
{
    public List<GameObject> arrowList;
    
    public bool stop = false;
    public static int order = 0;

    public static float Count = 0;
    private void Update()
    {
        if (stop)
        {
            //reset player color
            GameObject.Find("GameDirector").GetComponent<GameDirector>().playerRhino.transform.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 180);
            
            //for count how long does it complete combo
            Count += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (arrowList[order].transform.localScale.x == 0.5f)
                    arrowList[order++].transform.GetComponent<Renderer>().material.color = Color.red;
                else
                {
                    for (int i = 0; i < arrowList.Count; i++)
                        arrowList[i].transform.GetComponent<Renderer>().material.color = Color.white;
                    order = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (arrowList[order].transform.localScale.x == -0.5f)
                    arrowList[order++].transform.GetComponent<Renderer>().material.color = Color.red;
                else
                {
                    for (int i = 0; i < arrowList.Count; i++)
                        arrowList[i].transform.GetComponent<Renderer>().material.color = Color.white;
                    order = 0;
                }
            }

            if (arrowList.Count == order)
            {
                PlayerController.score += Count;
                Count = 0;
                for (int i = 0; i < arrowList.Count; i++)
                    Destroy(arrowList[i]);
                arrowList.Clear();
                stop = false;
                PlayerController.canMove = true;
                if (ComboGenerator.Count < 3)
                {
                    if (ItemController.itemcount < 3)
                    {
                        ItemController.itemcount += 1;
                    }
                    GameObject item = GameObject.Find("ItemController");
                    item.GetComponent<ItemController>().Item();
                }

                if (HealingController.healingcount < 5)
                {
                    HealingController.healingcount += 1;
                    GameObject healing = GameObject.Find("HealingController");
                    healing.GetComponent<HealingController>().Healing();
                }
                PlayerController.canMove = true;
                order = 0;
            }

            if (Count > 7)
            {
                
                for (int i = 0; i < arrowList.Count; i++)
                    Destroy(arrowList[i]);
                arrowList.Clear();
                stop = false;
                PlayerController.canMove = true;
                order = 0;

                Count = 0;
                GameObject director = GameObject.Find("GameDirector");
                for (int i = 0; i < 4; i++)
                    director.GetComponent<GameDirector>().DecreaseHp();

                PlayerController.score += 7; //만약 7초 넘으면 점수 추가
            }
        }
    }

    public void GenerateCombo(int num)
    {
        arrowList.Clear();
        int randNum = GameDirector.rand.Next();
        float xpos = -10f;
        for (int i = 0; i < num; i++)
        {
            GameObject arrow = Instantiate(Resources.Load("Prefab/LeftPrefab")) as GameObject;
            arrow.transform.position = new Vector3(xpos, 4.3f, -2f);
            if (randNum % 2 == 0)
                arrow.transform.localScale = new Vector3(0.5f, 0.5f, 0);
            else
                arrow.transform.localScale = new Vector3(-0.5f, 0.5f, 0);
            arrowList.Add(arrow);
            randNum /= 2;
            xpos += 1f;
        }
        stop = true;
        Debug.Log(arrowList.Count.ToString());
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Quit");
    }
}
