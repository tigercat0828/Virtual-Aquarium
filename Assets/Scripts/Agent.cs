using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.UI;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices.WindowsRuntime;

public class Agent : MonoBehaviour {

    public enum AgentState {
        Wander, Spirit, Exit
    }
    

    
    public float health;
    public float ViewRange;
    public float WanderSpeed;
    public float SprintSpeed;


    public Vector3 accelerate;
    public Vector3 velocity;
    public Transform target;
    public Transform enemy;
    public AgentState state;

    public int eaten = 0;
    // Start is called before the first frame update

    private float _worldSize = Manager.WorldSize / 2f * 0.98f;
    private float _worldDepth = Manager.WorldDepth * 0.98f;
   
    void Awake() {

    }
    void Start() {

        WanderSpeed = Manager.AgentWanderSpeed;
        ViewRange = Manager.AgentViewRange;
        health = (0.15f * WanderSpeed * WanderSpeed + 1.15f * ViewRange) * Manager.StartLifespan;
        state = AgentState.Wander;
    }

    void Update() {
        //FixHeight();
        ObserveEnviroment();
        health -= (0.15f * WanderSpeed * WanderSpeed + 1.15f * ViewRange) * Time.deltaTime;
        // Wander and Search for food to survive in the arena !!
        if (state == AgentState.Wander) {
            if (target) {
                MoveTowardTarget();
            }
            else {
                transform.position += transform.forward * WanderSpeed * Time.deltaTime;
            }
        }
        // I have to escape from my enemy or i will die
        else if (state == AgentState.Spirit) {

            AvoidFromEnemy();
        }
        else if (state == AgentState.Exit) {
            GobackToArena();
        }

    }
    void MoveTowardTarget() {
        Vector3 desired = Vector3.ClampMagnitude(target.position - transform.position, WanderSpeed);
        Vector3 steerForce = Manager.Limit(desired - velocity, Manager.AgentTurnForce);
        accelerate = steerForce;    //扭力
        velocity += accelerate;
        //velocity = Manager.Limit(velocity, WanderSpeed); 
        velocity = velocity.normalized * WanderSpeed;

        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity;
    }
    void AvoidFromEnemy() {

    }
    void ObserveEnviroment() {

        Collider[] colliderList = Physics.OverlapSphere(transform.position, ViewRange);
        // check new enemy or Food;
        foreach (Collider collider in colliderList) {
            if (collider.gameObject.tag == "Enemy") {
                enemy = collider.transform;
                state = AgentState.Spirit;
                break;
            }
            if (collider.gameObject.tag == "Food") {
                target = collider.transform;
                state = AgentState.Wander;
                break;
            }
        }
        // check my enemy far away my range
        if (enemy) {
            if (Vector3.Distance(transform.position, enemy.transform.position) > ViewRange) {
                enemy = null;
                state = AgentState.Wander;
            }

        }

        // check whether I am exiting the arena
        if (Mathf.Abs(transform.position.x) > _worldSize) {
            state = AgentState.Exit;
        }
        else if (Mathf.Abs(transform.position.z) > _worldSize) {
            state = AgentState.Exit;
        }
        else if (transform.position.y < 5f) {
            state = AgentState.Exit;
        }
        else if (transform.position.y > _worldDepth) {
            state = AgentState.Exit;
        }
        else {
            // I am in the Arena
            state = AgentState.Wander;
        }

    }
    void GobackToArena() {
        Vector3 desired = Vector3.ClampMagnitude(Manager.WorldCenter - transform.position, WanderSpeed);
        Vector3 steerForce = Manager.Limit(desired - velocity, Manager.AgentTurnForce);
        accelerate = steerForce;    //扭力
        velocity += accelerate;
        velocity = Manager.Limit(velocity, WanderSpeed);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity;
    }

}
