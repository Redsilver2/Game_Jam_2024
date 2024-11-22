using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform body;

    [Space]
    [SerializeField] private float rotationSpeed = 5f;

    [Space]
    [SerializeField] private float maxRotationX = 90f;

    private float rotationX;
    private float rotationY;

    private void Update()
    {
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");

        if (axisY != 0)
        {
            rotationX += axisY * rotationSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -maxRotationX, maxRotationX); 
        }


        if (axisX != 0 && body != null)
        {
            rotationY = body.eulerAngles.y + (axisX * rotationSpeed * Time.deltaTime);
        }

    }

    private void LateUpdate()
    {
        transform.localEulerAngles = Vector3.right * rotationX;

        if(body != null)
        {
            body.eulerAngles = Vector3.up * rotationY;
        }
    }
}
