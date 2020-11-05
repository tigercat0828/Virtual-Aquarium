using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    // Start is called before the first frame update
    public float energy;

    void Awake() {

    }
    void Start() {
        energy = Manager.FoodEnergy;
    }

    // Update is called once per frame
    void Update() {

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Agent") {
            other.gameObject.GetComponentInParent<Agent>().EatFood(energy);
        }

        Destroy(gameObject);
    }
}