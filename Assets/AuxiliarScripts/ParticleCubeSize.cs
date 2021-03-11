using UnityEngine;
using System.Collections;
using Hell;

public class ParticleCubeSize : MonoBehaviour
{

    public Vector3 newCubeScale;
    private Vector3 baseCubeScale;
    public float duration;
    public AnimationCurve growthCurve;
    private ParticleSystem ps = null;
    private ParticleSystem.ShapeModule shape;

    void Start() {
        ps = GetComponent<ParticleSystem>();
        baseCubeScale = ps.shape.box;
        shape = ps.shape;
        Debug.Log("<color=red>growthCurve.Evaluate(time)</color>");
        StartCoroutine(Stopwatch.PlayUntilReady(duration,
            time => {
                shape.box = Vector3.Lerp(baseCubeScale, newCubeScale, growthCurve.Evaluate(time));
            }));
    }
}
