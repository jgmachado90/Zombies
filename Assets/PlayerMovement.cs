using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;


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
            moving = true; 
            Rotate();
            transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y + verticalMove, transform.position.z);
        }
        else
        {
            moving = false;
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
