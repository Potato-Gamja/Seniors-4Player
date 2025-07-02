using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource SFX_AudioSource;
    [SerializeField] AudioSource BGM_AudioSource;
    [SerializeField] AudioClip victoryAudioClip;

    public void SFX_Play(AudioClip audioClip)
    {
        SFX_AudioSource.clip= audioClip;
        SFX_AudioSource.pitch = Random.Range(0.9f, 1.1f);
        SFX_AudioSource.Play();
    }

    public void VictorySFX_Play()
    {
        SFX_AudioSource.clip = victoryAudioClip;
        SFX_AudioSource.Play();
        BGM_AudioSource.Pause();
    }
}
