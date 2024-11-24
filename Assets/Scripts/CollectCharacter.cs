using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollectCharacter : MonoBehaviour
{
    private PlayerController controller;
    private BoxCollider boxCollider;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.isTrigger = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.gameObject != gameObject)
        {
            boxCollider.enabled = false;
            PlayerCharacterSwap.Instance.UnlockCharacter(controller);
        }
    }

}
