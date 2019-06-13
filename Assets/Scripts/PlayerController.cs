using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private int line;
    public static float score = 0;
    public static bool canMove = true;
    public static float currentTm = 0;
    public static float tempTm = 0;
    public static bool pastStatus = true; //아깐 bool만
    bool isChangedNow = false;
    Vector3[] linepos;
    Renderer rend;
    Color translucent;
    Color nonTransparent;

    // Use this for initialization
    void Start () {
        line = 0;
        rend = GetComponent<Renderer>();
        linepos = new Vector3[4];
        linepos[0] = new Vector3(-7.3f, -2.94f, 0);
        linepos[1] = new Vector3(-2.1f, -2.94f, 0);
        linepos[2] = new Vector3(2.1f, -2.94f, 0);
        linepos[3] = new Vector3(7.3f, -2.94f, 0);
        translucent = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0.5f);
        nonTransparent = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        //시간을 재는데 이건 플레이어가 필살기를 쓰는 경우 이 시간을 이용해서 필살기 시간을 조정하기 위함
        currentTm += Time.deltaTime;
        //Debug.Log("asdfsdafsdaf" + currentTm);
        if(currentTm - tempTm > 3) // 플레이어가 필살기를 쓰는 시간이 이 시간만큼 경과한다면 다시 skillOn 상태를 false로 바꿔서 플레이어 hp가 떨어지게 함
        {
            SkillController.skillOn = false;
        }

        //rend.material.color = translucent;

        //GameObject rhino = GameObject.Find("player");
        //rhino.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);

        isChangedNow = false;
        if(pastStatus != canMove)
        {
            GameObject background = GameObject.Find("Black");
            background.GetComponent<BlackController>().ChangeTransperancy();
            isChangedNow = true;

            if (pastStatus)
            {
                GameObject combo = GameObject.Find("ComboGenerator");
                combo.GetComponent<ComboGenerator>().GenerateCombo(GameDirector.stage > 1 ? 8 : 6);
            }
        }
        pastStatus = canMove;

        if (GameDirector.stage == 1)
        {
            for (int i = 1; i < 3; i++)
            {
                if (mobGenerator.count == i * 30)
                    canMove = false;
            }
        }
        else
        {
            for(int i = 1; i < 6; i++)
            {
                if (mobGenerator.count == i * 50)
                    canMove = false;
            }
        }

        if (canMove && !isChangedNow)
        {
            if(SkillController.skillOn)
            {
                rend.material.color = translucent;
            }
            else
            {
                rend.material.color = nonTransparent;
            }
 
            if (Input.GetKeyDown(KeyCode.LeftArrow) && line > 0)
                line--;
            else if (Input.GetKeyDown(KeyCode.RightArrow) && line < 3)
                line++;
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (ItemController.itemcount == 0 || ItemController.itemcount > 3)
                {
                    return;
                }
                else
                {
                    tempTm = currentTm;
                    GameObject skill = GameObject.Find("SkillController");
                    skill.GetComponent<SkillController>().Skill();
                    SkillController.skillOn = true;
                    Debug.Log("sdfasdfsdaf");
                    //skill.GetComponent<SkillController>().Invincible();
                    GameObject item = GameObject.Find("ItemController");
                    item.GetComponent<ItemController>().Item();
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (HealingController.healingcount == 0 || HealingController.healingcount > 5)
                {
                    return;
                }
                else
                {
                    //HealingController.healingcount -= 1;
                    //HealingController.i -= 1;
                    //HealingController.healingcount -= 1; //하나 줄여주고
                    //Destroy(GameObject.Find("HealingController").GetComponent<HealingController>().HealingList[HealingController.healingcount]);


                    //GameObject.Find("HealingController").GetComponent<HealingController>().HealingList[HealingController.healingcount].SetActive(false);
                    //Destroy(GameObject.Find("HealingController").GetComponent<HealingController>().HealingList[HealingController.healingcount]);

                    SoundManager_GameScene.instance.PlaySE("Drinking"); //물약 마시는 효과음

                    GameObject skill = GameObject.Find("SkillController");
                    skill.GetComponent<SkillController>().Skillhealing();
                    GameObject healing = GameObject.Find("HealingController");
                    healing.GetComponent<HealingController>().Healing();
                    //healing.GetComponent<HealingController>().Reset();
                    GameObject increasehp = GameObject.Find("GameDirector");
                    increasehp.GetComponent<GameDirector>().IncreaseHp();


                    //GameObject.Find("HealingController").GetComponent<HealingController>().HealingList[HealingController.healingcount].SetActive(true);
                }
            }
            transform.position = linepos[line];
        }


        //else
        //    rend.material.color = translucent;
    }

    
}