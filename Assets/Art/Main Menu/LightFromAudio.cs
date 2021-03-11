using UnityEngine;
using System.Collections;

public class LightFromAudio : MonoBehaviour {

    public bool showWaveForm;

    public AudioSource gameMusic;

    private Light menuLight;

    float[] spectrum = new float[128];

    // Use this for initialization
    void Start()
    {
        menuLight = GetComponent<Light>();
    }

    void Update()
    {
        gameMusic.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        if (showWaveForm)
        {
            ShowWaveForm();
        }

        ChangeLight(0.5f, 2);
    }

    void ChangeLight(float scale, float max)
    {
        float intensity = Mathf.Abs(Mathf.Log(spectrum[1])) * scale;

        menuLight.intensity = Mathf.Clamp(intensity, 1, max);
    }

    void ShowWaveForm()
    {
        print(Mathf.Log(spectrum[1]));

        int i = 1;

        while (i < spectrum.Length - 1)
        {
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);

            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);

            i++;
        }
    }
}
