using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.UI;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SocialPlatforms;

public class Agent : MonoBehaviour {



    public static Agent AgentPrefab;

    public enum AgentState {
        Wander, Spirit, Exit
    }
    public float health;
    public float ViewRange;
    public float WanderSpeed;
    public float SprintSpeed;

    public float breedTimer = 0f;
    public float breedCooldown;
    public bool isAbleToBreed;
    public float MaxHealth;
    public Vector3 accelerate;
    public Vector3 velocity;
    public Transform target;
    public Transform enemy;
    public AgentState state;

    [SerializeField]
    private Transform spawner;
    private Vector3 homeland;
    // Start is called before the first frame update

    private float _worldSize = Manager.WorldSize / 2f * 0.98f;
    private float _worldDepth = Manager.WorldDepth * 0.98f;

    void Awake() {

    }
    void Start() {
        float diverseRate;

        diverseRate = 1 + Random.Range(-Manager.AgentDiverseRate, Manager.AgentDiverseRate);
        WanderSpeed = Manager.AgentWanderSpeed * diverseRate;
        diverseRate = 1 + Random.Range(-Manager.AgentDiverseRate, Manager.AgentDiverseRate);
        ViewRange = Manager.AgentViewRange * diverseRate;
        diverseRate = 1 + Random.Range(-Manager.AgentDiverseRate, Manager.AgentDiverseRate);
        breedCooldown = Manager.AgentBreedCooldown * diverseRate;

        spawner = GameObject.Find("AgentSpawner").transform;
        isAbleToBreed = false;
        breedTimer = 0;
        MaxHealth = (0.15f * WanderSpeed * WanderSpeed + 1.15f * ViewRange) * Manager.StartLifespan;
        health = MaxHealth;
        state = AgentState.Wander;
        homeland = Manager.RandomWorldPosition();
    }

    void Update() {

        CheckHealth();
        ObserveEnviroment();
        health -= (0.15f * WanderSpeed * WanderSpeed + 1.15f * ViewRange) * Time.deltaTime;

        breedTimer += Time.deltaTime;
        if (breedTimer >= breedCooldown) {
            isAbleToBreed = true;
        }

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
        if (isAbleToBreed) {
            foreach (Collider collider in colliderList) {
                if (collider.gameObject.tag == "Agent") {
                    Agent agent = collider.gameObject.GetComponentInParent<Agent>();
                    if (agent) {
                        if (agent.isAbleToBreed) {
                            this.BreedNewIndividual(agent);
                            break;
                        }
                    }
                }
            }
        }

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
        CheckIsOutside();

    }
    void GobackToArena() {
        // Vector3 desired = Vector3.ClampMagnitude(Manager.WorldCenter - transform.position, WanderSpeed);
        Vector3 desired = Vector3.ClampMagnitude(homeland - transform.position, WanderSpeed);
        Vector3 steerForce = Manager.Limit(desired - velocity, Manager.AgentTurnForce);
        accelerate = steerForce;    //扭力
        velocity += accelerate;
        velocity = Manager.Limit(velocity, WanderSpeed);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity;
    }
    void CheckHealth() {
        // out of max storage energy
        if (health >= MaxHealth) {
            health = MaxHealth;

        }
        // ran out of energy, die!
        else if (health < 0f) {
            print(gameObject.name + " Die!");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Food") {
            health += Manager.FoodEnergy;
            Debug.Log("Food eat");
        }
    }
    public void EatFood(float energy) {
        health += energy;
    }

    void CheckIsOutside() {
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

    public void BreedNewIndividual(Agent waifu) {


        isAbleToBreed = false;
        breedTimer = 0f;
        Agent agentPrefab = spawner.GetComponent<AgentSpawner>().AgentPrefab;
        Agent agent = Instantiate(agentPrefab, (this.transform.position + waifu.transform.position) / 2f, Quaternion.identity, spawner) as Agent;

        // new child difference;
        // ==============================================================================

        agent.WanderSpeed = (this.WanderSpeed + waifu.WanderSpeed) / 2f;
        agent.ViewRange = (this.ViewRange + waifu.ViewRange) / 2f;
        agent.breedCooldown = (this.breedCooldown + waifu.breedCooldown) / 2f;

        // ==============================================================================
        /*
        float bp; // breakpoint
        bp = Random.Range(0, 1);
        agent.WanderSpeed = this.WanderSpeed * bp + waifu.WanderSpeed * (1 - bp);

        bp = Random.Range(0, 1);
        agent.ViewRange = this.ViewRange * bp + waifu.ViewRange * (1 - bp);

        bp = Random.Range(0, 1);
        agent.breedCooldown = this.breedCooldown * bp + waifu.breedCooldown * (1 - bp);
        // ==============================================================================
         */

        agent.spawner = GameObject.Find("AgentSpawner").transform;
        agent.isAbleToBreed = false;
        agent.breedTimer = 0;
        agent.MaxHealth = (0.15f * WanderSpeed * WanderSpeed + 1.15f * ViewRange) * Manager.StartLifespan;
        agent.health = MaxHealth * 0.8f;
        agent.state = AgentState.Wander;
        agent.homeland = Manager.RandomWorldPosition();
        agent.name = "Agent " + spawner.childCount;
        print(agent.name + " born!");

    }
}
