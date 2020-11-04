using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.UI;
using UnityEngine;
using UnityEditor;


public class Agent : MonoBehaviour {

    public enum AgentState {
        Wander, Spirit, Exit
    }
    public Vector3 accelerate;
    public Vector3 velocity;
    public float health;
    public Transform target;
    public Transform enemy;
    public float WanderSpeed;
    public float SprintSpeed;
    public float ViewRange;
    public AgentState state;
    // Start is called before the first frame update


    void Awake() {

    }
    void Start() {

        WanderSpeed = Manager.AgentWanderSpeed;
        ViewRange = Manager.AgentViewRange;
        state = AgentState.Wander;
    }

    void Update() {
        //FixHeight();
        ObserveEnviroment();
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
        if (Vector3.Distance(transform.position, Manager.WorldCenter) > Manager.WorldSize / 2f) {
            state = AgentState.Exit;
        }
    }
    void GobackToArena() {
        Vector3 desired = Vector3.ClampMagnitude(Manager.WorldCenter - transform.position,  WanderSpeed);
        Vector3 steerForce = Manager.Limit(desired - velocity, Manager.AgentTurnForce);
        accelerate = steerForce;    //扭力
        velocity = velocity.normalized * WanderSpeed;
        velocity += accelerate;
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity;
    }

    void FixHeight() {
        if (transform.position.y > 2.0f) {
            transform.position -= new Vector3(0, 0.1f, 0);
        }
        else if (transform.position.y < 2.0f) {
            transform.position += new Vector3(0, 0.1f, 0);
        }
    }

}




/*
// --------------------- global eye view;
GameObject[] foodList = GameObject.FindGameObjectsWithTag("Food");
float minDist = float.MaxValue;
foreach (var food in foodList) {
    float dist = Vector3.Distance(transform.position, food.transform.position);
    if (dist < minDist) {
        target = food.transform;
        minDist = dist;
    }
}
 */