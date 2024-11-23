using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SettingsManager 
{
    [SerializeField] private Slider sensitivityXSlider;
    [SerializeField] private Slider sensitivityYSlider;
    [SerializeField] private Slider masterAudioSlider;

    [Space]
    [SerializeField] private TextMeshProUGUI sensitivityXValueDisplayer;
    [SerializeField] private TextMeshProUGUI sensitivityYValueDisplayer;
    [SerializeField] private TextMeshProUGUI masterAudioValueDisplayer;

    private const string SENSITIVITY_X_KEY = "SENSITIVITY_X_KEY";
    private const string SENSITIVITY_Y_KEY = "SENSITIVITY_Y_KEY";
    private const string MASTER_VOLUME_KEY = "MASTER_VOLUME_KEY";

    public void Init()
    {
        if(sensitivityXSlider != null)
        {
            sensitivityXSlider.minValue = 100;
            sensitivityXSlider.maxValue = 1000;
            sensitivityXSlider.wholeNumbers = true;

            sensitivityXSlider.onValueChanged.AddListener(OnSensitvityXSliderValueChangedEvent);
            sensitivityXSlider.value = PlayerPrefs.GetFloat(SENSITIVITY_X_KEY, sensitivityXSlider.minValue);
        }

        if(sensitivityYSlider != null)
        {
            sensitivityYSlider.minValue = 100;
            sensitivityYSlider.maxValue = 1000;
            sensitivityYSlider.wholeNumbers = true;

            sensitivityYSlider.onValueChanged.AddListener(OnSensitvityYSliderValueChangedEvent);
            sensitivityYSlider.value = PlayerPrefs.GetFloat(SENSITIVITY_Y_KEY, sensitivityYSlider.minValue);
        }

        if (sensitivityXSlider != null)
        {
            masterAudioSlider.minValue = 0;
            masterAudioSlider.maxValue = 1;

            masterAudioSlider.onValueChanged.AddListener(OnSensitvityXSliderValueChangedEvent);
            masterAudioSlider.value = PlayerPrefs.GetFloat(SENSITIVITY_X_KEY, sensitivityXSlider.minValue);
        }
    }

    private void OnSensitvityXSliderValueChangedEvent(float value)
    {
        if (sensitivityXValueDisplayer != null)
        {
            sensitivityXValueDisplayer.text = ((int)value).ToString();
        }

        PlayerCameraController.SetSensitvityX(value);
        PlayerPrefs.SetFloat(SENSITIVITY_X_KEY, value);
    }

    private void OnSensitvityYSliderValueChangedEvent(float value)
    {
        if (sensitivityYValueDisplayer != null)
        {
            sensitivityYValueDisplayer.text = ((int)value).ToString();
        }

        PlayerCameraController.SetSensitvityY(value);
        PlayerPrefs.SetFloat(SENSITIVITY_Y_KEY, value);
    }

    private void OnMasterAudioSliderEvent(float value)
    {
        if (masterAudioValueDisplayer != null)
        {
            masterAudioValueDisplayer.text = $"{value * 100}%";
        }

        AudioListener.volume = value;
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, value);
    }
}
