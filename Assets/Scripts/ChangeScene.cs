using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void NewStart()
    {
        mobGenerator.count = 0;
        ItemController.itemcount = 0;
        GameDirector.stage = 0;
        GameDirector.HP = 10;
        GameDirector.hpGaugefill = 1f;
        GameDirector.HPfill = 10;
        ComboGenerator.Count = 0;
        PlayerController.score = 0;
        HealingController.healingcount = 0;

        //ComboGenerator.stop = false;
        ComboGenerator.order = 0;

        BlackController.blackState = 0;
        SceneManager.LoadScene("StartScene");
    }

    public void EndingScene_Exit()
    {
        mobGenerator.count = 0;
        ItemController.itemcount = 0;
        GameDirector.stage = 0;
        GameDirector.HP = 10;
        GameDirector.hpGaugefill = 1f;
        GameDirector.HPfill = 10;
        ComboGenerator.Count = 0;
        PlayerController.score = 0;
        HealingController.healingcount = 0;

        //ComboGenerator.stop = false;
        ComboGenerator.order = 0;

        BlackController.blackState = 0;
        SceneManager.LoadScene("StartScene");
    }

    public void EndingScene_Ranking_Registration()
    {
        SceneManager.LoadScene("RankingRegistrationScene");
    }

    public void RankingRegistrationScene_Exit()
    {
        mobGenerator.count = 0;
        ItemController.itemcount = 0;
        GameDirector.stage = 0;
        GameDirector.HP = 10;
        GameDirector.hpGaugefill = 1f;
        GameDirector.HPfill = 10;
        ComboGenerator.Count = 0;
        PlayerController.score = 0;
        HealingController.healingcount = 0;

        //ComboGenerator.stop = false;
        ComboGenerator.order = 0;

        BlackController.blackState = 0;
        SceneManager.LoadScene("StartScene");
    }

    public void RankingRegistrationScene_Registration()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void RankingScene()
    {
        SceneManager.LoadScene("RankingScene");
    }
}
