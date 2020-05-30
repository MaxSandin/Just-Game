using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionAreaController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform player;

    public float minimumSwipeDistance = 20f;
    private SwipeDetector swipeDetector;

    public DoubleTouchDetector touchDetector;
    public float maxTouchPressedTime = 0.5f;
    public float maxTimeBetweenTouches = 0.4f;
    public float maxTouchesDistance = 50;

    private Vector2 startTouchPosition = -Vector2.one;
    private Vector2 endTouchPosition = -Vector2.one;
    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        swipeDetector = new SwipeDetector(player);
        touchDetector = new DoubleTouchDetector(player);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        endTouchPosition = startTouchPosition = eventData.position;
        isPressed = true;

        if (touchDetector.DetectTouch(startTouchPosition, maxTouchPressedTime, maxTimeBetweenTouches, maxTouchesDistance))
            return;
    }

    public void OnDrag(PointerEventData eventData)
    {
        endTouchPosition = eventData.position;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        endTouchPosition = eventData.position;
        isPressed = false;

        // свайпы
        if (swipeDetector.DetectSwipe(startTouchPosition, endTouchPosition, minimumSwipeDistance))
            return;

        if (touchDetector.DetectTouch(endTouchPosition, maxTouchPressedTime, maxTimeBetweenTouches, maxTouchesDistance))
            return;
    }

    Quaternion CalcPlayerRotation(Vector3 position)
    {
        Plane playerPlane = new Plane(Vector3.up, player.position);

        Ray ray = Camera.main.ScreenPointToRay(position);

        float hitdist = 0.0f;
        Quaternion playerRotation = player.rotation;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            playerRotation = Quaternion.LookRotation(targetPoint - player.position);
        }

        return playerRotation;
    }
}
