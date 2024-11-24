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
            MeshRenderer meshRenderer  = controller.MeshRenderer;

            if (controller == starterCharacterController)
            {

                currentIndex = i;
                characterDatas[i].SetIsCharacterUnlocked(true);

                controller.enabled   = true;
                meshRenderer.enabled = false;
                continue;
            }

            controller.enabled = false;
            meshRenderer.enabled = true;
        }

        if (swapBackground != null)
        {
            swapBackground.canvasRenderer.SetAlpha(0);
            swapBackground.enabled = false;
        }

        SceneLoaderManager.AddOnLoadSingleLevelEvent(OnLoadSingleLevelEvent);
    }

    public void UnlockCharacter(PlayerController controller)
    {
        for (int i = 0; i < characterDatas.Count; i++)
        {
            PlayerController _controller = characterDatas[i].Controller;

            if (controller == _controller)
            {
        
                characterDatas[i].SetIsCharacterUnlocked(true);
                Debug.LogWarning(controller.name + " Unlocked " + characterDatas[i].IsCharacterUnlocked);
                break;
            }
        }
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
        Debug.LogWarning("1");
        if (currentIndex != index && characterDatas.Count > 0 && swapBackground != null)
        {
            Debug.LogWarning("2");
            if (index >= 0 && index < characterDatas.Count)
            {
                Debug.LogWarning("3");
                if (characterDatas[index].IsCharacterUnlocked)
                {
                    Debug.LogWarning("4");
                    PlayerCharacterData currentData = characterDatas[currentIndex];
                    swapBackground.enabled = true;

                    isSwaping = true;
                    currentIndex = index;

                    currentData.Controller.enabled = false;
                    yield return UIManager.LerpCanvasRendererAlpha(swapBackground.canvasRenderer, true, swapBackgroundFadeDuration);

                    currentData.Controller.MeshRenderer.enabled = true;
                    currentData.Camera.enabled = false;
                    currentData = characterDatas[index];
                    currentData.Camera.enabled = true;
                    currentData.Controller.MeshRenderer.enabled = false;

                    yield return UIManager.LerpCanvasRendererAlpha(swapBackground.canvasRenderer, false, swapBackgroundFadeDuration);
                    currentData.Controller.enabled = true;

                    isSwaping = false;
                    swapBackground.enabled = false;
                }
            }
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

    public static void AddPlayerCharacterData(Camera camera, PlayerController controller, bool isUnlocked)
    {
        if(camera != null && controller != null)
        {
            characterDatas.Add(new PlayerCharacterData(camera, controller, isUnlocked));
        }
    }

    private void OnDisable()
    {
        SceneLoaderManager.RemoveOnLoadSingleLevelEvent(OnLoadSingleLevelEvent);
    }
}
