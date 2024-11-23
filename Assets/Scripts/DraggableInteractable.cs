using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableInteractable : Interactable
{
    private PearCameraController cameraController;

    protected override void Awake()
    {
        base.Awake();

        foreach(PlayerController controller in PlayerCharacterSwap.Instance.GetPlayerControllers()) 
        {
            cameraController = (PearCameraController)controller.CameraController;

            if(cameraController != null && controller.CharacterType == CharacterType.Pear)
            {
                break;
            }
        }
    }
}
