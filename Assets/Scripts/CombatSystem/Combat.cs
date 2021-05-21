using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {
    private GameObject gridMvmt, fov;
    private GridMovement characterList;
    private FieldOfview fieldOfView;
    public LayerMask FOW;

    public List<GameObject> avaliableTargets = new List<GameObject>();
    public GameObject avaliableTarget;

    private bool shootingPhase;

    private void Start() {
        gridMvmt = GameObject.Find("GridMovement");
        characterList = gridMvmt.GetComponent<GridMovement>();
        fov = GameObject.Find("FieldOfview");
        fieldOfView = gridMvmt.GetComponent<FieldOfview>();

    }

    private void Update() {
        if (Input.GetKeyDown("g")) {
            if (shootingPhase == false) {
                shootingPhase = true;
                CheckEnemies();
            }
            else {
                shootingPhase = false;
                clearTargets();
            }
        }
        
        if(Input.GetMouseButtonDown(1) && fieldOfView.visibleTargets.Count > 0) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000f, ~FOW) && hit.collider.tag == "Enemy") {
                GameObject buf = hit.collider.gameObject;
                //Shoot(buf);
                StartCoroutine("Rotate", buf);
            }
        }
    }
    public void CheckEnemies() {
        clearTargets();
        if(fieldOfView.visibleTargets.Count > 0) {
            foreach(var target in fieldOfView.visibleTargets) {
                avaliableTargets.Add(Instantiate(avaliableTarget, target.position, Quaternion.Euler(90, 0, 0)));
            }
        }
    }

    IEnumerator Rotate(GameObject buf) {
        float duration = 1f;
        float startRotation = buf.transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration) {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            buf.transform.eulerAngles = new Vector3(buf.transform.eulerAngles.x, yRotation, buf.transform.eulerAngles.z);
            yield return null;
        }
    }

    private IEnumerator Shoot(GameObject buf) {
        Vector3 scale = buf.transform.localScale;
        buf.transform.localScale = scale / 2;
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        buf.transform.localScale = scale;
        //print("Shoot");
    }

    public void clearTargets() {
        foreach (var target in avaliableTargets) {
            Destroy(target);
        }
    }
}
