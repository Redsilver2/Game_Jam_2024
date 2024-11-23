using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCharacterSwap : MonoBehaviour
{
    [SerializeField] private Image swapBackground;
    [SerializeField] private PlayerController starterCharacterController;

    [Space]
    [SerializeField] private float swapBackgroundFadeDuration;

    private static List<PlayerCharacterData> characterDatas = new List<PlayerCharacterData>();
    private bool isSwaping = false;
    private int currentIndex = -1;

    private static UnityEvent<int> onCharacterIndexChanged = new UnityEvent<int>();
    public static PlayerCharacterSwap Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < characterDatas.Count; i++)
        {
            PlayerController controller = characterDatas[i].Controller;

            if (controller == starterCharacterController)
            {
                currentIndex = i;
                controller.enabled = true;
                break;
            }
        }

        if (swapBackground != null)
        {
            swapBackground.canvasRenderer.SetAlpha(0);
        }

        SceneLoaderManager.AddOnLoadSingleLevelEvent(OnLoadSingleLevelEvent);
    }


    private void Update()
    {
        if (!isSwaping && GetActifController().IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(SwapCharacter(0));
            }


            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(SwapCharacter(1));
            }


            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(SwapCharacter(2));
            }
        }
    }

    private IEnumerator SwapCharacter(int index)
    {
        if (index < 0)
        {
            index = 0;
        }
        else if (index >= characterDatas.Count)
        {
            index = characterDatas.Count - 1;
        }

        if (currentIndex != index && characterDatas.Count > 0 && swapBackground != null)
        {
            PlayerCharacterData currentData = characterDatas[currentIndex];

            isSwaping = true;
            currentIndex = index;

            currentData.Controller.enabled = false;
            yield return UIManager.LerpCanvasRendererAlpha(swapBackground.canvasRenderer, true, swapBackgroundFadeDuration);

            currentData.Camera.enabled = false;
            currentData = characterDatas[index];
            currentData.Camera.enabled = true;

            yield return UIManager.LerpCanvasRendererAlpha(swapBackground.canvasRenderer, false, swapBackgroundFadeDuration);
            currentData.Controller.enabled = true;
            isSwaping = false;
        }
    }

    public PlayerController[] GetPlayerControllers()
    {
        List<PlayerController> result = new List<PlayerController>();

        for (int i = 0; i < characterDatas.Count; i++)
        {
            result.Add(characterDatas[i].Controller);
        }

        return result.ToArray();
    }
    public PlayerController GetActifController() => characterDatas[currentIndex].Controller;

    private void OnLoadSingleLevelEvent()
    {
        PlayerCharacterData currentData = characterDatas[currentIndex];
        PlayerController controller = currentData.Controller;

        if (controller != null) controller.enabled = false;
    }

    public static void AddPlayerCharacterData(Camera camera, PlayerController controller)
    {
        if(camera != null && controller != null)
        {
            controller.enabled = false;
            characterDatas.Add(new PlayerCharacterData(camera, controller));
        }
    }

    private void OnDisable()
    {
        SceneLoaderManager.RemoveOnLoadSingleLevelEvent(OnLoadSingleLevelEvent);
    }
}
