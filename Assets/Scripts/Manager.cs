using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager {
    // Start is called before the first frame update
    public static float WorldSize = 200f;
    public static Vector3 WorldCenter = new Vector3(WorldSize / 2f, 2f, WorldSize / 2f);
    public static float WorldHeight = 2f;

    //Food
    public static float SpawnFoodCoolDown = 1f;
    public static int MaxStartFoodNum = 80;
    public static float FoodEnergy = 10f;
    
    // Agent
    public static int StartAgentNum = 1;
    public static float AgentWanderSpeed = 10f;
    public static float AgentSprintSpeed = 5f;
    public static float AgentTurnForce= 0.3f;
    public static float AgentViewRange = 10f;
    public static float AgentDiverseRate = 0.1f;

    // Predator
    public static float PredatorWanderSpeed = 3f;
    public static float PredatorSprintSpeed = 5f;

    // Debug
    public static int CiricleSegment = 50;

    // Utility Function
    public static Vector3 RandomWorldPosition() {
        float range = Manager.WorldSize / 2f * 0.8f;
        float randX = Random.Range(-range, range);
        float randZ = Random.Range(-range , range);
        return WorldCenter + new Vector3(randX, Manager.WorldHeight, randZ);
    }
    public static Vector3 Limit(Vector3 invec, float limit) { 
        if(invec.magnitude > limit) {
            return invec.normalized * limit;
        }
        return invec;
    }
}
