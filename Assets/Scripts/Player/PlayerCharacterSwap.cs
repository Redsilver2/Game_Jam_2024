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

    private void Start()
    {
        for (int i = 0; i < characterDatas.Count; i++)
        {
            PlayerCharacterData currentData = characterDatas[i];
            PlayerController controller = currentData.Controller;
            controller.enabled = false;

            if (controller == starterCharacterController)
            {
                currentIndex = i;
                controller.enabled = true;
                continue;
            }
        }

        if (swapBackground != null)
        {
            swapBackground.canvasRenderer.SetAlpha(0);
        }

        //SceneLoaderManager.AddOnLoadSingleLevelEvent();
    }


    private void Update()
    {
        if (!isSwaping)
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
            characterDatas.Add(new PlayerCharacterData(camera, controller));
        }
    }


}
