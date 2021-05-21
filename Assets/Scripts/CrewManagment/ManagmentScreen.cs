using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagmentScreen : MonoBehaviour {

    int currentDate = 2200;
    int UpgradePoints = 5;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI lvlHealth, lvlSpeed, lvlDamage, UpgradePnts;
    private int lvlH, lvlS, lvlD;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        dateText.text = "Date: " + currentDate.ToString();
        UpgradePnts.text = UpgradePoints.ToString();
        lvlHealth.text = lvlSpeed.text = lvlDamage.text = "";
        for(int i = 0; i < lvlH; i++) {
            lvlHealth.text += "|";
        }
        for (int i = 0; i < lvlS; i++) {
            lvlSpeed.text += "|";
        }
        for (int i = 0; i < lvlD; i++) {
            lvlDamage.text += "|";
        }
    }

    private bool checkPoints() {
        return UpgradePoints > 0;
    }

    public void nextMission() {
        SceneManager.LoadScene("DemoShipCreation");
    }

    public void upgradeHealth() {
        if (checkPoints()) {
            foreach (var character in Crew.currentEmergencyTeam) {
                character.Constitution++;
            }
            UpgradePoints--;
            lvlH++;
        }
    }

    public void upgradeDamage() {
        if (checkPoints()) {
            foreach (var character in Crew.currentEmergencyTeam) {
                character.Strength++;
            }
            UpgradePoints--;
            lvlD++;
        }
    }

    public void upgradeSpeed() {
        if (checkPoints()) {
            foreach (var character in Crew.currentEmergencyTeam) {
                character.Dexterity++;
            }
            UpgradePoints--;
            lvlS++;
        }
    }
}
