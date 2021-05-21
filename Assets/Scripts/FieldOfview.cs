using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfview : MonoBehaviour {
    GameObject gridMvmt;
    GridMovement characterList;

    public float radius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public bool canSee = false;

    private void Start() {
        gridMvmt = GameObject.Find("GridMovement");
        characterList = gridMvmt.GetComponent<GridMovement>();
        StartCoroutine("FOVRoutine");
    }

    private IEnumerator FOVRoutine() {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while(true) {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(characterList.GetTransformCurrentCharacter().position, radius, targetMask);
        canSee = false;
        visibleTargets.Clear();
        for(int i = 0; rangeChecks.Length != 0 && i < rangeChecks.Length; i++) {
            Transform target = rangeChecks[i].transform;
            Vector3 directionToTarget = (target.position - characterList.GetTransformCurrentCharacter().position).normalized;

            float distanceToTarget = Vector3.Distance(characterList.GetTransformCurrentCharacter().position, target.position);
            if(!Physics.Raycast(characterList.GetTransformCurrentCharacter().position, directionToTarget, distanceToTarget, obstacleMask)) {
                Debug.DrawRay(characterList.GetTransformCurrentCharacter().position, target.position.normalized, Color.green, 1);
                visibleTargets.Add(target);
                //print("YES");
                canSee = true;
            }
                
        }
        
    }

    /*
    private void Start() {
        gridMvmt = GameObject.Find("GridMovement");
        characterList = gridMvmt.GetComponent<GridMovement>();
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        //Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadiues, targetMask);
        Collider[] targetsInViewRadius = Physics.OverlapSphere(characterList.GetTransformCurrentCharacter().position, viewRadiues, obstacleMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - characterList.GetTransformCurrentCharacter().position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
                float distToTarget = Vector3.Distance(characterList.GetTransformCurrentCharacter().position, target.position);
                if(!Physics.Raycast(characterList.GetTransformCurrentCharacter().position, dirToTarget, distToTarget, obstacleMask)) {
                    visibleTargets.Add(target);
                    print("YES");
                }
                else {
                    print("NO");
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees) {
        return new Vector3(Mathf.Sin(angleInDegrees = Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees = Mathf.Deg2Rad));
    }*/
}
