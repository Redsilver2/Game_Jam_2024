using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] protected string interactableName;
    [SerializeField] private Sprite   interactableIcon;


    private UnityEvent onInteractOnce;
    private UnityEvent onInteract;

    private bool hasInteractedOnce = false;
    
    private void Awake()
    {
        onInteractOnce = new UnityEvent();
        onInteract     = new UnityEvent();
        InteractionManager.AddInteractableInstance(GetComponent<Collider>(), this);
    }

    public virtual Sprite GetIcon() => interactableIcon;
    public virtual string GetName() => interactableName;
    public virtual void Interact()
    {
        if (hasInteractedOnce) 
        {
            hasInteractedOnce = true;
            onInteractOnce.Invoke();
        }

        onInteract.Invoke();
    }

    public void AddOnInteractOnceEvent(UnityAction action)
    {
        onInteractOnce.AddListener(action);
    }
    public void RemoveOnInteractOnceEvent(UnityAction action)
    {
        onInteractOnce.RemoveListener(action);
    }

    public void AddOnInteract(UnityAction action)
    {
        onInteract.AddListener(action);
    }
    public void RemoveOnInteract(UnityAction action)
    {
        onInteract.RemoveListener(action);
    }
}
