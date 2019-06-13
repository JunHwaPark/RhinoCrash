using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //인스펙터에 Sound클래스를 넣기 위해
public class Sound
{
    //공통된 사항을 하나의 클래스로 묶기 위해
    //사운드 이름과, mp3가 담기게 될 변수들
    public string soundName;
    public AudioClip clip; //mp3임
}
public class SoundManager : MonoBehaviour
{
    [Header("사운드 등록")] 
    //사운드 클래스를 수정할수 있게
    [SerializeField] Sound bgmSounds; //bgm이 여러개 있을 수 있음, 배열로 만드는게 효율적

    [Header("브금 플레이어")] //mp3 파일을 재생하는 플레이어 필요
    [SerializeField] AudioSource bgmPlayer;

    // Start is called before the first frame update
    void Start()
    {
        PlayRandomBGM();
    }

    public void PlayRandomBGM()
    {
        //int random = Random.Range(0,2); //브금 랜덤재성
        bgmPlayer.clip = bgmSounds.clip;
        bgmPlayer.Play();

    }
}
