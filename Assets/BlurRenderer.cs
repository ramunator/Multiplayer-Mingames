using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BlurRenderer : MonoBehaviour
{
    public Camera blurCamera;
    public Material blurMat;

    // Start is called before the first frame update
    void Start()
    {
        if(blurCamera.targetTexture != null)
        {
            blurCamera.targetTexture.Release();
        }
        blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMat.SetTexture("MainTex", blurCamera.targetTexture);
    }
}
