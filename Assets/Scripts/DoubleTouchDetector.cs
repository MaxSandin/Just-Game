using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleTouchDetector : MonoBehaviour
{
    public Transform player;
	public float maxTouchPressedTime = 0.5f;
	public float maxTimeBetweenTouches = 0.4f;
	public float maxTouchesDistance = 50;
	
	private RectTransform background;
	private float lastTouchPressedTime;
	private float lastTouchEndedTime;
	private Vector2 lastTouchPosition;

	public static event Action<Quaternion> OnDoubleTouch = delegate { };

	void Start()
    {
		background = GetComponent<RectTransform>();

		lastTouchEndedTime = -1f;
		lastTouchPressedTime = -1f;
		lastTouchPosition = Vector2.zero;
	}

	private void Update()
	{
		#region Comp 
		if (Input.GetMouseButtonDown(0))
		{
			if (!CheckTouchPosition(Input.mousePosition))
				return;

			if (CheckTouchDistance(Input.mousePosition))
			{
				if (lastTouchEndedTime > 0f)
				{
					if (Time.time - lastTouchEndedTime < maxTimeBetweenTouches)
						OnDoubleTouch(CalcPlayerRotation(Input.mousePosition));
				}
			}

			lastTouchPosition = Input.mousePosition;
			lastTouchPressedTime = Time.time;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (CheckTouchDistance(Input.mousePosition))
			{
				if (Time.time - lastTouchPressedTime < maxTouchPressedTime)
					lastTouchEndedTime = Time.time;
				else
					lastTouchEndedTime = -1;

				lastTouchPosition = Input.mousePosition;
			}
		}
		#endregion

		#region Mobile
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			if (!CheckTouchPosition(touch.position))
				return;

			switch (touch.phase)
			{
				case TouchPhase.Began:
					if (CheckTouchDistance(touch.position))
					{
						if (lastTouchEndedTime > 0f)
						{
							if (Time.time - lastTouchEndedTime < maxTimeBetweenTouches)
								OnDoubleTouch(CalcPlayerRotation(touch.position));
						}
					}

					lastTouchPosition = touch.position;
					lastTouchPressedTime = Time.time;
					break;
				case TouchPhase.Ended:
					if (CheckTouchDistance(touch.position))
					{
						if (Time.time - lastTouchPressedTime < maxTouchPressedTime)
							lastTouchEndedTime = Time.time;
						else
							lastTouchEndedTime = -1;

						lastTouchPosition = touch.position;
					}
					break;
			}
		}
		#endregion
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

	bool CheckTouchPosition(Vector2 touchPosition)
	{
		Vector2 localPoint = Vector2.zero;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(background, touchPosition, null, out localPoint))
			return false;

		if (!background.rect.Contains(localPoint))
			return false;

		return true;
	}

	bool CheckTouchDistance(Vector2 touchPosition)
	{
		return (touchPosition - lastTouchPosition).magnitude < maxTouchesDistance;
	}
}
