using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Agent))]
public class AgentEditor : Editor
{
    

    public void OnSceneGUI() {
        Agent c = (Agent)target;
        Handles.color = Color.red;
        Handles.DrawWireDisc(c.transform.position, Vector3.up,  c.ViewRange);
    }
}
