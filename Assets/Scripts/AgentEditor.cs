using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Agent))]
public class AgentEditor : Editor
{


    public void OnSceneGUI() {
        Agent c = (Agent)target;
        if (c) {

            Handles.color = Color.red;
            Handles.DrawWireDisc(c.transform.position, c.transform.up, c.ViewRange);
            Handles.DrawWireDisc(c.transform.position, c.transform.right, c.ViewRange);
            Handles.DrawWireDisc(c.transform.position, c.transform.forward, c.ViewRange);
        }
    
    }
}
