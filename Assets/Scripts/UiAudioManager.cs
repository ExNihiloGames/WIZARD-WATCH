using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIAudioManager : MonoBehaviour
{

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;

    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI effectsSliderText;

    private void OnAwake()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVol");
        effectsSlider.value = PlayerPrefs.GetFloat("effectsVol");
    }

    public void OnMusicSliderValChange(float value)
    {
        PlayerPrefs.SetFloat("musicVol", value);
        musicSliderText.text = value.ToString();
    }

    public void OnEffectsSliderValChange(float value)
    {
        PlayerPrefs.SetFloat("effectsVol", value);
        effectsSliderText.text = value.ToString();
    }
}
