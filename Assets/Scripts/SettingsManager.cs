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
            sensitivityXSlider.minValue = 1;
            sensitivityXSlider.maxValue = 10;
            sensitivityXSlider.wholeNumbers = true;

            sensitivityXSlider.value = PlayerPrefs.GetFloat(SENSITIVITY_X_KEY, sensitivityXSlider.minValue);
            sensitivityXSlider.onValueChanged.AddListener(OnSensitvityXSliderValueChangedEvent);

            if (sensitivityXValueDisplayer != null)
            {
                sensitivityXValueDisplayer.text = ((int)sensitivityXSlider.value * 10).ToString();
            }
        }

        if(sensitivityYSlider != null)
        {
            sensitivityYSlider.minValue = 1;
            sensitivityYSlider.maxValue = 10;
            sensitivityYSlider.wholeNumbers = true;

            sensitivityYSlider.value = PlayerPrefs.GetFloat(SENSITIVITY_Y_KEY, sensitivityYSlider.minValue);
            sensitivityYSlider.onValueChanged.AddListener(OnSensitvityYSliderValueChangedEvent);

            if (sensitivityYValueDisplayer != null)
            {
                sensitivityYValueDisplayer.text = ((int)sensitivityYSlider.value * 10).ToString();
            }
        }

        if (masterAudioSlider != null)
        {
            masterAudioSlider.minValue = 0;
            masterAudioSlider.maxValue = 1;

            masterAudioSlider.value = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, masterAudioSlider.maxValue);
            masterAudioSlider.onValueChanged.AddListener(OnMasterAudioSliderEvent);

            if (masterAudioValueDisplayer != null)
            {
                masterAudioValueDisplayer.text = $"{(int)(masterAudioSlider.value * 100)}%";
            }

        }
    }

    private void OnSensitvityXSliderValueChangedEvent(float value)
    {
        if (sensitivityXValueDisplayer != null)
        {
            sensitivityXValueDisplayer.text = ((int)value * 10).ToString();
        }

        PlayerCameraController.SetSensitvityX(value);
        PlayerPrefs.SetFloat(SENSITIVITY_X_KEY, value);
    }

    private void OnSensitvityYSliderValueChangedEvent(float value)
    {
        if (sensitivityYValueDisplayer != null)
        {
            sensitivityYValueDisplayer.text = ((int)value * 10).ToString();
        }

        PlayerCameraController.SetSensitvityY(value);
        PlayerPrefs.SetFloat(SENSITIVITY_Y_KEY, value);
    }

    private void OnMasterAudioSliderEvent(float value)
    {
        if (masterAudioValueDisplayer != null)
        {
            masterAudioValueDisplayer.text = $"{(int)(value * 100)}%";
        }

        AudioListener.volume = value;
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, value);
    }
}
