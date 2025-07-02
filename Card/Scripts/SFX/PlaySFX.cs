using UnityEngine;
using UnityEngine.Events;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;

    public AudioClip filpAudio; //����� �ҽ�
    public AudioClip getAudio; //����� �ҽ�

    private void Start()
    {
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    //���� ����� �ҽ� ���� ��ġ�� ������ ����
    public void SelectSoundPlay()
    {
        soundManager.SFX_Play(filpAudio);
    }

    //���� ��� ����� �ҽ� ���� ��ġ�� ������ ����
    public void CancelSoundPlay()
    {
        soundManager.SFX_Play(filpAudio);
    }

    public void GetSoundPlay()
    {
        soundManager.SFX_Play(getAudio);
    }
}
