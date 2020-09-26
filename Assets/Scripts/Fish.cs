using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;



[RequireComponent(typeof(Collider))]
public class Fish : Creature, IInfoDisplayable {

    public float NeighborRadius = 5f;
    List<Transform> neighborList;
    Collider colliderz;
    public override void Start() {
        base.Start();
    }
    public override void Update() {
        if (currState == ActionState.Wander) {
            transform.position += currMoveDirection * WanderSpeed * Time.deltaTime;
        }

        // increase age 
        currAge += Time.deltaTime;
    }

    public List<Transform> GetNeighborList() {
        List<Transform> neighborList = new List<Transform>();
        Collider[] nearbyCollider = Physics.OverlapSphere(transform.position, NeighborRadius);
        foreach (Collider c in nearbyCollider) {
            Fish fish = c.GetComponent<Fish>();
            if (c != this.colliderz && fish != null) {
                neighborList.Add(c.transform);
            }
        }
        return neighborList;
    }
    public Vector3 Align_Behavior() {
        Vector3 alignMove = Vector3.zero;
        foreach (Transform item in neighborList) {
            alignMove += item.forward;
        }
        alignMove /= neighborList.Count;
        return alignMove.normalized;
    }
    public Vector3 Cohesion_Behavior() {
        Vector3 cohesion = Vector3.zero;
        return cohesion
    }
}
