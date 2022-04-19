using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance { get; private set; }

    public GameObject winPanel;
    public AudioClip winSFX;

    private void Awake()
    {
        Instance = this;
    }

    public static void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        LeanAudio.play(Instance.winSFX, .5f);
    }
}
