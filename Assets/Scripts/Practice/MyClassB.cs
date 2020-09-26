using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClassB : MonoBehaviour, ITestInterface
{

    public int HP;
    public int SP;

    public void DisplayStats() {
        Debug.Log("HP = " + HP + "SP = " + SP);
    }
}
