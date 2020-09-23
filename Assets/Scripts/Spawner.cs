using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public int SpawnNum;
    public float SpawnRadius;
    public Transform spawnPrefab;
    private void Awake() {
        SpawnCreature();
    }
    public void SpawnCreature() {
        for (int i = 0; i < SpawnNum; i++) {
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(0, SpawnRadius), 0, Random.Range(0, SpawnRadius));
            Vector3 randomForward = new Vector3(0,Random.Range(0,360),0);
            Instantiate(spawnPrefab, randomPosition, Quaternion.Euler(randomForward));
        }
    }
}
