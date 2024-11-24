using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class BlueberryController : PlayerController {


    [Space]
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float jumpForce = 5f;
    private bool canJump = true;

    protected override void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && IsGrounded)
        {
            StartCoroutine(JumpCoroutine());
        }

        base.LateUpdate();
    }

    private IEnumerator JumpCoroutine()
    {
        float t = 0f;
        canJump = false;
        canFall = false;

        while (t < jumpTime)
        {
            character.Move(transform.up * jumpForce * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        canFall = true;
        canJump = true;
    }

}
