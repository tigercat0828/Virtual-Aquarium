using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureDisplayer : MonoBehaviour {


    public Transform Selected;
    public Text RankText;
    public Text NameText;
    public Text StateText;
    public Text HealthText;
    public Text HungryText;
    public Text AgeText;
    private void Update() {

        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit)) {
                Selected = hit.transform;
                if (Selected.CompareTag("Selectable")) {
                    Debug.Log(Selected.name);
                    Creature creature = Selected.GetComponent<Creature>();
                    NameText.text = creature.name;
                    HealthText.text = creature.currHealth.ToString();
                }

            }
            
        }

    }

}
