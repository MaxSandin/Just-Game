using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Joystick joystick;
    public Rigidbody rb;

    void Start()
    {
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        transform.Translate(moveDirection * speed * Time.deltaTime);
        //if (Input.touchCount > 0)
        //{
        //    //Store the first touch detected.
        //    Touch myTouch = Input.touches[0];

        //    //Check if the phase of that touch equals Began
        //    if (myTouch.phase == TouchPhase.Began)
        //    {
        //        //If so, set touchOrigin to the position of that touch
        //        touchOrigin = myTouch.position;
        //        moveVector = Vector2.zero;
        //    }

        //    if (myTouch.phase == TouchPhase.Moved)
        //    {
        //        //Set touchEnd to equal the position of this touch
        //        Vector2 touchEnd = myTouch.position;

        //        //Calculate the difference between the beginning and end of the touch on the x axis.
        //        moveVector.x = (int)(touchEnd.x - touchOrigin.x);

        //        //Calculate the difference between the beginning and end of the touch on the y axis.
        //        moveVector.y = (int)(touchEnd.y - touchOrigin.y);
        //    }

        //    if(myTouch.phase == TouchPhase.Ended)
        //    {
        //        //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
        //        touchOrigin.x = -1;
        //        touchOrigin.y = -1;
        //        moveVector = Vector2.zero;
        //    }

        //    transform.Translate(moveVector.x * speed * Time.deltaTime, 0, moveVector.y * speed * Time.deltaTime);

        //}
    }
}
