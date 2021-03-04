using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {
    private GridGraph map;
    private Vector3 origPos, targetPos;
    private Vector2 StartNodePosition;
    private List<Node> path;

    public List<GameObject> characters = new List<GameObject>();
    private int currentCharacter = 0;

    public GameObject chosenCharacter;

    public GameObject pathTile;
    public GameObject greenPathTile;
    private bool moving = false;
    private List<GameObject> pathTiles = new List<GameObject>();

    [SerializeField] private GameObject accessPopup;

    // Start is called before the first frame update
    private void Start() {
        accessPopup = Instantiate(accessPopup, Vector3.zero, Quaternion.Euler(90, 0, 0));
        accessPopup.SetActive(false);

        map = new GridGraph(8, 8);
        List<Vector2> _Walls = new List<Vector2>();
        foreach(var chars in characters) {
            _Walls.Add(new Vector2(chars.transform.position.x, chars.transform.position.z));
            //print("Character at position " + chars.transform.position.x + chars.transform.position.z);
        }

        _Walls.Add(new Vector2(1, 2));
        _Walls.Add(new Vector2(2, 2));
        _Walls.Add(new Vector2(2, 3));
        _Walls.Add(new Vector2(2, 4));
        _Walls.Add(new Vector2(2, 5));
        _Walls.Add(new Vector2(2, 6));
        _Walls.Add(new Vector2(1, 6));
        _Walls.Add(new Vector2(0, 6));
        map.Walls = _Walls;

        List<Vector2> _forests = new List<Vector2>();
        map.Forests = _forests;

        StartNodePosition = new Vector2(0, 0);
        StartNodePosition = new Vector2((int)characters[currentCharacter].transform.position.x,
                                        (int)characters[currentCharacter].transform.position.z);

        chosenCharacter = Instantiate(chosenCharacter, characters[currentCharacter].transform.position, Quaternion.Euler(90, 0, 0));
        Vector2 GoalNodePosition = new Vector2(0, 2);

        int x1 = (int)StartNodePosition.x;
        int y1 = (int)StartNodePosition.y;
        int x2 = (int)GoalNodePosition.x;
        int y2 = (int)GoalNodePosition.y;

        // Find the path from StartNodePosition to GoalNodePosition
        path = AStar.Search(map, map.Grid[x1, y1], map.Grid[x2, y2]);
        //StartCoroutine(Move());
    }

    // Update is called once per frame
    private void Update() {
        handleInput();
    }

    private void drawWay() {
        int i = 0;
        foreach(var node in path) {
            if (characters[currentCharacter].GetComponent<Character>().Dexterity * 2 >= node.Priority) {
                pathTiles.Add(Instantiate(greenPathTile, new Vector3(node.Position.x, 0, node.Position.y), Quaternion.Euler(90, 0, 0)));
            }
            else {
                pathTiles.Add(Instantiate(pathTile, new Vector3(node.Position.x, 0, node.Position.y), Quaternion.Euler(90, 0, 0)));
            }
            //i++;
        }
    }

    private void destroyWay() {
        foreach (var tile in pathTiles) {
            Destroy(tile);
        }
    }

    private Vector3 getMousePosition() {
        Vector3 clickPosition = -Vector3.one;
        Plane plane = new Plane(Vector3.up, 0f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;
        if (plane.Raycast(ray, out distanceToPlane)) {
            clickPosition = ray.GetPoint(distanceToPlane);
        }
        return clickPosition;
    }

    private void handleInput() {
        
        if(Input.GetMouseButtonDown(0) && moving == false) {
            Vector3 clickPosition = getMousePosition();

            //print(Mathf.Round(clickPosition.x) + " " + Mathf.Round(clickPosition.z));
            checkWay(clickPosition);
        }
        if(Input.GetKeyDown("n")) {
            NextCharacter();
        }
        if(Input.GetKeyDown("1")) {
            drawAllPossibleWays();
        }
    }

    private void drawAllPossibleWays() {
        int upperLeftX = Mathf.Clamp((int)characters[currentCharacter].transform.position.x - characters[currentCharacter].GetComponent<Character>().Dexterity * 2, 0, map.Height-1),
            upperLeftZ = Mathf.Clamp((int)characters[currentCharacter].transform.position.z + characters[currentCharacter].GetComponent<Character>().Dexterity * 2, 0, map.Width-1),
            lowerRightX = Mathf.Clamp((int)characters[currentCharacter].transform.position.x + characters[currentCharacter].GetComponent<Character>().Dexterity * 2, 0, map.Height-1),
            lowerRightZ = Mathf.Clamp((int)characters[currentCharacter].transform.position.z - characters[currentCharacter].GetComponent<Character>().Dexterity * 2, 0, map.Height-1);
        for(int i = upperLeftX; i <= lowerRightX; i++) {
            for (int j = upperLeftZ; j >= lowerRightZ; j--) {
                print("i = " + i + " j = " + j);
                path = AStar.Search(map, map.Grid[(int)Mathf.Round(StartNodePosition.x), (int)Mathf.Round(StartNodePosition.y)],
                map.Grid[(int)Mathf.Round(i), (int)Mathf.Round(j)]);
                drawWay();
            }
        }

    }

    private void checkWay(Vector3 clickPosition) {
        path = AStar.Search(map, map.Grid[(int)Mathf.Round(StartNodePosition.x), (int)Mathf.Round(StartNodePosition.y)],
                                     map.Grid[(int)Mathf.Round(clickPosition.x), (int)Mathf.Round(clickPosition.z)]);

        if (path[path.Count - 1].Position == new Vector2((int)Mathf.Round(clickPosition.x), (int)Mathf.Round(clickPosition.z)) &&
            characters[currentCharacter].GetComponent<Character>().Dexterity * 2 >= path.Count) {
            map.Walls.Remove(new Vector2((int)characters[currentCharacter].transform.position.x,
                                    (int)characters[currentCharacter].transform.position.z));
            destroyWay();
            drawWay();
            StartCoroutine(Move());
            map.Walls.Add(new Vector2((int)Mathf.Round(clickPosition.x), (int)Mathf.Round(clickPosition.z)));
        }
        else {
            StartCoroutine(popUpAccess(new Vector3(clickPosition.x, 1.3f, clickPosition.z)));
        }
    }

    private void NextCharacter() {
        if(currentCharacter + 1 < characters.Count) {
            currentCharacter++;
        }
        else {
            currentCharacter = 0;
        }
        StartNodePosition = new Vector2((int)characters[currentCharacter].transform.position.x,
                                        (int)characters[currentCharacter].transform.position.z);
        chosenCharacter.transform.position = characters[currentCharacter].transform.position;
        destroyWay();
    }

    private IEnumerator Move() {
        moving = true;
        foreach (var node in path) {
            origPos = characters[currentCharacter].transform.position;
            targetPos = new Vector3(node.Position.x, 0, node.Position.y);
            rotateToMove(origPos, targetPos);
            characters[currentCharacter].transform.position = new Vector3(targetPos.x, 0, targetPos.z);
            yield return new WaitForSeconds(0.5f);
        }

        StartNodePosition.x = transform.position.x;
        StartNodePosition.y = transform.position.z;
        moving = false;
        destroyWay();
        NextCharacter();
    }

    private void rotateToMove(Vector3 fromPos, Vector3 toPos) {
        Vector3 difference = Vector3.zero;
        Vector3 up = new Vector3(0, 0, -1);
        Vector3 down = new Vector3(0, 0, 1);
        Vector3 left = new Vector3(1, 0, 0);
        Vector3 right = new Vector3(-1, 0, 0);
        difference.x = fromPos.x - toPos.x;
        difference.y = 0;
        difference.z = fromPos.z - toPos.z;
        //print(difference.x + " " + difference.y + " " + difference.z);

        Vector3 up3 = new Vector3(0, 360, 0);
        Vector3 down3 = new Vector3(0, 180, 0);
        Vector3 left3 = new Vector3(0, 270, 0);
        Vector3 right3 = new Vector3(0, 90, 0);
        //print("x - " + difference.x + " y - " + difference.y);
        if (difference == up) {
            //print("up");
            characters[currentCharacter].transform.eulerAngles = up3;
            //transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        if (difference == down) {
            //print("down");
            characters[currentCharacter].transform.eulerAngles = down3;
            //transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (difference == left) {
            //print("left");
            characters[currentCharacter].transform.eulerAngles = left3;
            //transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (difference == right) {
            //print("right");
            characters[currentCharacter].transform.eulerAngles = right3;
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private IEnumerator popUpAccess(Vector3 Position) {
        accessPopup.transform.position = Position;
        accessPopup.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        accessPopup.SetActive(false);
    }
}
