using UnityEngine;
using System.Collections;

public class PinchDragInput : MonoBehaviour
{
    public bool oneFingerTouch;
    public float touchScalar = 0.05f;

    [Range (0.5f, 2.5f)]
    public float minZoom = 1.0f;
    private float maxZoom;

    private Camera mapCam;
    private float maxCamSize;
    private float currentCamSize;
    
    private float minEdge, maxEdge;

    private Transform originalParent, camParent;
    private Vector3 originalPos, limitedCameraPosition;

    //Variables for http://www.theappguruz.com/blog/pinch-zoom-panning-unity code
    private float scrollVelocity = 0.0f;
    private float timeTouchPhaseEnded;
    private Vector2 scrollDirection = Vector2.zero;
    //

    void Start()
    {
        mapCam = GetComponent<Camera>();
        UpdateCamSize(mapCam.orthographicSize);

        CreateParentOnject();

        ConstrainEdges();
    }

    void SingleTouch()
    {
        Touch touch = Input.GetTouch(0);

        // These lines of code will pan/drag the object around untill the edge of the image
        if (oneFingerTouch && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            mapCam.transform.localPosition -= (Vector3)touch.deltaPosition * 0.01f;

            ConstrainEdges();
        }
        // On double tap image will be set at original position and scale
        else if (touch.phase == TouchPhase.Began && touch.tapCount == 2)
        {
            mapCam.orthographicSize = maxCamSize;
            transform.position = originalPos;
        }
    }

    void MultiTouch()
    {
        Touch[] touches = Input.touches;

        Vector3 cameraViewsize = new Vector3(mapCam.pixelWidth, mapCam.pixelHeight);

        Vector3 touchOnePrevPos = touches[0].position - touches[0].deltaPosition;
        Vector3 touchTwoPrevPos = touches[1].position - touches[1].deltaPosition;


        //Function From: http://www.theappguruz.com/blog/pinch-zoom-panning-unity
        // I opted not to reinvent the entire wheel on pinch zooming
        float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
        float touchDeltaMag = (touches[0].position - touches[1].position).magnitude;

        float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

        mapCam.transform.position += mapCam.transform.TransformDirection((touchOnePrevPos + touchTwoPrevPos - cameraViewsize) * mapCam.orthographicSize / cameraViewsize.y);

        mapCam.orthographicSize += deltaMagDiff * touchScalar;
        mapCam.orthographicSize = Mathf.Clamp(mapCam.orthographicSize, minZoom, maxZoom) - 0.001f;

        mapCam.transform.position -= mapCam.transform.TransformDirection(((Vector3)touches[0].position + (Vector3)touches[1].position - cameraViewsize) * mapCam.orthographicSize / cameraViewsize.y);
        //

        ConstrainEdges();
    }

    void Update()
    {
        if (Input.touchCount == 1)
            SingleTouch();
        else if (Input.touchCount > 1)
            MultiTouch();
        else if (scrollVelocity != 0.0f)
        {
            //Snippet From: http://www.theappguruz.com/blog/pinch-zoom-panning-unity
            float t = (Time.time - timeTouchPhaseEnded);
            float frameVelocity = Mathf.Lerp(scrollVelocity, 0.0f, t);
            mapCam.transform.position += -(Vector3)scrollDirection.normalized * (frameVelocity * 0.05f) * Time.deltaTime;

            if (t >= 1.0f)
                scrollVelocity = 0.0f;
            //
        }
    }

    void UpdateCamSize(float newHeight)
    {
        maxCamSize = newHeight;
        maxZoom = newHeight;
    }

    void CreateParentOnject()
    {
        //Create A parent to the camera at runtime
        originalParent = mapCam.transform.parent;
        originalPos = transform.position;

        camParent = new GameObject("Cam Anchor").transform;
        camParent.position = transform.position;
        camParent.rotation = transform.rotation;

        camParent.parent = originalParent;
        transform.parent = camParent;
        transform.localEulerAngles = Vector3.zero;
    }

    void ConstrainEdges()
    {
        //Get current cam dimensions
        currentCamSize = mapCam.orthographicSize;

        minEdge = currentCamSize - maxCamSize;
        maxEdge = maxCamSize - currentCamSize;

        limitedCameraPosition = mapCam.transform.localPosition;
        limitedCameraPosition.x = Mathf.Clamp(limitedCameraPosition.x, minEdge * mapCam.aspect, maxEdge * mapCam.aspect);
        limitedCameraPosition.y = Mathf.Clamp(limitedCameraPosition.y, minEdge, maxEdge);

        mapCam.transform.localPosition = limitedCameraPosition;
    }
}
