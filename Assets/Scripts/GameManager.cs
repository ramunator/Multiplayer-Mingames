using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int FrameRate { get; set; }

    private void Awake()
    {
        FrameRate = 240;
        Application.targetFrameRate = FrameRate;
    }
}
