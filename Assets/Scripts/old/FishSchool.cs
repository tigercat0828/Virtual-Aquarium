using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchool: MonoBehaviour {
    // Start is called before the first frame update


    public Fish FishPrefab;
    public Vector3 SpawnPosition;
    public int FishCount;

    // state of fish belong to this flock
    [Range(0,1)] public float DifferenceRate;
    public string SchoolName;
    public int Rank;
    public float MaxHealth;
    public float MaxHungry;
    public float MaxAge;

    // flock behavoir value
    public float NeighborRadius;
    public float AvoidRadius;
    public float WeightAlign;
    public float WeightAvoid;
    public float WeightCohesion;

    public void SpawnSchool(int fishCount, string flockName, float neighborRadius, float avoidRaius,
                               float weightAlign, float weightAvoid, float wightCohesion) {
        DifferenceRate = 1 - DifferenceRate;
        for (int i = 0; i < fishCount; i++) {
            Vector3 randomRotation = Utility.GetRandomUnitVector();
            Fish newFish = Instantiate(
                FishPrefab,
                SpawnPosition,
                Quaternion.Euler(randomRotation),
                transform
            );
            newFish.belongFlock = SchoolName;
            newFish.name = SchoolName + i;
            newFish.Rank = Rank;
            newFish.MaxHungry = MaxHungry;
            newFish.MaxHealth = MaxHealth;
            newFish.MaxAge = MaxAge;
        }
    }
}
