using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeveloperConsoleBehaviour : MonoBehaviour
{
    private static DeveloperConsoleBehaviour Instance;

    private DeveloperConsole developerConsole;

    private DeveloperConsole DeveloperConsole
    {
        get
        {
            if(developerConsole != null) { return developerConsole; }
            return developerConsole = new DeveloperConsole(prefix, commands);
        }
    }

    [SerializeField] private string prefix = string.Empty;
    [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

    [Header("UI")]
    [SerializeField] private GameObject uiCanvas = null;
    [SerializeField] private TMP_InputField inputField = null;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) { return; }

        if (uiCanvas.activeSelf)
        {
            uiCanvas.SetActive(true);
        }
        else
        {
            uiCanvas.SetActive(true);
            inputField.ActivateInputField();
        }
    }
}