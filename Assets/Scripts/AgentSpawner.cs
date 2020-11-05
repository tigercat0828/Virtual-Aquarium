using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Experimental.GraphView;

public class AgentSpawner : MonoBehaviour {


    public Agent AgentPrefab;
    public int AgentNum;
    public float AverageWanderSpeed;
    public float AverageViewRange;
    public float AverageBreedCooldown;

    public float TimeBetweenOutput;
    public float OutputTimer;
    
    // Start is called before the first frame update
    void Start() {
        OutputTimer = 0f;
        TimeBetweenOutput = 5f;
        SpawnStartAgents();
    }

    // Update is called once per frame
    void Update() {
        

        OutputTimer += Time.deltaTime;
        if (OutputTimer > TimeBetweenOutput) {
            DoStatistics();
            WriteOutputFile();
            OutputTimer = 0;
        }
        AgentNum = transform.childCount;
    }
    void SpawnStartAgents() {
        for (int i = 0; i < Manager.StartAgentNum; i++) {
            Vector3 pos = Manager.RandomWorldPosition();
            Agent agent = Instantiate(AgentPrefab, pos, Quaternion.identity, transform) as Agent;
            agent.name = "Agent " + i;
        }
    }
    void DoStatistics() {
        print("do statistics");
        
        float sum_WanderSpeed= 0f;
        float sum_ViewRange = 0f;
        float sum_BreedCooldown = 0f;

        GameObject[] AgentList = GameObject.FindGameObjectsWithTag("Agent");
        int counted = 0;
        // sum of all propertie value of agents
        foreach (var gameObject in AgentList) {
            counted++;
            Agent agent = gameObject.GetComponentInParent<Agent>();
            // Debug.Log(agent.name + " V:" + agent.ViewRange + " S:" + agent.WanderSpeed + " B:" + agent.breedCooldown);
            sum_WanderSpeed += agent.WanderSpeed;
            sum_ViewRange += agent.ViewRange;
            sum_BreedCooldown += agent.breedCooldown;
        }
        AverageBreedCooldown = sum_BreedCooldown / counted;
        AverageWanderSpeed = sum_WanderSpeed / counted;
        AverageViewRange = sum_ViewRange / counted;
    }
    void WriteOutputFile() {
        string path = Application.dataPath + "/statistics.txt";
        if (!File.Exists(path)) {
            File.WriteAllText(path, "Population | WanderSpeed | ViewRange | BreedCooldown\n");
        }
        string content = transform.childCount + " " + AverageWanderSpeed + " " + AverageViewRange + " " + AverageBreedCooldown + '\n';
        File.AppendAllText(path, content);
    }
}
