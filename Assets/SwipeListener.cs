using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SwipeListener : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private ActionQueue aQ;
    private TopBar tB;

    public GameObject hoverParticles;
    public AnimationCurve rotationCurve;

    [Range (1, 100)]
    public int resetSpeed = 50;

    public int sendThreshold = 1280;
    
    private int isSnapping = 0;

    public RectTransform dialogBox;
    public RectTransform envelopeLip;

    public Vector2 initialPos;

    private float scaleFix;

    public PlanNEW gamePlan;

	// Use this for initialization
	void Start () {
        scaleFix = 1536 / Screen.height;
        initialPos = dialogBox.anchoredPosition;
        aQ = FindObjectOfType<ActionQueue>();
        tB = FindObjectOfType<TopBar>();
	}
	
	// Update is called once per frame
	void Update () {
        //Update size constantly when in edit mode
        #if UNITY_EDITOR
            scaleFix = 1536 / Screen.height;
        #endif

        //Snap back to reality
        if (isSnapping != 0)
        {
            dialogBox.anchoredPosition += new Vector2(0, resetSpeed);
            if(dialogBox.anchoredPosition.y <= initialPos.y) {
                isSnapping = 0;
                dialogBox.anchoredPosition = initialPos;
            }
            SetRotation(dialogBox.anchoredPosition.y);
        }
	}

    public void OnDrag(PointerEventData eventData)
    {
        if (isSnapping != 0) { return; }

        float newPos = Input.mousePosition.y * scaleFix;
        dialogBox.anchoredPosition = new Vector2(0, newPos);
        envelopeLip.localRotation = SetRotation(newPos);

        DrawParticles();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSnapping != 0) { return; }

        if (dialogBox.anchoredPosition.y > sendThreshold)
        {
            SendMessaage();

            //call some method
            isSnapping = resetSpeed;

            Invoke("ResetDialog", 1.0f);

            return;
        }
        else if (dialogBox.anchoredPosition.y > initialPos.y)
        {
            isSnapping = -resetSpeed;
        }
        else if (dialogBox.anchoredPosition.y < initialPos.y)
        {
            isSnapping = resetSpeed;
        }
    }

    private Quaternion SetRotation(float yPos)
    {
        float normalized = (yPos - initialPos.y) / (sendThreshold - initialPos.y);

        normalized = rotationCurve.Evaluate(normalized);

        Quaternion newAngle = Quaternion.Euler(180, 0, 0);

        if(normalized > 0)
            newAngle = Quaternion.Euler(180 * normalized + 180, 0, 0);

        return newAngle;
    }

    private void DrawParticles()
    {
        
    }

    private void ResetDialog()
    {
        tB.SetDialogOverlay(false);
        isSnapping = 0;
        dialogBox.anchoredPosition = initialPos;
    }

    private void SendMessaage()
    {
        //"Don't F this." - Tanner Steele
        gamePlan = aQ.plan;

        gamePlan.DebugMessage();

        string jsonPlan = JsonUtility.ToJson(gamePlan);

        if (ServerProxyObject.s != null)
        {
            ServerProxyObject.s.photonView.RPC("FinishPlanningEvent", PhotonTargets.All, jsonPlan);
        }

        aQ.ClearActionQueue(0);
    }
}
