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
        // select target()
        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit)) {
                Selected = hit.transform;
                if (Selected != null) {
                    if (Selected.CompareTag("Selectable")) {
                        Debug.Log(Selected.name);
                        Creature creature = Selected.GetComponent<Creature>();
                        DisplayCreatureData(creature);
                    }
                    else {
                        DisplayDefaultText();
                    }
                }
            }   
        }
    }
    public void DisplayCreatureData(Creature creature) {
        RankText.text = creature.Rank.ToString();
        NameText.text = creature.name;
        StateText.text = creature.currState.ToString();
        HealthText.text = creature.currHealth.ToString();
        HungryText.text = creature.currHungry.ToString();
        AgeText.text = creature.currAge.ToString();
    }
    public void DisplayDefaultText() {
        RankText.text = "-";
        NameText.text = "-";
        StateText.text = "-";
        HealthText.text = "-";
        HungryText.text = "-";
        AgeText.text = "-";
    }
}
