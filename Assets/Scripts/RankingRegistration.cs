using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingRegistration : MonoBehaviour
{
    public Text ScoreText;
    public Text RankingText;
    public Text Initial1_Text;
    public Text Initial2_Text;
    public Text Initial3_Text;
    public Text Initial_Input_Text;
    public Text Name_Text;

    public static List<string> initial = new List<string>();

    public static int initial_alphabet_index = 0;   //알파벳 리스트에서 지금 몇번째 알파벳인지
    public static int initial_index = 1;            //지금 몇번째 이니셜을 치고있는지
    public static string Name;


    // Start is called before the first frame update
    void Start()
    {

        ScoreText.text = PlayerController.score.ToString();
        RankingText.text = RankingGet.checkRank.ToString(); //등수 만들고 싶기는해, 내가 추가한거

        //Initial_Input_Text.text = "A";

        initial.Add("A");
        initial.Add("B");
        initial.Add("C");
        initial.Add("D");
        initial.Add("E");
        initial.Add("F");
        initial.Add("G");
        initial.Add("H");
        initial.Add("I");
        initial.Add("J");
        initial.Add("K");
        initial.Add("L");
        initial.Add("M");
        initial.Add("N");
        initial.Add("O");
        initial.Add("P");
        initial.Add("Q");
        initial.Add("R");
        initial.Add("S");
        initial.Add("T");
        initial.Add("U");
        initial.Add("V");
        initial.Add("W");
        initial.Add("X");
        initial.Add("Y");
        initial.Add("Z");

        Initial_Input_Text.text = initial[initial_alphabet_index].ToString();

    }

    // Update is called once per frame


    public void RightArrow()
    {
        RankingRegistration.initial_alphabet_index++;
        if (RankingRegistration.initial_alphabet_index > 25)
        {
            RankingRegistration.initial_alphabet_index -= 26;
            Initial_Input_Text.text = initial[RankingRegistration.initial_alphabet_index].ToString();
        }
        Initial_Input_Text.text = initial[RankingRegistration.initial_alphabet_index].ToString();
    }

    public void LeftArrow()
    {
        RankingRegistration.initial_alphabet_index--;
        if (RankingRegistration.initial_alphabet_index < 0)
        {
            RankingRegistration.initial_alphabet_index += 26;
            Initial_Input_Text.text = initial[RankingRegistration.initial_alphabet_index].ToString();
        }
        Initial_Input_Text.text = initial[RankingRegistration.initial_alphabet_index].ToString();

    }

    public void InitialInput()
    {
        switch (initial_index)
        {
            case 1:
                Initial1_Text.text = initial[RankingRegistration.initial_alphabet_index].ToString();
                initial_index++;
                break;
            case 2:
                Initial2_Text.text = initial[RankingRegistration.initial_alphabet_index].ToString();
                initial_index++;
                break;
            case 3:
                Initial3_Text.text = initial[RankingRegistration.initial_alphabet_index].ToString();

                Name = Initial1_Text.text.ToString() + Initial2_Text.text.ToString() + Initial3_Text.text.ToString();
                Name_Text.text = Name;
                break;
        }

    }


//<<<<<<< HEAD
//    //public void sendInfo()
//    //{
//    //    RankingGet rg = GameObject.Find("RankingGet").GetComponent<RankingGet>();
//    //    Debug.Log("i'm silhang");
//    //    rg.Insert(Name, PlayerController.score);
//    //}
//=======
//    public void sendInfo()
//    {
//        //Debug.Log("i'm silhang");
//        //rg.Insert(Name, PlayerController.score);
//    }
//>>>>>>> 06e9424edf52c31653288d788fa4290e41288144

}