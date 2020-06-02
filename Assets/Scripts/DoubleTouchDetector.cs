using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleTouchDetector
{
	private Transform player;
	private float lastTouchPressedTime;
	private float lastTouchEndedTime;
	private Vector2 lastTouchPosition;

	public static event Action OnDoubleTouch = delegate { };

	public DoubleTouchDetector(Transform p)
    {
		player = p;
		lastTouchEndedTime = -1f;
		lastTouchPressedTime = -1f;
		lastTouchPosition = Vector2.zero;
	}

	public bool DetectTouch(Vector2 touchPosition, float maxTouchPressedTime, float maxTimeBetweenTouches, float maxTouchesDistance)
	{
		if(lastTouchPressedTime < 0f)
		{
			if (CheckTouchDistance(touchPosition, maxTouchesDistance))
			{
				if (lastTouchEndedTime > 0f)
				{
					if (Time.time - lastTouchEndedTime < maxTimeBetweenTouches)
						OnDoubleTouch();
				}
			}

			lastTouchPosition = touchPosition;
			lastTouchPressedTime = Time.time;
		}
		else
		{
			if (CheckTouchDistance(touchPosition, maxTouchesDistance))
			{
				if (Time.time - lastTouchPressedTime < maxTouchPressedTime)
					lastTouchEndedTime = Time.time;
				else
					lastTouchEndedTime = -1;
			}

			lastTouchPosition = touchPosition;
			lastTouchPressedTime = -1f;
		}

		return true;
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

	bool CheckTouchDistance(Vector2 touchPosition, float maxTouchesDistance)
	{
		return (touchPosition - lastTouchPosition).magnitude < maxTouchesDistance;
	}
}
