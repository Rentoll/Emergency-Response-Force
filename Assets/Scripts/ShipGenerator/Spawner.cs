using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private GameObject gridMvmt;
    private GridMovement characterList;
    public GameObject HoS, Medic, Engineer, Heavy;
    [SerializeField] public List<GameObject> EnemyMesh = new List<GameObject>();
    public GameObject Enemy;

    private void Start() {
        gridMvmt = GameObject.Find("GridMovement");
        characterList = gridMvmt.GetComponent<GridMovement>();
        //character.teamRole = Character.teamRoles.HoS;
        //spawnCharacter(character);
    }

    public void spawnCharacter(Character character) {
        
        switch(character.teamRole) {
            case Character.teamRoles.HoS:
                AddNewCharacter(HoS, character);
                break;
            case Character.teamRoles.Medic:
                AddNewCharacter(Medic, character);
                break;
            case Character.teamRoles.Engineer:
                AddNewCharacter(Engineer, character);
                break;
            case Character.teamRoles.Heavy:
                AddNewCharacter(Heavy, character);
                break;
            default:
                break;
        }
    }

    private void AddNewCharacter(GameObject mesh, Character character) {
        gridMvmt = GameObject.Find("GridMovement");
        characterList = gridMvmt.GetComponent<GridMovement>();
        GameObject buf = Instantiate(mesh, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(0, 0, 0));
        buf.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        buf.AddComponent<Character>();
        buf.GetComponent<Character>().Dexterity = character.Dexterity;
        buf.GetComponent<Character>().Strength = character.Strength;
        buf.GetComponent<Character>().Constitution = character.Constitution;
        buf.GetComponent<Character>().HP = character.HP;
        buf.GetComponent<Character>().teamRole = character.teamRole;
        characterList.characters.Add(buf);
        //print(characterList.characters.Count);
    }

    public void spawnEnemy() {
        //GameObject buf = Instantiate(EnemyMesh[Random.Range(0, EnemyMesh.Count)], new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(0, 0, 0));
        GameObject buf = Instantiate(Enemy, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(0, 0, 0));
        buf.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        buf.AddComponent<Character>();
        buf.GetComponent<Character>().Dexterity = Random.Range(1, 5);
        buf.GetComponent<Character>().Strength = Random.Range(1, 5);
        buf.GetComponent<Character>().Constitution = Random.Range(1, 5);
        buf.GetComponent<Character>().HP = buf.GetComponent<Character>().Constitution * 2;
        AI.Enemies.Add(buf);
    }
}
