using UnityEngine;
using System.Collections;
using Hell;

public class Missile : MonoBehaviour {

    /// <summary>
    /// The taget i am into
    /// </summary>
    private Vector3 target;

    /// <summary>
    /// My rigid body
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// the time ill take for me to get there
    /// the tracker tracking speed
    /// my lerp rotation speed
    /// </summary>
    private float
        time,
        trackerSpeed = 20.0f,
        rotationSpeed = 1.0f;

    /// <summary>
    /// The force that i'll use to move to my target
    /// the force i'll use to fly up in the sky
    /// </summary>
    [SerializeField]//[Range(1,10)]
    float 
        trackingForce,
        flyingForce;

    /// <summary>
    /// My tracker mechanism
    /// </summary>
    Transform
        tracker;

    /// <summary>
    /// how much i need to be near my target to consider that i am there
    /// </summary>
    private float threshold = 0.1f;
    
    /// <summary>
    /// initialization method
    /// </summary>
    /// <param name="target">my target</param>
    /// <param name="time">how much time i have to get there</param>
    public void Initialize(Vector3 target, float time)
    {
        this.target = target;
        this.time = time;
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        tracker = new GameObject().transform;
        tracker.position = transform.position;
        tracker.parent = transform.parent;
        FlyUp();
        StartCoroutine(Firing());
	}

    /// <summary>
    /// firing routine
    /// </summary>
    IEnumerator Firing()
    {
        yield return new WaitForSeconds(time);

        tracker.position = transform.position;

        yield return new WaitWhile(() => {
            SetTracker();
            GoToTarget();
            return IsTooFarAway;
        });

        OnReachTarget();
    }

    /// <summary>
    /// Fires a HUGE energy upwards
    /// </summary>
    public void FlyUp(){
        rb.AddForce(Vector3.up * flyingForce * 10);
    }

    /// <summary>
    /// Make the tracker look to target and move it to where i am
    /// </summary>
    public void SetTracker(){
        tracker.LookAt(target);
        tracker.transform.position = Vector3.Lerp(tracker.position, transform.position, Time.deltaTime * trackerSpeed);
    }

    /// <summary>
    /// Push me towards the target
    /// </summary>
    private void GoToTarget(){
        transform.rotation = Quaternion.Lerp(transform.rotation, tracker.rotation, Time.deltaTime * rotationSpeed);
        rb.AddForce(transform.forward * trackingForce *1);
    }

    /// <summary>
    /// if the target is too far away from me
    /// </summary>
    private bool IsTooFarAway{
        get{
            return (Vector3.Distance(transform.position, target) > threshold);
        }
    }

    /// <summary>
    /// What happen when i get there
    /// </summary>
    private void OnReachTarget()
    {
        Destroy(transform.parent);
    }
}
