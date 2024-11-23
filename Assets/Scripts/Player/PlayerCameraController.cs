using UnityEngine;


public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform body;

    [Space]
    [SerializeField] private float maxRotationX = 90f;

    [Space]
    [SerializeField] private PlayerCameraMotion cameraMotion;

    private float rotationX;
    private float rotationY;

    private static float sensitivityX = 500f;
    private static float sensitivityY = 500f;

    private void Start()
    {
        cameraMotion.Init(body.GetComponent<PlayerController>());
    }

    private void Update()
    {
        float axisX = Input.GetAxis("Mouse X") * sensitivityX;
        float axisY = Input.GetAxis("Mouse Y") * sensitivityY;

        if (axisY != 0)
        {
            rotationX += -axisY * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -maxRotationX, maxRotationX);
        }

        if (axisX != 0 && body != null)
        {
            rotationY = body.eulerAngles.y + (axisX * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = Vector3.right * rotationX;

        if (body != null)
        {
            body.eulerAngles = Vector3.up * rotationY;
        }
    }

    public static void SetSensitvityX(float value)
    {
        sensitivityX = value;
    }
    public static void SetSensitvityY(float value)
    {
        sensitivityY = value;
    }

}
