using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] public float minimumSwipeDistance = 20f;

    private RectTransform background;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public enum SwipeDirection { Up, Down, Left, Right};

    public static event Action<SwipeDirection> OnSwipe = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<RectTransform>();

        startTouchPosition = Vector2.zero;
        endTouchPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (!CheckTouchPosition(touch.position))
                return;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;
                //case TouchPhase.Moved:
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
        else
        {
            startTouchPosition = Vector2.zero;
            endTouchPosition = Vector2.zero;
        }
    }

    bool CheckTouchPosition(Vector2 touchPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(background, touchPosition, null, out localPoint))
            return false;

        if (!background.rect.Contains(localPoint))
        {
            startTouchPosition = Vector2.zero;
            endTouchPosition = Vector2.zero;
            return false;
        }

        return true;
    }

    void DetectSwipe()
    {
        if (startTouchPosition == Vector2.zero || endTouchPosition == Vector2.zero)
            return;

        if(CheckMinSwipeDistance())
        {
            if(IsVerticalSwipe())
            {
                var direction = endTouchPosition.y > startTouchPosition.y ? SwipeDirection.Up : SwipeDirection.Down;
                OnSwipe(direction);
            }
            else
            {
                var direction = endTouchPosition.x > startTouchPosition.x ? SwipeDirection.Right : SwipeDirection.Left;
                OnSwipe(direction);
            }
        }
    }

    bool CheckMinSwipeDistance()
    {
        return Mathf.Abs(endTouchPosition.x - startTouchPosition.x) >= minimumSwipeDistance ||
            Mathf.Abs(endTouchPosition.y - startTouchPosition.y) >= minimumSwipeDistance;
    }

    bool IsVerticalSwipe()
    {
        return Mathf.Abs(endTouchPosition.y - startTouchPosition.y) > Mathf.Abs(endTouchPosition.x - startTouchPosition.x);
    }
}