using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("Base Settings")]
    [SerializeField] private PlayerCameraController ownerCameraController;
    [SerializeField] private CharacterType characterType;

    [Space]
    [Header("Movement Settings")]
    [SerializeField] private int walkMovementSpeed;
    [SerializeField] private int runMovementSpeed;
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

    [Space]
    [SerializeField] private float jumpTime  = 0.5f;
    [SerializeField] private float jumpForce = 5f;

    private bool canJump = true;

    private bool isRunning = false;
    private bool isGrounded = false;

    private bool canPlayLandingSound = false;
    private float airTime = 0f;


    private string groundTag = string.Empty;
    private float currentMovementSpeed = 0;
    private float currentGravitySpeed = 0;


    private Vector2 inputMotion;
    private CharacterController character;

    private UnityEvent<bool> onStateChanged;
    private static UnityEvent<PlayerController, Vector2> onMovementMotionChanged = new UnityEvent<PlayerController, Vector2>();

    public bool IsRunning => isRunning;
    public PlayerCameraController CameraController => ownerCameraController;
    public CharacterType CharacterType => characterType;

    private void OnValidate()
    {
        footstepAudioHandler.Validate();
    }


    private void Awake()
    {
        character = GetComponent<CharacterController>();
        onStateChanged = new UnityEvent<bool>();

        currentMovementSpeed = walkMovementSpeed;
        currentGravitySpeed = defaultGravity;

        if (ownerCameraController != null)
        {
            PlayerCharacterSwap.AddPlayerCharacterData(ownerCameraController.GetComponent<Camera>(), this);
            AddOnStateChangedEvent(isEnabled =>
            {
                ownerCameraController.enabled = isEnabled;
            });
        }
    }

    private void Update()
    {
        isGrounded = IsGrounded(out groundTag);
        inputMotion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isRunning = isGrounded && Input.GetKey(KeyCode.LeftShift);
    }

    private void LateUpdate()
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
                   // footstepAudioHandler.PlayMovementSound(groundTag, isRunning);
                }
                else
                {
                   // footstepAudioHandler.PlayLandingSound(groundTag);
                    canPlayLandingSound = false;
                    airTime = 0f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                Debug.Log("Jumping");
                StartCoroutine(JumpCoroutine());
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


        if (canJump)
        {
            currentGravitySpeed = Mathf.Lerp(currentGravitySpeed, desiredGravitySpeed, gravityMovementSpeed * Time.deltaTime);
        }

        character.Move((transform.forward * inputMotion.y * currentMovementSpeed
                      + transform.right * inputMotion.x * currentMovementSpeed
                      + -transform.up * currentGravitySpeed) * Time.deltaTime);


        onMovementMotionChanged.Invoke(this, inputMotion);
    }
    private bool IsGrounded(out string groundTag)
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, groundCheckRayLenght, LayerMask.GetMask("Ground")) && hitInfo.collider != null)
        {
            groundTag = hitInfo.collider.tag;
            return true;
        }

        groundTag = string.Empty;
        return false;
    }

    private IEnumerator JumpCoroutine()
    {
        float t = 0f;
        canJump = false;
        currentGravitySpeed = 0f;

        while (t < jumpTime)
        {
            character.Move(transform.up * jumpForce);
            t += Time.deltaTime;
            yield return null;
        }

        canJump = true;
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
