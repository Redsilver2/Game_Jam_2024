using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public abstract class PlayerController : MonoBehaviour
{

    [Header("Base Settings")]
    [SerializeField] protected PlayerCameraController ownerCameraController;

    [Space]
    [Header("Movement Settings")]
    [SerializeField] private int   walkMovementSpeed;
    [SerializeField] private int   runMovementSpeed;
    [SerializeField] private float gravityMovementSpeed;

    [Space]
    [SerializeField] private float movementTransitionSpeed;

    [Space]
    [Header("Gravity Settings")]
    [SerializeField] private float defaultGravity;
    [SerializeField] private float fallingGravity;

    [Space]
    [SerializeField] private float gravityTransitionSpeed;

    [Space]
    [SerializeField] private float groundCheckRayLenght = 10f;

    [Space]
    [SerializeField] private FootstepAudioHandler footstepAudioHandler;


    [Space]
    [SerializeField] private float maxAirTime = 0.1f;
    protected bool canFall = true;

    private bool isRunning = false;
    private bool isGrounded = false;

    private bool canPlayLandingSound = false;
    private float airTime = 0f;


    private GameObject ground;
    private float currentMovementSpeed = 0;
    private float currentGravitySpeed = 0;


    private MeshRenderer meshRenderer;
    private Vector2 inputMotion;
    protected CharacterController character;

    private UnityEvent<bool> onStateChanged;
    private static UnityEvent<PlayerController, Vector2> onMovementMotionChanged = new UnityEvent<PlayerController, Vector2>();

    public bool IsRunning => isRunning;
    public bool IsGrounded => isGrounded;

    public MeshRenderer MeshRenderer => meshRenderer;
    public PlayerCameraController CameraController => ownerCameraController;

    private void OnValidate()
    {
        footstepAudioHandler.Validate();
    }


    protected virtual void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        character = GetComponent<CharacterController>();
        onStateChanged = new UnityEvent<bool>();

        currentMovementSpeed = walkMovementSpeed;
        currentGravitySpeed = defaultGravity;

        if (ownerCameraController != null)
        {
            PlayerCharacterSwap.AddPlayerCharacterData(ownerCameraController.GetComponent<Camera>(), this);

            if (ownerCameraController != null)
            {
                AddOnStateChangedEvent(isEnabled =>
                {
                    meshRenderer.enabled = !isEnabled;
                    ownerCameraController.enabled = isEnabled;
                });
            }
        }
    }

    private void Update()
    {
        isGrounded = GetGroundedState(out ground);
        inputMotion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isRunning = isGrounded && Input.GetKey(KeyCode.LeftShift);
    }

    protected virtual void LateUpdate()
    {
        float desiredMovementSpeed = walkMovementSpeed;
        float desiredGravitySpeed = defaultGravity;

        if (isGrounded)
        {
            if (isRunning)
            {
                desiredMovementSpeed = runMovementSpeed;
            }

            if (character.velocity.magnitude > 0.1f)
            {
                if (!canPlayLandingSound)
                {
                    footstepAudioHandler.PlayMovementSound(ground, isRunning);
                }
                else
                {
                    footstepAudioHandler.PlayLandingSound(ground);
                    canPlayLandingSound = false;
                    airTime = 0f;
                }
            }
        }
        else
        {
            if (airTime >= maxAirTime)
            {
                airTime = maxAirTime;
                canPlayLandingSound = true;
            }

            airTime += Time.deltaTime;

            desiredMovementSpeed = gravityMovementSpeed;
            desiredGravitySpeed = fallingGravity;
        }

        currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, desiredMovementSpeed, movementTransitionSpeed * Time.deltaTime);
        currentGravitySpeed  = Mathf.Lerp(currentGravitySpeed, desiredGravitySpeed, gravityMovementSpeed * Time.deltaTime);

        character.Move((transform.forward * inputMotion.y * currentMovementSpeed
                      + transform.right * inputMotion.x * currentMovementSpeed
                      +  -transform.up * (canFall ? currentGravitySpeed : 0f)) * Time.deltaTime);


        onMovementMotionChanged.Invoke(this, inputMotion);
    }
    private bool GetGroundedState(out GameObject ground)
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, groundCheckRayLenght, LayerMask.GetMask("Ground")) && hitInfo.collider != null)
        {
            ground = hitInfo.collider.gameObject;
            return true;
        }

        ground = null;
        return false;
    }

    public void AddOnStateChangedEvent(UnityAction<bool> action)
    {
        onStateChanged.AddListener(action);
    }
    public void RemoveOnStateChangedEvent(UnityAction<bool> action)
    {
        onStateChanged.RemoveListener(action);
    }



    public static void AddOnMovementMotionChangedEvent(UnityAction<PlayerController, Vector2> action)
    {
        onMovementMotionChanged.AddListener(action);
    }
    public static void RemoveOnMovementMotionChangedEvent(UnityAction<PlayerController, Vector2> action)
    {
        onMovementMotionChanged.RemoveListener(action);
    }


    private void OnDisable()
    {
        onStateChanged.Invoke(false);
    }

    private void OnEnable()
    {
        onStateChanged.Invoke(true);
    }
}
