using UnityEngine;
using System.Collections.Generic;
using Hell;
//using DG.Tweening;

public class CenterCamera : MonoBehaviour {

    //Variables to handle character positions
    public float widthScalar, heightScaler;

    [SerializeField]
    private float left, right, top, bottom;

    private Camera sceneCamera;

    //Variables to handle lerp
    private bool center = true;

    private Vector3 targetPos, startPos;

    [Tooltip ("This determine how fast camera moves to and from")]
    public float lerpTime = 3;
    public AnimationCurve easeFactor;

    private float startTime, waitTime;

    [SerializeField]
    private float travelTime;

    private float journeyLength;
    private float distCovered, fracJourney;


	// Use this for initialization
	void Start () {
        sceneCamera = Camera.main;// GameObject.Find("Iso Camera").GetComponent<Camera>();

        UpdateCharacterPositions();
	}
	
	// Update is called once per frame
	void Update () {

        //Is Auto Center enabled
        if (center)
        {
            UpdateCharacterPositions();

            ChangeCameraSize();
        }
        else if (fracJourney > 1)
        {
            fracJourney = 1;
            Invoke("ReturnToCenter", waitTime);

        }
        else if (fracJourney < 1)
        {
            travelTime += Time.deltaTime;
            distCovered = (Time.time - startTime) * lerpTime;
            fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, targetPos, easeFactor.Evaluate(fracJourney));
        }

        //Can lerp to specific position when instructed if center = false;
	}

    void UpdateCharacterPositions()
    {
        //Get the transforms of all characters (enemies and players)
        left = right = top = bottom = 0;

        int characterCount = 0;
        Vector3 averagePos = Vector3.zero;

        foreach (Character characterCol in FindObjectsOfType<Character>())
        {
            SetCameraBoundaries(characterCol.transform.position);

            characterCount++;
            averagePos += characterCol.transform.position;
        }

        transform.position = averagePos / characterCount;
    }

    void SetCameraBoundaries(Vector3 charPos)
    {
        float hDistance = charPos.x + charPos.z;
        float vDistance = charPos.z - charPos.x;

        //Set edge cases based on outermost characters
        if (hDistance < left) { left = hDistance; }
        if (hDistance > right) { right = hDistance; }
        if (vDistance < bottom) { bottom = vDistance; }
        if (vDistance > top) { top = vDistance; }
    }

    void ChangeCameraSize() {

        float camWidth = Mathf.Abs(left) + Mathf.Abs(right);
        float camHeight = Mathf.Abs(top) + Mathf.Abs(bottom) * heightScaler;

        sceneCamera.orthographicSize = Mathf.Max(camWidth, camHeight) * widthScalar;
    }

    public void SetTargetPosition(Vector3 newPos, float duration)
    {
        startTime = Time.time;
        travelTime = 0;

        startPos = transform.position;
        targetPos = newPos;
        waitTime = duration;

        journeyLength = Vector3.Distance(transform.position, targetPos);

        center = false;
    }

    private void ReturnToCenter()
    {
        Invoke("EnableCenterMode", travelTime);
        distCovered = fracJourney = 0;
        startTime = Time.time;

        //Set vectors to return to
        targetPos = startPos;
        startPos = transform.position;

        journeyLength = Vector3.Distance(transform.position, targetPos);
    }

    public void EnableCenterMode()
    {
        center = true;
    }
}
