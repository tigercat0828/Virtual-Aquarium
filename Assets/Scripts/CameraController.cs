using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float yaw = 0;
    float pitch = 0;
    const float roll = 0;
    public float MoveSpeed = 1.0f;
    public float TurnSpeed = 3.0f;

    public Transform target;
    public float SmoothSpeed = 0.125f;
    public float MinDistanceFromTarget;
    private float currDistanceFromTarget;

    private void Start() {
        currDistanceFromTarget = MinDistanceFromTarget;
    }
    private void Update() {
        SelectTarget();
    }
    private void FixedUpdate() {
        Move();
        FollowTarget();
    }
    private void Move() {
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.ClampMagnitude(transform.forward, MoveSpeed);
        else if (Input.GetKey(KeyCode.S))
            transform.position -= Vector3.ClampMagnitude(transform.forward, MoveSpeed);

        // Move left or right
        if (Input.GetKey(KeyCode.A))
            transform.position -= Vector3.ClampMagnitude(transform.right, MoveSpeed);
        else if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.ClampMagnitude(transform.right, MoveSpeed);

        // Move up or down
        if (Input.GetKey(KeyCode.Space))
            transform.position += new Vector3(0, MoveSpeed, 0);
        else if (Input.GetKey(KeyCode.LeftShift))
            transform.position -= new Vector3(0, MoveSpeed, 0);

        // Rotate
        pitch -= Input.GetAxis("Mouse Y") * TurnSpeed;
        pitch = Mathf.Clamp(pitch, -90, 90);
        yaw += Input.GetAxis("Mouse X") * TurnSpeed;
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void SelectTarget() {
        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit)) {
                target = hit.transform;
                if (target.CompareTag("Selectable")) {
                    Debug.Log(target.name);
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            target = null;
        }
        currDistanceFromTarget -= Input.mouseScrollDelta.y;
        if(currDistanceFromTarget < MinDistanceFromTarget) {
            currDistanceFromTarget = MinDistanceFromTarget;
        }
    }
    void FollowTarget() {
        if (target != null) {
            Vector3 TargetToCamDir = transform.position - target.position;
            Vector3 offset = TargetToCamDir.normalized * currDistanceFromTarget;
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
    }

}
