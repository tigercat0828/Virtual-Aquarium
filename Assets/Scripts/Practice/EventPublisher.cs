using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    public event System.Action OnSpacePressed;
    public event System.Action<int> OnKeyAltPressed;

    public int altKeyPressedCount = 0;
    private void Start() {
        
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (OnSpacePressed != null) {
                OnSpacePressed();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            if(OnKeyAltPressed != null) {
                altKeyPressedCount++;
                OnKeyAltPressed(altKeyPressedCount);
            }
        }

    }
}
