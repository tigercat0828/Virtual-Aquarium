using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float pitch;
    private float yaw;

    public float DistanceFromTarget = 3;
    public float MoveSpeed;
    public float TurnSpeed;
    
    private Transform focusTarget;
    private bool hasFocusTarget;
    [SerializeField]
    private string targetName;
    private void Start() {
        Init();
    }
    private void Update() {
        HandleMove();
        HandleTurn();
        LockTarget();
        if (hasFocusTarget) {
            FollowTarget();
        }
        
    }
    private void Init() {
        pitch = transform.localEulerAngles.x;
        yaw = transform.localEulerAngles.y;
        
        Cursor.lockState = CursorLockMode.Locked;

        focusTarget = null;
        hasFocusTarget = false;
        targetName = "-";
    }
    private void HandleMove() {
        float moveSpeed = MoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) {
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.position -= new Vector3(transform.forward.x, 0, transform.forward.z) * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.position -= transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position += transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            transform.position += Vector3.down * moveSpeed;
        }
        if (Input.GetKey(KeyCode.Space)) {
            transform.position += Vector3.up * moveSpeed;
        }
    }
    private void HandleTurn() {
        pitch -= Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        yaw += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
    private void LockTarget() {
        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit)) {
                focusTarget = hit.transform;
                if (focusTarget.CompareTag("Selectable")) {
                    hasFocusTarget = true;
                }
                else {
                    hasFocusTarget = false;
                    focusTarget = null;
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            hasFocusTarget = false;
            focusTarget = null;
        }

        DebugDisplayTargetName();
    }

    private void FollowTarget() {
        Vector3 toTargetDir = focusTarget.transform.position - transform.position;
        Vector3 fixedOffset = toTargetDir.normalized * DistanceFromTarget;
        transform.position = focusTarget.position - fixedOffset;
    }
    private void DebugDisplayTargetName() {
        if (focusTarget != null) {
            targetName = focusTarget.name;
        }
        else {
            targetName = "-";
        }
    }
    
}
