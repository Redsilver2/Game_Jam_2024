using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    private Image interactionCrosshair;
    private TextMeshProUGUI interactionNameDisplayer;
    private float interactionRange = 10f;

    public float PushForce { get; private set; }
    private static Dictionary<Collider, Interactable> interactableInstances = new Dictionary<Collider, Interactable>();

    public void Update()
    {
        CastInteractionRay();
    }


    public void Init(float interactionRange, float pushForce, Image crosshair, TextMeshProUGUI nameDisplayer)
    {
        this.interactionRange         = interactionRange;
        this.PushForce                = pushForce;
        this.interactionCrosshair     = crosshair;
        this.interactionNameDisplayer = nameDisplayer;
    }

    private void CastInteractionRay()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionRange, LayerMask.GetMask("Interactable")) && hitInfo.collider != null)
        {
            Sprite interactableIcon = null;
            string interactableName = string.Empty;


            if(interactableInstances.TryGetValue(hitInfo.collider, out Interactable interactable))
            {
                interactableIcon = interactable.GetIcon();
                interactableName = interactable.GetName();

                Debug.Log(interactableName);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }

            SetInteractionUI(interactableName, interactableIcon);
        }
    }
    private void SetInteractionUI(string name, Sprite icon)
    {
        if(interactionNameDisplayer != null)
        {
            interactionNameDisplayer.text = name;
        }

        if(interactionCrosshair != null)
        {
            if(icon == null)
            {
                interactionCrosshair.enabled = false;
                interactionCrosshair.sprite  = null;
            }
            else
            {
                interactionCrosshair.sprite  = icon;
                interactionCrosshair.enabled = true;
            }
        }
    }
    
    public static void AddInteractableInstance(Collider collider, Interactable interactable)
    {
        if(collider != null && !interactableInstances.ContainsKey(collider))
        {
            interactableInstances.Add(collider, interactable);
        }
    }
    public static void RemoveInteractableInstance(Collider collider)
    {
        if (interactableInstances.ContainsKey(collider))
        {
            interactableInstances.Remove(collider);
        }
    }

    private void OnDisable()
    {
        if (interactionCrosshair) interactionCrosshair.enabled = false;
        if (interactionNameDisplayer) interactionNameDisplayer.enabled = false;
    }

    private void OnEnable()
    {
        if (interactionCrosshair) interactionCrosshair.enabled = true;
        if (interactionNameDisplayer) interactionNameDisplayer.enabled = true;
    }
}
