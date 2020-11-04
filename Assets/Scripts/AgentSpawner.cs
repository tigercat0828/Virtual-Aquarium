using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AgentSpawner : MonoBehaviour {


    public Agent AgentPrefab;
    // Start is called before the first frame update
    void Start() {
        SpawnStartAgents();
    }

    // Update is called once per frame
    void Update() {

    }
    void SpawnStartAgents() {
        for (int i = 0; i < Manager.StartAgentNum; i++) {
            Vector3 pos = Manager.RandomWorldPosition();
            Agent agent = Instantiate(AgentPrefab, pos, Quaternion.identity, transform) as Agent;
            agent.name = "Agent " + i;
        }
    }
}
