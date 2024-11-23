using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderManager : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private float fadeBackgroundDuration = 5f;

    public static UnityEvent OnLoadSingleLevel = new UnityEvent();

    public static bool IsLoadingSingleScene { get; private set; }
    public static SceneLoaderManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (background)
        {
            background.canvasRenderer.SetAlpha(0);
        }

        LoadSingleScene(1);
    }

    public void LoadSingleScene(int index)
    {

        if (IsLoadingSingleScene) return;

        if (index < 0)
        {
            index = 0;
        }
        else if (index >= SceneManager.sceneCountInBuildSettings)
        {
            index = SceneManager.sceneCountInBuildSettings - 1;
        }

        StartCoroutine(LoadSingleSceneCoroutine(index));
    }

    public void QuitApplication()
    {
        if (IsLoadingSingleScene) return;
        StartCoroutine(QuitApplicationCoroutine());
    }

    private IEnumerator LoadSingleSceneCoroutine(int index)
    {
        IsLoadingSingleScene = true;
        OnLoadSingleLevel.Invoke();

        yield return UIManager.LerpCanvasRendererAlpha(background.canvasRenderer, true, fadeBackgroundDuration);

        AsyncOperation operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f) { yield return null; }

        operation.allowSceneActivation = true;
        yield return UIManager.LerpCanvasRendererAlpha(background.canvasRenderer, false, fadeBackgroundDuration);

        IsLoadingSingleScene = false;
    }

    private IEnumerator QuitApplicationCoroutine()
    {
        IsLoadingSingleScene = true;
        yield return UIManager.LerpCanvasRendererAlpha(background.canvasRenderer, true, fadeBackgroundDuration);
        Application.Quit();
    }

    public static void AddOnLoadSingleLevelEvent(UnityAction action) { OnLoadSingleLevel.AddListener(action); }
    public static void RemoveOnLoadSingleLevelEvent(UnityAction action) { OnLoadSingleLevel.RemoveListener(action); }

}
