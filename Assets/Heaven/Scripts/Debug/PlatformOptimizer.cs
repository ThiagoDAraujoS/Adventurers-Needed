using UnityEngine;
using System.Collections.Generic;

public enum PlatformBuild
{
    android,
    pc
}

public class PlatformOptimizer : MonoBehaviour {

    public PlatformBuild exclusivePlatform;

	void Awake ()
    {

    #if UNITY_ANDROID //&& !UNITY_EDITOR
        if(exclusivePlatform != PlatformBuild.android)
            Destroy(gameObject);
    #endif

    #if UNITY_STANDALONE_WIN //&& !UNITY_EDITOR
        if (exclusivePlatform != PlatformBuild.pc)
            Destroy(gameObject);
    #endif
    }
}
