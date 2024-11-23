using UnityEngine;

[System.Serializable]
public class PlayerCameraMotion 
{
    [SerializeField] private Transform transform;

    [Space]
    [SerializeField] private float maxPositionX;
    [SerializeField] private float minPositionX;

    [Space]
    [SerializeField] private float maxPositionY;

    [Space]
    [SerializeField] private float headbobWalkingSpeed;
    [SerializeField] private float headbobRunningSpeed;

    [Space]
    [SerializeField] private float headbobTransitionSpeed;

    private Vector2 defaultPosition;
    private PlayerController ownerPlayerController;

    private float currentPositionX;
    private float currentPositionY;
    private float currentHeadbobSpeed;

    public void Init(PlayerController controller)
    {
        defaultPosition  = transform.localPosition;
        currentPositionX = defaultPosition.x;
        currentPositionY = defaultPosition.y;
        ownerPlayerController = controller;
        PlayerController.AddOnMovementMotionChangedEvent(OnMovementMotionChangedEvent);
    }

    private void OnMovementMotionChangedEvent(PlayerController controller, Vector2 motion)
    {
        if (controller == ownerPlayerController && transform != null)
        {
            float desiredPositionX = defaultPosition.x;
            float desiredPositionY = defaultPosition.y;
            float desiredHeabobSpeed = headbobWalkingSpeed;

            if (controller.IsRunning)
            {
                desiredHeabobSpeed = headbobRunningSpeed;
            }

            currentHeadbobSpeed = Mathf.Lerp(currentHeadbobSpeed, desiredHeabobSpeed, headbobTransitionSpeed * Time.deltaTime);

            if (motion.magnitude > 0.1f)
            {
                float sin = Mathf.Sin(currentHeadbobSpeed * Time.time);
                desiredPositionX = Mathf.Lerp(minPositionX, maxPositionX, Mathf.Abs(sin)) * sin;
                desiredPositionY = Mathf.Lerp(defaultPosition.y, maxPositionY, Mathf.Abs(sin));
            }

            currentPositionX = Mathf.Lerp(transform.localPosition.x, desiredPositionX,  Time.deltaTime);
            currentPositionY = Mathf.Lerp(transform.localPosition.y, desiredPositionY, Time.deltaTime);

            transform.localPosition = Vector3.right * currentPositionX
                                    + Vector3.up    * currentPositionY; 
        }
    }


}
