using UnityEngine;
//using UnityEditor;
using System.Collections;

public class CameraShader3 : MonoBehaviour
{
    public static CameraShader3 s;

    public bool ramp;
    public float rampValue;
    // public Shader shader;
    public Material mat;


    public float after_Desaturation   = 0   ;
    public float after_Darken         = 0   ;
    public float after_RedChannel     = 1   ;
    public float after_GreenChannel   = 1   ;
    public float after_BlueChannel    = 1   ;
    public float after_ContrastFactor = 1   ;

    public float _Desaturation ;
    public float _Darken ;
    public float _RedChannel ;
    public float _GreenChannel ;
    public float _BlueChannel;
    public float _ContrastFactor ;


    void Awake ()
    {
        s = this;
        //  mat = new Material( shader );
        Camera.main.depthTextureMode = DepthTextureMode.DepthNormals;
    }
    void Update ()
    {
        if ( ramp )
        {
            rampValue = Mathf.Clamp( rampValue + ( 1 * Time.deltaTime ), 0.0f, 1.0f );
        }
        else
        {
            rampValue = Mathf.Clamp( rampValue - ( 1 * Time.deltaTime ), 0.0f, 1.0f );
        }

        // Time.timeScale = 1 - rampValue;
        mat.SetFloat( "_Desaturation",      Mathf.Lerp( after_Desaturation  , _Desaturation, rampValue ) );
        mat.SetFloat( "_Darken",            Mathf.Lerp( after_Darken        , _Darken, rampValue ) );
        mat.SetFloat( "_RedChannel",        Mathf.Lerp( after_RedChannel    , _RedChannel, rampValue ) );
        mat.SetFloat( "_GreenChannel",      Mathf.Lerp( after_GreenChannel  , _GreenChannel, rampValue ) );
        mat.SetFloat( "_BlueChannel",       Mathf.Lerp( after_BlueChannel   , _BlueChannel, rampValue ) );
        mat.SetFloat( "_ContrastFactor",    Mathf.Lerp( after_ContrastFactor, _ContrastFactor, rampValue ) );
    }
    // Called by the camera to apply the image effect
    void OnRenderImage ( RenderTexture source, RenderTexture destination )
    {

        //mat is the material containing your shader
        Graphics.Blit( source, destination, mat );
    }
}

