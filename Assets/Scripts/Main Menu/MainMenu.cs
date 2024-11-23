using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    [Space]
    [SerializeField] private SettingsManager settingsManager;
    private SceneLoaderManager sceneLoader;


    private void Start()
    {
        sceneLoader = SceneLoaderManager.Instance;
        settingsManager.Init();

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnClickStartButtonEvent);
        }

        if(quitButton != null)
        {
            quitButton.onClick.AddListener(OnClickQuitButtonEvent);
        }
    }

    private void OnClickStartButtonEvent()
    {
        sceneLoader.LoadSingleScene(1);
    }

    private void OnClickQuitButtonEvent()
    {
        sceneLoader.QuitApplication();
    }

    
}
