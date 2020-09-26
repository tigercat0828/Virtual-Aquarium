using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClassC : MonoBehaviour {
    private void Start() {
        MyClassA classA = new MyClassA();
        TestInterface(classA);
    }
    void TestInterface(ITestInterface testInterface) {
        testInterface.DisplayStats();
    }


}
