using UnityEngine;
using UnityEngine.Events;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;

    public AudioClip filpAudio; //오디오 소스
    public AudioClip getAudio; //오디오 소스

    private void Start()
    {
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    //선택 오디오 소스 랜덤 피치값 설정과 실행
    public void SelectSoundPlay()
    {
        soundManager.SFX_Play(filpAudio);
    }

    //선택 취소 오디오 소스 랜덤 피치값 설정과 실행
    public void CancelSoundPlay()
    {
        soundManager.SFX_Play(filpAudio);
    }

    public void GetSoundPlay()
    {
        soundManager.SFX_Play(getAudio);
    }
}
