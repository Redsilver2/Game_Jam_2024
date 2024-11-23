using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] protected string interactableName;
    [SerializeField] private   Sprite   interactableIcon;


    private UnityEvent       onInteractOnce;
    private UnityEvent<bool> onInteract;

    private bool hasInteractedOnce = false;
    private bool interactionState = false;
    
    protected virtual void Awake()
    {
        onInteractOnce = new UnityEvent();
        onInteract     = new UnityEvent<bool>();
        InteractionManager.AddInteractableInstance(GetComponent<Collider>(), this);
    }

    public virtual Sprite GetIcon() => interactableIcon;
    public virtual string GetName() => interactableName;
    public virtual void Interact()
    {
        interactionState = !interactionState;

        if (hasInteractedOnce) 
        {
            hasInteractedOnce = true;
            onInteractOnce.Invoke();
        }

        onInteract.Invoke(interactionState);
    }

    public void AddOnInteractOnceEvent(UnityAction action)
    {
        onInteractOnce.AddListener(action);
    }
    public void RemoveOnInteractOnceEvent(UnityAction action)
    {
        onInteractOnce.RemoveListener(action);
    }

    public void AddOnInteract(UnityAction<bool> action)
    {
        onInteract.AddListener(action);
    }
    public void RemoveOnInteract(UnityAction<bool> action)
    {
        onInteract.RemoveListener(action);
    }
}