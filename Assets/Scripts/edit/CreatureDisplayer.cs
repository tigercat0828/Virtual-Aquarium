using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CreatureDisplayer : MonoBehaviour {
    public Transform selectedTransform;
    public IInfoDisplayable currCreatureInfo;
    public Text RankText;
    public Text NameText;
    public Text StateText;
    public Text HealthText;
    public Text HungryText;
    public Text AgeText;
    private void Update() {
        SelectTarget();
       if(currCreatureInfo != null) {
            DisplayCreatureData(currCreatureInfo);
        }
        else {
            DisplayDefaultText();
        }
       
    }
    public void SelectTarget() {
        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit)) {
                selectedTransform = hit.transform;
                if (selectedTransform != null) {
                    if (selectedTransform.CompareTag("Selectable")) {
                        currCreatureInfo = selectedTransform.GetComponent<IInfoDisplayable>();
                    }
                }
            }
        }
        if (Input.GetMouseButton(1)) {
            selectedTransform = null;
            currCreatureInfo = null;
        }
    }
    public void DisplayCreatureData(IInfoDisplayable infoDisplayable) {
        CreatureInfo info = infoDisplayable.GetCreatureInfo();
        RankText.text = info.rank.ToString();
        NameText.text = info.name;
        StateText.text = info.currState.ToString();
        HealthText.text = info.currHealth.ToString();
        HungryText.text = info.currHungry.ToString();
        AgeText.text = ((int)(info.currAge)).ToString();
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
