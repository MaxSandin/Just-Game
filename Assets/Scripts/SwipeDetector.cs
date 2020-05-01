using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector
{
    private Transform player;

    public enum SwipeDirection { Up, Down, Left, Right};
    public struct SwipeData
    {
        public SwipeDirection direction;
        public Quaternion rotation;
    };
    public static event Action<SwipeData> OnSwipe = delegate { };

    public SwipeDetector(Transform player)
    {
        this.player = player;
    }

    public bool DetectSwipe(Vector2 startTouchPosition, Vector2 endTouchPosition, float minimumSwipeDistance)
    {
        if (startTouchPosition == Vector2.zero || endTouchPosition == Vector2.zero)
            return false;

        if (CheckMinSwipeDistance(startTouchPosition, endTouchPosition, minimumSwipeDistance))
        {
            SwipeData data = new SwipeData();
            Vector2 position = startTouchPosition + (endTouchPosition - startTouchPosition) / 2; // TODO: придумать хорошее распознавание направления
            //data.rotation = CalcPlayerRotation(position);

            if (IsVerticalSwipe(startTouchPosition, endTouchPosition))
            {
                data.direction = endTouchPosition.y > startTouchPosition.y ? SwipeDirection.Up : SwipeDirection.Down;
                OnSwipe(data);
            }
            else
            {
                data.direction = endTouchPosition.x > startTouchPosition.x ? SwipeDirection.Right : SwipeDirection.Left;
                OnSwipe(data);
            }
            return true;
        }

        return false;
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
    bool CheckMinSwipeDistance(Vector2 startTouchPosition, Vector2 endTouchPosition, float minimumSwipeDistance)
    {
        return Mathf.Abs(endTouchPosition.x - startTouchPosition.x) >= minimumSwipeDistance ||
            Mathf.Abs(endTouchPosition.y - startTouchPosition.y) >= minimumSwipeDistance;
    }

    bool IsVerticalSwipe(Vector2 startTouchPosition, Vector2 endTouchPosition)
    {
        return Mathf.Abs(endTouchPosition.y - startTouchPosition.y) > Mathf.Abs(endTouchPosition.x - startTouchPosition.x);
    }
}