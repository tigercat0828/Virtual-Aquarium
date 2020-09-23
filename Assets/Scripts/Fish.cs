using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Fish : MonoBehaviour {

    private enum State {
        Wonder, Foraging, Died
    }
    private const float MAX_SATURATION_LEVEL = 10.0f;
    private const float STARVE_LEVEL = 5.0f;
    private float basalMetabolicRate = 0.1f;

    private Vector3 moveDirection;

    public float moveSpeed;
    public float HealthLevel;
    public float SaturationLevel;


    private void Start() {
        HealthLevel = 10.0f;
        SaturationLevel = 10.0f;
        moveDirection = transform.forward;
    }
    private void Update() {
        Move();
    }

    public void Move() {
        transform.position += moveDirection * moveSpeed * HungryEffectSpeed() * Time.deltaTime;
    }
    private void PerformMetabolism() {
        
    }
    private float HungryEffectSpeed() {
        if (SaturationLevel < 5.0f) {
            float slowDownPercent = 1 - SaturationLevel / MAX_SATURATION_LEVEL;
            return slowDownPercent;
        }
        else {
            return 1.0f;
        }
    }

}
