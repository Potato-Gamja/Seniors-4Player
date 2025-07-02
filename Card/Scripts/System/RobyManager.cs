using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobyManager : MonoBehaviour
{
    GameObject levelPanel; //레벨 판넬
    GameObject settingPanel; //세팅 판넬
    GameObject quitPanel; //종료 판넬
    GameObject tipPanel; //팁 판넬

    public Slider bgSlider; //배경음악 슬라이더
    public Image bgImage; //배경음악 이미지

    public Slider sfxSlider; //효과음 슬라이더
    public Image sfxImage; //효과음 이미지

    public Sprite soundImage; //사운드 on 이미지
    public Sprite nonSoundImage; //사운드 off 이미지

    public AudioSource audioSource;
    public AudioClip clickClip;

    private void Awake()
    {
        levelPanel = GameObject.Find("---Canvas---").transform.GetChild(1).gameObject; //레벨 판넬 게임오브젝트
        settingPanel = GameObject.Find("---Canvas---").transform.GetChild(2).gameObject; //세팅 판넬 게임오브젝트
        quitPanel = GameObject.Find("---Canvas---").transform.GetChild(3).gameObject; //종료 판넬 게임오브젝트
        tipPanel = GameObject.Find("---Canvas---").transform.GetChild(4).gameObject;

        //InvertPanel("Level Panel"); //레벨 판넬 상태 반전
        //InvertPanel("Setting Panel"); //세팅 판넬 상태 반전
        //InvertPanel("Quit Panel"); //종료 판넬 상태 반전
    }

    //난이도 별 씬 이동
    public void SetLevel(int level) //0 쉬움 / 1 보통 / 2 어려움
    {
        switch (level)
        {
            case 0:
                SceneManager.LoadScene("GameScene_Easy"); //게임씬 불러오기
                break;

            case 1:
                SceneManager.LoadScene("GameScene_Normal");
                break;

            case 2:
                SceneManager.LoadScene("GameScene_Hard");
                break;
        }
    }

    public void SoundPlay(AudioClip audioClip)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(audioClip);
    }

    //버튼을 이용한 소리 설정
    public void SetSound(string type)
    {
        switch (type)
        {
            case "BG_Low": //배경음악 소리 감소
                if (bgSlider.value <= 0.1f) //배경음악 슬라이더 값 비교
                    bgImage.sprite = nonSoundImage; //소리 off 이미지로 변경

                bgSlider.value -= 0.1f; //슬라이더 값 감소
                break;

            case "BG_High": //배경음악 소리 증가
                if (bgImage.sprite == nonSoundImage) //배경음악 슬라이더 값 비교
                    bgImage.sprite = soundImage; //소리 on 이미지로 변경
                bgSlider.value += 0.1f; //슬라이더 값 증가
                break;

            case "SFX_Low": //효과음 소리 감소
                if (sfxSlider.value <= 0.1f) //효과음 슬라이더 값 비교
                    sfxImage.sprite = nonSoundImage; //소리 off 이미지로 변경
                sfxSlider.value -= 0.1f; //슬라이더 값 감소
                break;

            case "SFX_High": //효과음 소리 증가
                if (sfxImage.sprite == nonSoundImage) //효과음 슬라이더 값 비교
                    sfxImage.sprite = soundImage; //소리 on 이미지로 변경
                sfxSlider.value += 0.1f; //슬라이더 값 증가
                break;
        }
    }
    
    //슬라이더 터치&드래그를 이용한 소리 이미지 설정
    public void ChangeSoundImage(string type)
    {
        switch(type)
        {
            case "BG": //배경음악
                if(bgSlider.value <= 0.0001f)
                {
                    bgImage.sprite = nonSoundImage; //소리 off 이미지로 변경
                }
                else
                {
                    bgImage.sprite = soundImage; //소리 on 이미지로 변경
                }
                break;

            case "SFX": //효과음
                if (sfxSlider.value == 0.0001f)
                {
                    sfxImage.sprite = nonSoundImage; //소리 off 이미지로 변경
                }
                else
                {
                    sfxImage.sprite = soundImage; //소리 on 이미지로 변경
                }
                break;

        }
    }

    //판넬 활성화 상태 반전
    public void InvertPanel(string type)
    {
        switch (type)
        {
            case "Level Panel": //레벨 판넬
                levelPanel.SetActive(!levelPanel.activeSelf); //활성화 상태 반전
                break;

            case "Setting Panel": //설정 판넬
                settingPanel.SetActive(!settingPanel.activeSelf);
                break;

            case "Quit Panel": //종료 판넬
                quitPanel.SetActive(!quitPanel.activeSelf);
                break;

            case "Tip Panel": //종료 판넬
                tipPanel.SetActive(!tipPanel.activeSelf);
                break;
        }
    }

    //게임종료
    public void ExitGame()
    {
        Application.Quit();
    }

}
