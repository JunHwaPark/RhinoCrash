using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //인스펙터에 Sound클래스를 넣기 위해
public class GameSound
{
    //공통된 사항을 하나의 클래스로 묶기 위해
    //사운드 이름과, mp3가 담기게 될 변수들
    public string soundName;
    public AudioClip clip; //mp3임
}


public class SoundManager_GameScene : MonoBehaviour
{

    public static SoundManager_GameScene instance; //어디서든 접근하게
    [Header("사운드 등록")]
    //사운드 클래스를 수정할수 있게
    [SerializeField] Sound[] bgmSounds; //bgm이 여러개 있을 수 있음, 배열로 만드는게 효율적
    [SerializeField] Sound[] sfxSounds; //효과음 관리하기 위한것

    [Header("브금 플레이어")] //mp3 파일을 재생하는 플레이어 필요
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")] //mp3 파일을 재생하는 플레이어 필요
    [SerializeField] AudioSource[] sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PlayStageBGM();
    }

    public void PlayStageBGM()
    {
        //int random = Random.Range(0,2); //브금 랜덤재성
        if(GameDirector.stage==1)
        {
            bgmPlayer.clip = bgmSounds[1].clip;
            bgmPlayer.Play();
        }
        else if (GameDirector.stage == 2)
        {
            bgmPlayer.clip = bgmSounds[0].clip;
            bgmPlayer.Play();
        }
    }

    public void PlaySE(string _soundName)
    {
        //효과음을 찾기 위한 반복문
        for(int i = 0; i < sfxSounds.Length; i++)
        {
            if(_soundName == sfxSounds[i].soundName)
            {
                //찾았음. 재생중이지 않은 플레이어를 찾아야함, 그래서 반복문 돌림
                for(int x =0 ;x < sfxPlayer.Length ; x++)
                {
                    if(!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip;
                        sfxPlayer[x].Play();
                        //여기까지 재생시키고 싶은 걸 찾고 재생중이지 않은 플레이어를 재생시키게 만듦
                        //원하는 걸 찾았으면 강제로 빠져나가게
                        return;
                    }
                    Debug.Log("모든 효과음 플레이어가 사용중");
                    return;
                }
            }
        }
        Debug.Log("등록된 효과음이 없다");
    }
}
