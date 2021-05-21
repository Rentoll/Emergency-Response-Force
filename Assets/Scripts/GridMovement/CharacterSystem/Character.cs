using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public int Strength;
    public int Dexterity;
    public int Constitution;
    public int HP;
    public enum teamRoles {
        HoS = 0,
        Medic = 1,
        Engineer = 2,
        Heavy = 3
    }

    public Character() {
        Strength = Random.Range(1, 5);
        Dexterity = Random.Range(1, 5);
        Constitution = Random.Range(1, 5);
        HP = Constitution * 2;
        teamRole = (teamRoles)Random.Range(0, 4);
    }

    public teamRoles teamRole;

    void Start() {
        generateRandomCharacter();
    }

    public void generateRandomCharacter() {
        Strength = Random.Range(1, 5);
        Dexterity = Random.Range(1, 5);
        Constitution = Random.Range(1, 5);

        teamRole = (teamRoles)Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
