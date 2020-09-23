using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float yaw = 0;
    float pitch = 0;
    const float roll = 0;
    public float moveSpeed = 1.0f;
    public float turnSpeed = 3.0f;

    void FixedUpdate() {
        // Move forward or backward
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.ClampMagnitude(transform.forward, moveSpeed);
        else if (Input.GetKey(KeyCode.S))
            transform.position -= Vector3.ClampMagnitude(transform.forward, moveSpeed);

        // Move left or right
        if (Input.GetKey(KeyCode.A))
            transform.position -= Vector3.ClampMagnitude(transform.right, moveSpeed);
        else if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.ClampMagnitude(transform.right, moveSpeed);

        // Move up or down
        if (Input.GetKey(KeyCode.Space))
            transform.position += new Vector3(0, moveSpeed, 0);
        else if (Input.GetKey(KeyCode.LeftShift))
            transform.position -= new Vector3(0, moveSpeed, 0);

        // Rotate
        pitch -= Input.GetAxis("Mouse Y") * turnSpeed;
        pitch = Mathf.Clamp(pitch, -90, 90);
        yaw += Input.GetAxis("Mouse X") * turnSpeed;
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
