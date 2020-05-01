using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHolder : MonoBehaviour
{
    public float minCuttingVelocity = 0.001f;

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private Camera cam;
    private Vector3 previousPosition;
    private Transform player;
    private bool isEnemySwiped;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        cam = Camera.main;
        isEnemySwiped = false;
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    public void UpdateSwipe(Vector2 swipePosition)
    {
        Plane playerPlane = new Plane(Vector3.up, player.position);
        Ray ray = Camera.main.ScreenPointToRay(swipePosition);

        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 newPosition = ray.GetPoint(hitdist);
            rb.position = newPosition;

            float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
            if (velocity > minCuttingVelocity)
                sphereCollider.enabled = true;
            else
                sphereCollider.enabled = false;

            previousPosition = newPosition;
        }
    }

    public void StartSwipe(Vector2 swipePosition)
    {
        // что бы коллайдер не телепортировался при нажатиях в разных частях экрана
        previousPosition = cam.ScreenToWorldPoint(swipePosition);
        sphereCollider.enabled = false;
        isEnemySwiped = false;
    }

    public void StopSwipe()
    {
        sphereCollider.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            isEnemySwiped = true;
        }
    }

    public bool ChekEnemy()
    {
        if (isEnemySwiped)
        {
            isEnemySwiped = false;
            return true;
        }

        return false;
    }
}
