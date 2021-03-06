﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager {
    // Start is called before the first frame update
    public static float WorldSize = 400;
    public static float WorldDepth = 100f;
    public static Vector3 WorldCenter = new Vector3(0, WorldDepth /2f, 0);


    //Food
    public static float SpawnFoodCoolDown = 1f;
    public static int SpawnFoodNum = 10;
    public static int MaxStartFoodNum = 200;
    public static float FoodEnergy = 500f;


    // life 
    public static float StartLifespan = 60f;
    // Agent
    public static int StartAgentNum = 30;
    public static float AgentWanderSpeed = 20f;
    public static float AgentSprintSpeed = 40f;
    public static float AgentBreedCooldown = StartLifespan * 0.7f;
    public static float AgentTurnForce= 0.5f;
    public static float AgentViewRange = 30f;
    public static float AgentDiverseRate = 0.1f;
    public static float AgentMutationRate = 0.01f;
   // Predator
    public static float PredatorWanderSpeed = 30f;
    public static float PredatorSprintSpeed = 5f;
    public static float PredatorViewRange = 45f;

    // Utility Function
    public static Vector3 RandomWorldPosition() {
        float range = Manager.WorldSize / 2f * 0.8f;
        float randX = Random.Range(-range, range);
        float randZ = Random.Range(-range ,range);
        float randY = Random.Range(10, WorldDepth - 15);

        return new Vector3(randX, randY, randZ);
    }
    public static Vector3 Limit(Vector3 invec, float limit) { 
        if(invec.magnitude > limit) {
            return invec.normalized * limit;
        }
        return invec;
    }
}
