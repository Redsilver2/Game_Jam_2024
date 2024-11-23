using UnityEngine;

sealed class PearController : PlayerController
{
    [SerializeField] private float pushForce = 100;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        float magnitude = character.velocity.magnitude;
        Debug.Log(magnitude);

        if (IsRunning && magnitude > 0f)
        {
            if (hit.collider.TryGetComponent(out IBreakable breakable))
            {
                breakable.Break();
            }
            else if (hit.collider.TryGetComponent(out IPushable pushable))
            {
                pushable.Push(transform.forward, magnitude * pushForce);
            }
        }
    }
}
