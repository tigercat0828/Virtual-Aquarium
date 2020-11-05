using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public Food FoodPrefab;
    public float SpawnFoodTimer = 0f;
    public float SpawnFoodCooldown;
    void Awake() {
       
        SpawnStartFoods();
        SpawnFoodTimer = 0f;
        SpawnFoodCooldown = Manager.SpawnFoodCoolDown;
    }
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        GameObject[] foodList = GameObject.FindGameObjectsWithTag("Food");
        if (foodList.Length < Manager.MaxStartFoodNum) {
            if (SpawnFoodTimer >= SpawnFoodCooldown) {
                SpawnBatchFood();
                SpawnFoodTimer = 0f;
                print("food spawn");
            }
        }
        SpawnFoodTimer += Time.deltaTime;
    }

    void SpawnStartFoods() {
        for (int i = 0; i < Manager.MaxStartFoodNum; i++) {
            SpawnFood();
        }
    }
    void SpawnFood() {

        Vector3 pos = Manager.RandomWorldPosition();
        Food food = Instantiate(FoodPrefab, pos, Quaternion.identity, transform) as Food;

    }
    void SpawnBatchFood() {
        for (int i = 0; i < Manager.SpawnFoodNum; i++) {
            Vector3 pos = Manager.RandomWorldPosition();
            Food food = Instantiate(FoodPrefab, pos, Quaternion.identity, transform) as Food;
        }
    }
    
}
