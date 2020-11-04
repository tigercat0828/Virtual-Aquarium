using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridShower : MonoBehaviour
{
    private float WorldSize;
    private int Segments;
    private Vector3 Anchor;
    private float gridHeight;


    public float WaterLevel = 90;
    public int gridStep = 20;
    
    void Start() {
        WorldSize = Manager.WorldSize;
        Segments = (int)(WorldSize / gridStep);
        gridHeight = 1.1f;
        Anchor = new Vector3(-WorldSize / 2, gridHeight, -WorldSize /2);

        
    }

    // Update is called once per frame
    void Update() {
        DrawGridLine();
    }

    void DrawGridLine() {


        // Draw Waterlevel
        Vector3 a = new Vector3(-WorldSize / 2f, WaterLevel, -WorldSize /2f);
        Vector3 b = new Vector3(WorldSize / 2f, WaterLevel, -WorldSize / 2f);
        Vector3 c = new Vector3(WorldSize / 2f, WaterLevel, WorldSize / 2f);
        Vector3 d = new Vector3(-WorldSize / 2f, WaterLevel, WorldSize / 2f);
        Debug.DrawLine(a, b, Color.cyan);
        Debug.DrawLine(b, c, Color.cyan);
        Debug.DrawLine(c, d, Color.cyan);
        Debug.DrawLine(d, a, Color.cyan);
        // DrawGrid
        // X-line
        for (int i = 0; i <= Segments; i++) {

            Vector3 A =Anchor +  new Vector3(i * gridStep, 0, 0f);
            Vector3 B = Anchor + new Vector3(i * gridStep, 0, Manager.WorldSize);
            Debug.DrawLine(A, B, Color.gray);
        }
        // Y-line
        for (int i = 0; i <= Segments; i++) {
            Vector3 A = Anchor + new Vector3(0, 0, i * gridStep);
            Vector3 B = Anchor + new Vector3(Manager.WorldSize, 0, i * gridStep);
            Debug.DrawLine(A, B, Color.gray);
        }
    }
}
