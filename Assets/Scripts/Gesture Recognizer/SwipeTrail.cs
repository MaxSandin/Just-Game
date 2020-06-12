using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
    public GameObject trailPrefab;
    private GameObject thisTrail;
    private Vector3 startPos;
    private Plane objPlane;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float rayDistance;

            objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

            if (objPlane.Raycast(mRay, out rayDistance))
            {
                startPos = mRay.GetPoint(rayDistance);
                thisTrail = (GameObject)Instantiate(trailPrefab, startPos, Quaternion.identity);

            }
        }
        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float rayDistance;

            objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
            Debug.DrawRay(mRay.origin, mRay.direction * 10, Color.yellow);
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                if(thisTrail)
                    thisTrail.transform.position = mRay.GetPoint(rayDistance);
            }
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            Destroy(thisTrail);
        }
    }
}