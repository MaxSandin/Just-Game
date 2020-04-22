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

    public Joystick joystick;
    public float joystickTouchDelay = 1;

    private Vector2 startTouchPosition = -Vector2.one;
    private Vector2 endTouchPosition = -Vector2.one;
    private float lastTimeTouch = -1;
    private bool isPressed = false;

    public struct SwipeData
    {
        public SwipeDetector.SwipeDirection direction;
        public Quaternion rotation;
    };
    public static event Action<SwipeData> OnSwipe = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        swipeDetector = new SwipeDetector(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick != null)
        {
            if (isPressed && endTouchPosition == startTouchPosition 
                && lastTimeTouch > 0 && Time.time - lastTimeTouch >= joystickTouchDelay)
            {
                joystick.Activate(true, startTouchPosition);
                return;
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        endTouchPosition = startTouchPosition = eventData.position;
        lastTimeTouch = Time.time;
        isPressed = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        endTouchPosition = eventData.position;

        // джойстик
        if (joystick.IsActive) // проверим нажат ли джойстик
        {
            joystick.Drag(eventData.position);
            return;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        endTouchPosition = eventData.position;
        lastTimeTouch = Time.time;
        isPressed = false;

        if (joystick.IsActive)
        {
            joystick.Activate(false);
            lastTimeTouch = -1;
            return;
        }

        // свайпы
        if (swipeDetector.DetectSwipe(startTouchPosition, endTouchPosition, minimumSwipeDistance))
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
