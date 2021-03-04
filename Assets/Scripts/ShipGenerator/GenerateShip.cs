using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GenerateShip : MonoBehaviour {
    public int width;
    public int height;
    string pathSmall = "Assets/Scripts/ShipGenerator/smallShipModules.txt";

    public List<GameObject> ShipSmallCargo;
    public List<GameObject> ShipSmallBedroom;
    public List<GameObject> ShipSmallBridge;

    public List<GameObject> ShipMediumCargo;
    public List<GameObject> ShipMediumBedroom;
    public List<GameObject> ShipMediumBridge;
    public List<GameObject> ShipMediumLeftwing;
    public List<GameObject> ShipMediumRightwing;

    public List<GameObject> ShipLargeCargo;
    public List<GameObject> ShipLargeBedroom;
    public List<GameObject> ShipLargeBridge;
    public List<GameObject> ShipLargeLeftwing;
    public List<GameObject> ShipLargeRightwing;
    public List<GameObject> ShipLargeSouthwing;

    private List<GameObject> roomsToClear;


    public List<GameObject> Shuttle;

    private List<Vector2> _Walls;

    private void Start() {
        _Walls = new List<Vector2>();
        roomsToClear = new List<GameObject>();
    }

    private void Update() {
        if (Input.GetKeyDown("1")) {
            generateShip(1);
        }
        if (Input.GetKeyDown("2")) {
            generateShip(2);
        }
        if(Input.GetKeyDown("3")) {
            generateShip(3);
        }
        if(Input.GetKeyDown("0")) {
            clearRooms();
        }
    }

    public void generateShip(int shipType) {
        switch(shipType) {
            case 1:
                //ClearRooms();
                generateSmallShip();
                break;
            case 2:
                //ClearRooms();
                generateMediumShip();
                break;
            case 3:
                generateLargeShip();
                break;
            default:
                break;
        }

    }
    private void generateSmallShip() {
        //map size 12 45
        int cargo = Random.Range(0, ShipSmallCargo.Capacity);
        int bedroom = Random.Range(0, ShipSmallBedroom.Capacity);
        int bridge = Random.Range(0, ShipSmallBridge.Capacity);

        roomsToClear.Add(Instantiate(Shuttle[0], new Vector3(7, -1, 1), transform.rotation));
        readAndAddToWalls(pathSmall, "#shuttle0");

        roomsToClear.Add(Instantiate(ShipSmallCargo[cargo], new Vector3(11, 0, 18), transform.rotation));
        readAndAddToWalls(pathSmall, "#cargo" + cargo);

        roomsToClear.Add(Instantiate(ShipSmallBedroom[bedroom], new Vector3(0, 0, 18), Quaternion.Euler(0, 180, 0)));
        readAndAddToWalls(pathSmall, "#bedroom" + bedroom);

        roomsToClear.Add(Instantiate(ShipSmallBridge[bridge], new Vector3(0, 0, 31), Quaternion.Euler(0, 180, 0)));
        readAndAddToWalls(pathSmall, "#bridge" + bridge);
    }

    private void generateMediumShip() {
        int cargo = Random.Range(0, ShipMediumCargo.Capacity);
        int bedroom = Random.Range(0, ShipMediumBedroom.Capacity);
        int bridge = Random.Range(0, ShipMediumBridge.Capacity);
        int leftwing = Random.Range(0, ShipMediumLeftwing.Capacity);
        int rightwing = Random.Range(0, ShipMediumRightwing.Capacity);

        roomsToClear.Add(Instantiate(Shuttle[0], new Vector3(20, -1, 1), transform.rotation));

        roomsToClear.Add(Instantiate(ShipMediumCargo[cargo], new Vector3(24, 0, 18), transform.rotation));

        roomsToClear.Add(Instantiate(ShipMediumBedroom[bedroom], new Vector3(13, 0, 18), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipMediumBridge[bridge], new Vector3(13, 0, 31), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipMediumLeftwing[leftwing], new Vector3(0, 0, 18), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipMediumRightwing[rightwing], new Vector3((float)25.5, 0, (float)17.5), Quaternion.Euler(0, 180, 0)));

    }

    private void generateLargeShip() {
        int cargo = Random.Range(0, ShipLargeCargo.Capacity);
        int bedroom = Random.Range(0, ShipLargeBedroom.Capacity);
        int bridge = Random.Range(0, ShipLargeBridge.Capacity);
        int leftwing1 = Random.Range(0, ShipLargeLeftwing.Capacity);
        int leftwing2 = Random.Range(0, ShipLargeLeftwing.Capacity);
        int rightwing1 = Random.Range(0, ShipLargeRightwing.Capacity);
        int rightwing2 = Random.Range(0, ShipLargeRightwing.Capacity);
        int southwing = Random.Range(0, ShipLargeSouthwing.Capacity);

        roomsToClear.Add(Instantiate(Shuttle[0], new Vector3(20, -1, 17), transform.rotation));

        roomsToClear.Add(Instantiate(ShipLargeCargo[cargo], new Vector3(24, 0, 25), transform.rotation));

        roomsToClear.Add(Instantiate(ShipLargeBedroom[bedroom], new Vector3(13, 0, 25), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipLargeBridge[bridge], new Vector3(13, 0, 38), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipLargeLeftwing[leftwing1], new Vector3(0, 0, 12), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipLargeLeftwing[leftwing2], new Vector3(0, 0, 25), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipLargeRightwing[rightwing1], new Vector3((float)25.5, 0, (float)11.5), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipLargeRightwing[rightwing2], new Vector3((float)25.5, 0, (float)24.5), Quaternion.Euler(0, 180, 0)));

        roomsToClear.Add(Instantiate(ShipLargeSouthwing[southwing], new Vector3((float)11.45, 0, (float)-1.45), Quaternion.Euler(0, 180, 0)));

    }

    private void clearRooms() {
        foreach(var room in roomsToClear) {
            Destroy(room);
        }
        roomsToClear.Clear();
    }

    private void readAndAddToWalls(string pathToFile, string module) {
        StreamReader sr = new StreamReader(pathToFile);
        string buf = "";
        while (sr.Peek() >= 0 && buf != module) {
            buf = sr.ReadLine();
        }
        while(sr.Peek() >= 0 && buf.Contains("#")) {
            string pairOfNumbers = sr.ReadLine();
            if(pairOfNumbers.Contains("#")) {
                break;
            }
            print(pairOfNumbers);
            string[] bits = pairOfNumbers.Split(' ');
            int x = int.Parse(bits[0]);
            int z = int.Parse(bits[1]);
            if (pathToFile.Contains("medium")) {
                z += 17;
            }
            _Walls.Add(new Vector2(x, z));
        }
        sr.Close();
    }
}
