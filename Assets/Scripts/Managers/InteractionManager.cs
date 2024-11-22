using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Image interactionCrosshair;
    [SerializeField] private TextMeshProUGUI interactionNameDisplayer;

    [Space]
    [SerializeField] private float interactionRange = 10f;

    private static Dictionary<Collider, Interactable> interactableInstances = new Dictionary<Collider, Interactable>();
    public static InteractionManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Update()
    {
        CastInteractionRay();
    }

    private void CastInteractionRay()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionRange, LayerMask.GetMask("Interactable")) && hitInfo.collider != null)
        {
            Sprite interactableIcon = null;
            string interactableName = string.Empty;


            if(interactableInstances.TryGetValue(hitInfo.collider, out Interactable interactable))
            {
                interactableIcon = interactable.GetIcon();
                interactableName = interactable.GetName();

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
}
