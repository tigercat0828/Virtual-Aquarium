using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    private void Start() {
        EventPublisher publisher = GetComponent<EventPublisher>();
        publisher.OnSpacePressed += Publisher_OnSpacePressed;
        publisher.OnKeyAltPressed += Publisher_OnKeyAltPressed;
    }

    private void Publisher_OnSpacePressed() {
        Debug.Log("Fire!");
    }

    private void Publisher_OnKeyAltPressed(int obj) {
        Debug.Log("Alt Key:" + obj);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            CancelSubscribe();
        }
    }
    void CancelSubscribe() {
        EventPublisher publisher = GetComponent<EventPublisher>();
        publisher.OnSpacePressed -= Publisher_OnSpacePressed;
         

    }
}

 

