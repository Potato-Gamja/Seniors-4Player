using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    public Slider BGM_Slider;
    public Slider SFX_Slider;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("BGM"))
        {
            BGM_Slider.value = PlayerPrefs.GetFloat("BGM");
        }
        if (PlayerPrefs.HasKey("SFX"))
        {
            SFX_Slider.value = PlayerPrefs.GetFloat("SFX");
        }

    }

    void Start()
    {
        BGM_Slider.onValueChanged.AddListener(OnBGMChanged);
        SFX_Slider.onValueChanged.AddListener(OnSFXChanged);

        ApplySavedVolume();
    }

    public void OnBGMChanged(float value)
    {
        PlayerPrefs.SetFloat("BGM", value);
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    public void OnSFXChanged(float value)
    {
        PlayerPrefs.SetFloat("SFX", value);
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    void ApplySavedVolume()
    {
        OnBGMChanged(BGM_Slider.value);
        OnSFXChanged(SFX_Slider.value);
    }
}
