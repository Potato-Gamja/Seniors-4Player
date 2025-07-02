using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobyManager : MonoBehaviour
{
    GameObject levelPanel; //���� �ǳ�
    GameObject settingPanel; //���� �ǳ�
    GameObject quitPanel; //���� �ǳ�
    GameObject tipPanel; //�� �ǳ�

    public Slider bgSlider; //������� �����̴�
    public Image bgImage; //������� �̹���

    public Slider sfxSlider; //ȿ���� �����̴�
    public Image sfxImage; //ȿ���� �̹���

    public Sprite soundImage; //���� on �̹���
    public Sprite nonSoundImage; //���� off �̹���

    public AudioSource audioSource;
    public AudioClip clickClip;

    private void Awake()
    {
        levelPanel = GameObject.Find("---Canvas---").transform.GetChild(1).gameObject; //���� �ǳ� ���ӿ�����Ʈ
        settingPanel = GameObject.Find("---Canvas---").transform.GetChild(2).gameObject; //���� �ǳ� ���ӿ�����Ʈ
        quitPanel = GameObject.Find("---Canvas---").transform.GetChild(3).gameObject; //���� �ǳ� ���ӿ�����Ʈ
        tipPanel = GameObject.Find("---Canvas---").transform.GetChild(4).gameObject;

        //InvertPanel("Level Panel"); //���� �ǳ� ���� ����
        //InvertPanel("Setting Panel"); //���� �ǳ� ���� ����
        //InvertPanel("Quit Panel"); //���� �ǳ� ���� ����
    }

    //���̵� �� �� �̵�
    public void SetLevel(int level) //0 ���� / 1 ���� / 2 �����
    {
        switch (level)
        {
            case 0:
                SceneManager.LoadScene("GameScene_Easy"); //���Ӿ� �ҷ�����
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

    //��ư�� �̿��� �Ҹ� ����
    public void SetSound(string type)
    {
        switch (type)
        {
            case "BG_Low": //������� �Ҹ� ����
                if (bgSlider.value <= 0.1f) //������� �����̴� �� ��
                    bgImage.sprite = nonSoundImage; //�Ҹ� off �̹����� ����

                bgSlider.value -= 0.1f; //�����̴� �� ����
                break;

            case "BG_High": //������� �Ҹ� ����
                if (bgImage.sprite == nonSoundImage) //������� �����̴� �� ��
                    bgImage.sprite = soundImage; //�Ҹ� on �̹����� ����
                bgSlider.value += 0.1f; //�����̴� �� ����
                break;

            case "SFX_Low": //ȿ���� �Ҹ� ����
                if (sfxSlider.value <= 0.1f) //ȿ���� �����̴� �� ��
                    sfxImage.sprite = nonSoundImage; //�Ҹ� off �̹����� ����
                sfxSlider.value -= 0.1f; //�����̴� �� ����
                break;

            case "SFX_High": //ȿ���� �Ҹ� ����
                if (sfxImage.sprite == nonSoundImage) //ȿ���� �����̴� �� ��
                    sfxImage.sprite = soundImage; //�Ҹ� on �̹����� ����
                sfxSlider.value += 0.1f; //�����̴� �� ����
                break;
        }
    }
    
    //�����̴� ��ġ&�巡�׸� �̿��� �Ҹ� �̹��� ����
    public void ChangeSoundImage(string type)
    {
        switch(type)
        {
            case "BG": //�������
                if(bgSlider.value <= 0.0001f)
                {
                    bgImage.sprite = nonSoundImage; //�Ҹ� off �̹����� ����
                }
                else
                {
                    bgImage.sprite = soundImage; //�Ҹ� on �̹����� ����
                }
                break;

            case "SFX": //ȿ����
                if (sfxSlider.value == 0.0001f)
                {
                    sfxImage.sprite = nonSoundImage; //�Ҹ� off �̹����� ����
                }
                else
                {
                    sfxImage.sprite = soundImage; //�Ҹ� on �̹����� ����
                }
                break;

        }
    }

    //�ǳ� Ȱ��ȭ ���� ����
    public void InvertPanel(string type)
    {
        switch (type)
        {
            case "Level Panel": //���� �ǳ�
                levelPanel.SetActive(!levelPanel.activeSelf); //Ȱ��ȭ ���� ����
                break;

            case "Setting Panel": //���� �ǳ�
                settingPanel.SetActive(!settingPanel.activeSelf);
                break;

            case "Quit Panel": //���� �ǳ�
                quitPanel.SetActive(!quitPanel.activeSelf);
                break;

            case "Tip Panel": //���� �ǳ�
                tipPanel.SetActive(!tipPanel.activeSelf);
                break;
        }
    }

    //��������
    public void ExitGame()
    {
        Application.Quit();
    }

}
