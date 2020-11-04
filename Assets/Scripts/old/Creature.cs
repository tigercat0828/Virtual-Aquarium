using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Video;



public class Creature : MonoBehaviour, IInfoDisplayable {

    public enum ActionState {
        Wander, Search , Sprint, Die
    }
    // Basic Data
    public int Rank;
    public float MaxHealth;
    public float MaxHungry;
    public float MaxAge;

    // Current Data
    public ActionState currState;
    public float currHealth;
    public float currHungry;
    public float currAge;


    public virtual void Start() {
        Rank = 0;
        currHealth = MaxHealth;
        currState = ActionState.Wander;
        currHungry = MaxHungry;
        currAge = 15;
    }
    public virtual void Update() {
        // wander move
      
        // increase age 
        currAge += Time.deltaTime;
    }
    public CreatureInfo GetCreatureInfo() {
        CreatureInfo states = new CreatureInfo(Rank, transform.name ,currState, currHealth, currHungry, currAge);
        return states;
    }
}
