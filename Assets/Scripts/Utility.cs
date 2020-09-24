using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {
    public static Vector3 GetRandomUnitVector() {
        float randX = Random.Range(0f, 360f);
        float randY = Random.Range(0f, 360f);
        float randZ = Random.Range(0f, 360f);
        Vector3 randUnit = new Vector3(randX, randY, randZ);
        return randUnit;
    }
    
}
