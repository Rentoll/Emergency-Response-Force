using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew : MonoBehaviour {
    static private List<Character> avaliableCrew = new List<Character>();
    static public List<Character> currentEmergencyTeam = new List<Character>();
    private int startCrew = 4;
    private void Start() {
        //generateCrew();
    }

    public void generateCrew() {
        for(int i = 0; i < startCrew; i++) {
            //Character character;
            //var character = new Character();
            //character.generateRandomCharacter();
            avaliableCrew.Add(new Character());
        }
        currentEmergencyTeam = avaliableCrew;
    }

    public List<Character> getCrewList() {
        List<Character> buf = currentEmergencyTeam; 
        return buf;
    }
}
