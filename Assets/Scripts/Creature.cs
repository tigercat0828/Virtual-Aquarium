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
    // Basic Data
    public int Rank;
    public float MaxHealth;
    public float MaxHungry;
    public float MaxAge;
    public float WanderSpeed;
    // Current Data
    public State currState;
    public float currHealth;
    public float currHungry;
    public float currAge;
    public Vector3 currMoveDirection;

    public virtual void Start() {
        Rank = 0;
        currHealth = MaxHealth;
        currState = State.Wander;
        currHungry = MaxHungry;
        currAge = 15;
        
        currMoveDirection = transform.forward;
    }
    public virtual void Update() {
        // wander move
        if (currState == State.Wander) {
            transform.position += currMoveDirection * WanderSpeed * Time.deltaTime;
        }
        // increase age 
        currAge += Time.deltaTime;
    }

}
