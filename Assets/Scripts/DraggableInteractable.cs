using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableInteractable : Interactable, IPushable
{
    [Space]
    [SerializeField] private float positionZOffset = 3;
    private Rigidbody rigidbody;

    protected override void Awake()
    {
        base.Awake();
        //PearCameraController cameraController = null;

        InteractionManager interactionManager = null;

        foreach(PlayerController controller in PlayerCharacterSwap.Instance.GetPlayerControllers())
        {
            PlayerCameraController cameraController = controller.CameraController;
            interactionManager                      = cameraController.GetComponent<InteractionManager>();
            
            if(interactionManager != null)
            {
                break;
            }
        }


        rigidbody = GetComponent<Rigidbody>();


        AddOnInteract(interactionState =>
        {
            if (interactionManager != null)
            {
                if (interactionState)
                {
                    rigidbody.useGravity = false;
                    rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                   
                    transform.SetParent(interactionManager.transform);
                    transform.localPosition = Vector3.forward * positionZOffset;
                }
                else
                {
                    rigidbody.useGravity = true;
                    rigidbody.constraints = RigidbodyConstraints.None;

                    rigidbody.AddForce(transform.forward * interactionManager.PushForce);
                    transform.SetParent(null);
                }
            }
        });
    }

    public void Push(Vector3 direction, float force)
    {
        rigidbody.AddForce(direction * force);
    }

    private IEnumerator AvoidClippingCoroutine()
    {
        while (true)
        {
            yield return null;
        }
    }
}
