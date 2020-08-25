using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;

    public Animator playerAnimator;

    float horizontalMove;
    float verticalMove;
    public float runSpeed;
    public float rotationSpeed;

    public bool moving;

    private void FixedUpdate()
    {
        horizontalMove = joystick.Horizontal * runSpeed * Time.deltaTime;
        verticalMove = joystick.Vertical * runSpeed * Time.deltaTime;
        if (horizontalMove != 0 || verticalMove != 0)
        {
            if (!moving)
            {
                PlayAnimation("walk");
                moving = true;

            }

            Rotate();
            transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y + verticalMove, transform.position.z);
        }
        else
        {
            if (moving)
            {
                moving = false;
                PlayAnimation("idle");
            }
        }
       
    }

    private void PlayAnimation(string animationName)
    {
        playerAnimator.SetBool("Sprint", false);

        if(animationName == "walk")
        {
            Debug.Log("playing animation");
            playerAnimator.SetBool("Moving", true);
            playerAnimator.SetBool("Sprint", true);
        }
        if(animationName == "idle")
        {
            return;
        }

    }

    private void Rotate()
    {
        Vector3 vectorToTarget = new Vector3(transform.position.x + horizontalMove, transform.position.y + verticalMove, transform.position.z) - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }
}
