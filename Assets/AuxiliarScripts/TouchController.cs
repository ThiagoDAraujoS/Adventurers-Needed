using UnityEngine;
using System.Collections;
using System;

public class TouchController : MonoBehaviour
{
    
    public delegate void SwipeDelegate( Vector2 initial, Vector2 final, float force);
    public delegate void DragDelegate(Vector2 initial, Vector2 final, Vector2 displaceVector);

    private static TouchController s;
    public static TouchController S
    {
        get
        {
            if (s == null)
                s = FindObjectOfType<TouchController>();
            return s;
        }
    }
    // Use this for initialization
    void Awake() {
        s = this;
        InitializeSwipeDetectionRoutine();
    }

    public event SwipeDelegate OnSwipeRight = delegate { };
    public event SwipeDelegate OnSwipeLeft  = delegate { };
    public event SwipeDelegate OnSwipeDown  = delegate { };
    public event SwipeDelegate OnSwipeUp    = delegate { };
    public event DragDelegate  OnDrag       = delegate { };

    void InitializeSwipeDetectionRoutine()
    {
        //disble muitltouch
        Input.multiTouchEnabled = false;

        //start the swipe detection routine
        StartCoroutine(SwipeDetectionRoutine());
    }

    IEnumerator SwipeDetectionRoutine2()
    {
        Vector2 initialPosition, finalPosition, vector;
        while (true)
        {
            //reset vectors
            initialPosition = Vector2.zero;  finalPosition = Vector2.zero; vector = Vector2.zero;

            //wait while there's no touch
            yield return new WaitWhile(() => Input.touchCount == 0);

            //when save the first moment of the touch in the first vector
            initialPosition = Input.touches[0].position;

            //wait until there's no touch
            yield return new WaitUntil(() => {

                //if is still holding
                if (Input.touchCount > 0) {
                    //updates the final vector
                    finalPosition = Input.touches[0].position;

                    //updates the vector reference
                    vector = finalPosition - initialPosition;

                    //send an OnDrag message
                    OnDrag(initialPosition, finalPosition, vector);
                }

                //return if there're touches
                return Input.touchCount == 0;
            });

            //if the x position is positive send a onSwipeRight
            if (vector.x > 0)
                OnSwipeRight(initialPosition, finalPosition, vector.x);

            //else the x position is negative send a onSwipeLeft
            else if (vector.x < 0)
                OnSwipeLeft(initialPosition, finalPosition, -vector.x);

            //if the y position is positive send a onSwipeRight
            if (vector.y > 0)
                OnSwipeUp(initialPosition, finalPosition, vector.y);

            //else the y position is negative send a onSwipeLeft
            else if (vector.y < 0)
                OnSwipeDown(initialPosition, finalPosition, -vector.y);
  
        }
    }

    IEnumerator SwipeDetectionRoutine()
    {
        Vector2 initialPosition, finalPosition, vector;
        while (true)
        {
            //reset vectors
            initialPosition = Vector2.zero; finalPosition = Vector2.zero; vector = Vector2.zero;

            //wait while there's no touch
            yield return new WaitWhile(() => Input.GetMouseButtonDown(0));

            //when save the first moment of the touch in the first vector
            initialPosition = Input.mousePosition;

            //wait until there's no touch
            yield return new WaitUntil(() => {

                //if is still holding
                if (Input.GetMouseButton(0))
                {
                    //updates the final vector
                    finalPosition = Input.mousePosition;

                    //updates the vector reference
                    vector = finalPosition - initialPosition;

                    //send an OnDrag message
                    OnDrag(initialPosition, finalPosition, vector);
                }

                //return if there're touches
                return Input.GetMouseButtonUp(0);
            });

            //if the x position is positive send a onSwipeRight
            if (vector.x > 0)
                OnSwipeRight(initialPosition, finalPosition, vector.x);

            //else the x position is negative send a onSwipeLeft
            else if (vector.x < 0)
                OnSwipeLeft(initialPosition, finalPosition, -vector.x);

            //if the y position is positive send a onSwipeRight
            if (vector.y > 0)
                OnSwipeUp(initialPosition, finalPosition, vector.y);

            //else the y position is negative send a onSwipeLeft
            else if (vector.y < 0)
                OnSwipeDown(initialPosition, finalPosition, -vector.y);

        }
    }
}
