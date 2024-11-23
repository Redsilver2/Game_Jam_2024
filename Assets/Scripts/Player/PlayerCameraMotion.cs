using UnityEngine;

[System.Serializable]
public class PlayerCameraMotion 
{
    [SerializeField] private float maxPositionX;
    [SerializeField] private float minPositionX;

    [Space]
    [SerializeField] private float maxPositionY;
    private Vector2 defaultPosition;

    [Space]
    [SerializeField] private float headbobWalkingSpeed;
    [SerializeField] private float headbobRunningSpeed;


    private float currentPositionX;
    private float currentPositionY;

    private Transform transform;

    public void Init(Transform playerTransform, Transform transform)
    {
        defaultPosition  = transform.position;
        currentPositionX = defaultPosition.x;
        currentPositionY = defaultPosition.y;
        PlayerController.AddOnMovementMotionChangedEvent(OnMovementMotionChangedEvent);
    }

    private void OnMovementMotionChangedEvent(PlayerController controller, Vector2 motion)
    {
        float desiredPositionX   = defaultPosition.x;
        float desiredPositionY   = defaultPosition.y;
        float desiredHeabobSpeed = headbobWalkingSpeed;

        if (controller.IsRunning)
        {
            desiredHeabobSpeed = headbobRunningSpeed;
        }


        if (motion.magnitude > 0.1f)
        {
            //desiredPositionX = Mathf
        }
    }


}
