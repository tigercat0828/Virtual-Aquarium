using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    int Segments;
    // Start is called before the first frame update
    void Start() {
        Segments = (int)(Manager.WorldSize / 10f);
    }

    // Update is called once per frame
    void Update() {
        DrawGridLine();
    }

    void DrawGridLine() {
        // X-line
        for (int i = 0; i < Segments; i++) {
            Vector3 A = new Vector3(i * 10f, 0.01f, 0f);
            Vector3 B = new Vector3(i * 10f, 0.01f, Manager.WorldSize);
            Debug.DrawLine(A, B, Color.gray);
        }
        // Y-line
        for (int i = 0; i < Segments; i++) {
            Vector3 A = new Vector3(0, 0.01f, i * 10f);
            Vector3 B = new Vector3(Manager.WorldSize, 0.01f, i * 10f);
            Debug.DrawLine(A, B, Color.gray);
        }
    }
}