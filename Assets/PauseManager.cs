using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.SceneManagement;

public class PauseManager : NetworkBehaviour
{
    public static PauseManager Instance { get; private set; }
    public GameObject pausePanel;

    bool isPaused;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !isPaused)
        {
            Pause();
            isPaused = true;
        }
        else if(Keyboard.current.escapeKey.wasPressedThisFrame && isPaused)
        {
            StopPause();
            isPaused = false;
        }
    }

    public void LoadMainMenu()
    {
        SceneTranstions.Instance.LoadScene("Main Menu");
    }

    public static void Pause()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu") { return; }

        Instance.pausePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator StopPauseCou()
    {
        Instance.pausePanel.GetComponent<Animator>().SetTrigger("StopPause");
        yield return new WaitForSeconds(1);
        Instance.pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void StopPause()
    {
        Instance.StartCoroutine(Instance.StopPauseCou());
    }
}
