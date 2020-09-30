using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI() {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360,fov.ViewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.ViewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(+fov.ViewAngle / 2, false);

        Vector3 fovPosition = fov.transform.position;
        Handles.DrawLine(fovPosition, fovPosition + viewAngleA * fov.ViewRadius);
        Handles.DrawLine(fovPosition, fovPosition + viewAngleB * fov.ViewRadius);

        Handles.color = Color.red; 
        foreach(Transform target in fov.VisibleTargetList) {
            Handles.DrawLine(fovPosition, target.position);
        }
    }
}
