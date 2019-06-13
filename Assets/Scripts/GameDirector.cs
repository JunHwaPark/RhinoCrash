using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    //Renderer rend;

    GameObject hpGauge;
    GameObject skillGauge;
    public GameObject playerRhino;
    public static int stage = 0;
    public static int HP = 10;




    public static float hpGaugefill = 1f;
    public static int HPfill = 10;

    public static int skill = 0; //for count yellow heart

    public static System.Random rand = new System.Random();



    // Use this for initialization
    void Start()
    {
        this.hpGauge = GameObject.Find("hpGauge");
        this.skillGauge = GameObject.Find("skillGauge");
        this.playerRhino = GameObject.Find("player");
        stage++;
        Debug.Log(stage.ToString() + "Stage start");



        //rend = GetComponent<Renderer>();
        //rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0f);
        this.hpGauge.GetComponent<Image>().fillAmount = GameDirector.hpGaugefill;
        GameDirector.HP = GameDirector.HPfill;


    }

    public void changeColor()
    {
        playerRhino.transform.GetComponent<Renderer>().material.color = Color.red;
        new WaitForSeconds(1f);
    }
    //public void resetColor()
    //{
    //    //넌 아냐 이새끼야
    //    playerRhino.transform.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 180);
    //}
    public void DecreaseHp()
    {
        changeColor();
        this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;
        this.skillGauge.GetComponent<Image>().fillAmount += 0.125f;
        GameDirector.HP--;

        GameDirector.hpGaugefill -= 0.1f;
        GameDirector.HPfill -= 1;

        SoundManager_GameScene.instance.PlaySE("Stump"); //효과음 넣기

        if (skill < 8)
        {
            skill += 1;
        }
        else if (skill == 8)
        {
            skill = 0;
            if (ItemController.itemcount < 3)
            {
                ItemController.itemcount += 1;
            }
            this.skillGauge.GetComponent<Image>().fillAmount -= 1f;
            GameObject item = GameObject.Find("ItemController");
            item.GetComponent<ItemController>().Item();
        }
        Debug.Log("collision detected");
    }

    public void Dead()
    {
        //mobGenerator.count = 0;
        //ItemController.itemcount = 0;
        //GameDirector.stage = 0;
        //GameDirector.HP = 10;
        //GameDirector.hpGaugefill = 1f;
        //GameDirector.HPfill = 10;
        //ComboGenerator.Count = 0;
        //PlayerController.score = 0;
        //HealingController.healingcount = 0;

        ////ComboGenerator.stop = false;
        //ComboGenerator.order = 0; 

        SceneManager.LoadScene("DeadScene");
        SoundManager_GameScene.instance.PlaySE("Death"); //죽으면 소리날거

        /////
        PlayerController.pastStatus = PlayerController.canMove;
        PlayerController.canMove = true;

        //Renderer rend;


        //rend = GetComponent<Renderer>();
        //BlackController.rend.material.color = new Color(BlackController.rend.material.color.r, BlackController.rend.material.color.g, BlackController.rend.material.color.b, 0f);
        //background.GetComponent<BlackController>().ChangeTransperancy();

    }

    public void IncreaseHp()
    {
        this.hpGauge.GetComponent<Image>().fillAmount += 0.5f;
        GameDirector.HP += 5;
    }
}