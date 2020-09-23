using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Video;

public class Creature : MonoBehaviour {


    public enum State {
        Wander, Die
    }
    public float MaxHealth;
    public float MoveSpeed;

    public Vector3 currDirection;
    public float currHealth;


    private void Start() {
        currDirection = transform.forward;
        currHealth = MaxHealth;
    }
    private void Update() {
        transform.position += currDirection * MoveSpeed * Time.deltaTime;
    }

}
