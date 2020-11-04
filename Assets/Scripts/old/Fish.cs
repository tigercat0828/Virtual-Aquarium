using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Collider))]
public class Fish : Creature, IInfoDisplayable {

   
    public string belongFlock;
    public float NeighborRadius = 5f;
    public float AvoidRaius = 1f;
    public float WeightAlignment= 1.0f;
    public float WeightKeepDistance = 1.0f;
    public float WeightCohesion = 1.0f;

    List<Transform> neighborList;
    Collider colliderz;
    Transform prey;
    Transform enemy;


    public float WanderSpeed = 1f;
    public float SprintSpeed = 3f;

    Vector3 currVelocity;


    public override void Start() {
        base.Start();
    }
    public override void Update() {
        if (currState == ActionState.Wander) {
            GetNeighborList();
            transform.position += Wander() * WanderSpeed * Time.deltaTime;
        }
        else if (currState == ActionState.Sprint) {

        }

        // increase age 
        currAge += Time.deltaTime;
    }

    public List<Transform> GetNeighborList() {
        neighborList = new List<Transform>();
        Collider[] nearbyCollider = Physics.OverlapSphere(transform.position, NeighborRadius);
        foreach (Collider c in nearbyCollider) {
            Fish fish = c.GetComponent<Fish>();
            if(fish  != null) {
                if(fish.belongFlock == this.belongFlock) {
                    neighborList.Add(c.transform);
                }
            }
        }
        return neighborList;
    }
    public Vector3 Wander() {
        Vector3 move = Vector3.zero;
        move += Alignment_FlockBehavior() * WeightAlignment;
        move += Cohesion_FlockBehavior() * WeightCohesion;
        move += KeepDistance_FlockBehavior() * WeightKeepDistance;
        return move.normalized;
    }
    // average with forward direcion
    public Vector3 Alignment_FlockBehavior() {
        if(neighborList.Count == 0) {
            return transform.forward;
        }
        Vector3 alignMove = Vector3.zero;
        foreach (Transform item in neighborList) {
            alignMove += item.forward;
        }
        alignMove /= neighborList.Count;
        return alignMove.normalized;
    }
    // average with current position
    public Vector3 Cohesion_FlockBehavior() {
        if (neighborList.Count == 0) {
            return transform.forward;
        }
        Vector3 flockCenter = Vector3.zero;
        foreach (Transform item in neighborList) {
            flockCenter += item.position;
        }
        flockCenter /= neighborList.Count;
        Vector3 cohesionMove = flockCenter - transform.position;
        return cohesionMove.normalized;
    }
    // keep distance between each other
    public Vector3 KeepDistance_FlockBehavior() {
        if (neighborList.Count == 0) {

            return transform.forward;
        }
        Vector3 avoidMove = Vector3.zero;
        return avoidMove;
    }
    public Vector3 EscapeFromEnemy() {
        return new Vector3();
    }
    public Vector3 HuntPrey() {
        return new Vector3();
    }
}
