using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer;
    [SerializeField] private Slider slider;
    private float slidervalueOld;
    private float currentslidervalue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SavedMasterVolume", 100);
    }

    private void Update()
    {
        SetVolume(slider.value);
    }

    private void SetVolume(float value)
    {
        if (slider.value == 0)
        {
            audiomixer.SetFloat("MasterVolume", -80f);
            PlayerPrefs.SetFloat("SavedMasterVolume", value);
        }
        else
        {
            PlayerPrefs.SetFloat("SavedMasterVolume", value);
            audiomixer.SetFloat("MasterVolume", Mathf.Log10((value *= 100) / 100) * 40);
        }
    }
}
