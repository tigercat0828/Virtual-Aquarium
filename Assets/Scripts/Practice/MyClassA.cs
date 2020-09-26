using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClassA : MonoBehaviour, ITestInterface
{
    public int HP = 10;
    public int SP = 15;

    public void DisplayStats() {
        Debug.Log("HP = " + HP + "SP = " + SP);
    }
}
