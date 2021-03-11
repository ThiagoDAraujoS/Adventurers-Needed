using UnityEngine;
using System.Collections;
using Hell;

public class MagicProjector : MonoBehaviour {

    public Light light;

    public Projector projector;

    public Texture2D image;

    public Texture2D ramp;

    public Shader projectorShader;

    public float fadein;

    private float lightMaxint;

    // Use this for initialization
    void Start()
    {
        Material mat = new Material(projectorShader);
        projector.material = mat;
        mat.SetTexture("_ShadowTex", image);

        lightMaxint = light.intensity;
        light.intensity = 0;

        StartCoroutine(Stopwatch.PlayUntilReady(fadein, time => {
            projector.material.SetFloat("_Intensity",
            Mathf.Lerp(0.0f, 1.5f, time));
            Mathf.Lerp(0.0f, lightMaxint, time);
        }));
    }

    protected void DestroyThis()
    {
        StartCoroutine(KillRoutine());
    }
    IEnumerator KillRoutine()
    {
        yield return Stopwatch.PlayUntilReady(fadein, time =>
        {
            projector.material.SetFloat("_Intensity",
                Mathf.Lerp(projector.material.GetFloat("_Intensity"), 0.0f, time));
            light.intensity = Mathf.Lerp(light.intensity, 0.0f, time);
        });
    }
}
