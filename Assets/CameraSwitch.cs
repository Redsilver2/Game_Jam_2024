using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera[] thirdPersCams;

    private int currentIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        thirdPersCams[currentIndex].gameObject.SetActive(false);

        currentIndex = (currentIndex + 1) % thirdPersCams.Length;

        SetActiveCam(currentIndex);
    }

    void SetActiveCam(int index)
    {
        thirdPersCams[index].gameObject.SetActive(true);
    }
}

