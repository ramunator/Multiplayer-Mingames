using Steamworks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NavMainMenu : MonoBehaviour
{
    public static NavMainMenu Instance { get; private set; }

    public NavInput controls;

    public enum ControllerStateEnum
    {
        Xbox,
        Ps4,
        Pc
    }

    public ControllerStateEnum controllerState;

    [Header("Config")]
    public Transform buttons;
    public Transform body;

    public int navIndex;
    public int navHoverIndex;
    public int navPanelButtonsIndex;

    [Header("UI")]
    public PanelGroup panelGroup;
    public RawImage leftShoulder;
    public RawImage rightShoulder;

    public ButtonHover[] buttonHover;

    public int panelButtonsLenght = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        controls = new NavInput();
        controls.MainMenu.ChangeNavIndex.performed += ctx => ChangeNav(ctx.ReadValue<float>());
        controls.MainMenu.ChangeNavPanelIndex.performed += ctx => ChangeNavPanel(ctx.ReadValue<float>());
        controls.MainMenu.TriggerNavPanel.performed += ctx => TriggerNavPanel();
    }
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            ChangeNavPanel(-1);
        }
        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            ChangeNavPanel(1);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
        if (SteamManager.Initialized)
        {
            /*InputHandle_t controller1Handle = SteamInput.GetControllerForGamepadIndex(1);
            ESteamInputType inputType = SteamInput.GetInputTypeForHandle(controller1Handle);
            switch (inputType)
            {
                case ESteamInputType.k_ESteamInputType_Unknown:
                    Console.Instance.AnswerCommand("Unkown Device Detected"); break;
                case ESteamInputType.k_ESteamInputType_SteamController:
                    Console.Instance.AnswerCommand("Steam Controller Detected"); break;
                case ESteamInputType.k_ESteamInputType_PS4Controller:
                    Console.Instance.AnswerCommand("PS4 Detected"); 
                    string left = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_PS4_LeftBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, 2); ;
                    string right = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_PS4_RightBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, 2); ;
                    GetTexture(left, leftShoulder);
                    GetTexture(right, rightShoulder);
                    break;
                case ESteamInputType.k_ESteamInputType_XBoxOneController:
                    Console.Instance.AnswerCommand("Xbox One Detected");
                    string leftXbox = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_XBoxOne_RightBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, 2); ;
                    string rightXbox = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_XBoxOne_RightBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, 2); ;
                    GetTexture(leftXbox, leftShoulder);
                    GetTexture(rightXbox, rightShoulder);
                    break;
                case ESteamInputType.k_ESteamInputType_GenericGamepad:
                    Console.Instance.AnswerCommand("Generic Gamepad Detected"); break;
            }*/

            UpdateUI(0);
        }
    }

    public void UpdateUI(uint id)
    {
        if (controllerState == ControllerStateEnum.Ps4)
        {
            leftShoulder.enabled = true;
            rightShoulder.enabled = true;
            string left = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_PS4_LeftBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, id); 
            string right = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_PS4_RightBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, id); 
            GetTexture(left, leftShoulder);
            GetTexture(right, rightShoulder);
        }
        else if (controllerState == ControllerStateEnum.Xbox)
        {
            rightShoulder.enabled = true;
            leftShoulder.enabled = true;
            string leftXbox = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_XBoxOne_RightBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, id); 
            string rightXbox = SteamInput.GetGlyphPNGForActionOrigin(EInputActionOrigin.k_EInputActionOrigin_XBoxOne_RightBumper, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, id); 
            GetTexture(leftXbox, leftShoulder);
            GetTexture(rightXbox, rightShoulder);
        }
    }

    public void GetTexture(string url, RawImage image)
    {
        StartCoroutine(GetCoroutine(url, image));
    }

    private IEnumerator GetCoroutine(string url, RawImage image)
    {
        using(UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return unityWebRequest.SendWebRequest();

            if(unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
                Debug.Log("Received " + unityWebRequest.downloadHandler.data);
                Texture myTexture = DownloadHandlerTexture.GetContent(unityWebRequest);
                image.texture = myTexture;
            }
        }
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void TriggerNavPanel()
    {
        if (body.GetChild(navIndex).childCount < 1) { return; }

        GetButtons();

        panelButtonsLenght = 0;

        for (int i = 0; i < buttonHover.Length; i++)
        {
            if (buttonHover[i] != null)
            {
                panelButtonsLenght += 1;
            }
        }

        buttonHover[navPanelButtonsIndex].OnButtonClickedEvent?.Invoke();

        Console.Instance.AnswerCommand(navPanelButtonsIndex.ToString());
    }

    /// <summary>
    /// <para> Changes the current nav index </para>
    /// <para> If value is set to -1 we get the value by getting the child index </para>
    /// </summary>
    private void ChangeNav(float value)
    {
        buttons.GetChild(navIndex).GetComponent<ButtonHover>().OnButtonDeClickedEvent?.Invoke();

        //We pressed the right shoulder buttons aka R1
        if (value > 0)
        {
            navIndex += 1;
        }
        //We pressed the left shoulder buttons aka L1
        else if (value < 0)
        {
            navIndex -= 1;
        }
        navIndex = Mathf.Clamp(navIndex, 0, buttons.childCount - 1);

        buttons.GetChild(navIndex).GetComponent<ButtonHover>().OnButtonClickedEvent?.Invoke();

        Console.Instance.AnswerCommand(navIndex.ToString());

        panelGroup.ShowCurrentPanel();
    }

    private void GetButtons()
    {
        //Array.Clear(buttonHover, 0, buttonHover.Length);



        //for (int i = 0; i < body.GetChild(navIndex).childCount; i++)
        //{
        //    Debug.Log(i);
        //    if (body.GetChild(navIndex).GetComponent<MenuPanel>().buttons.Count - 1 > i)
        //    {
        //        body.GetChild(navIndex).GetChild(i).TryGetComponent<ButtonHover>(out buttonHover[i]);
        //        if (buttonHover[i] != null)
        //        {
        //            Console.Instance.AnswerCommand($"<color=green>We found an button with the index of {i} and a name of '{buttonHover[i].name}'!");
        //        }
        //    }
        //    else
        //    {
        //        for (int j = 0; j < body.GetChild(navIndex).GetChild(i).GetChild(body.GetChild(navIndex).GetChild(body.GetChild(navIndex).childCount - 1).childCount - 1).childCount; j++)
        //        {
        //            body.GetChild(navIndex).GetChild(i).GetChild(j).TryGetComponent<ButtonHover>(out buttonHover[i]);
        //            if (buttonHover[i] != null)
        //            {
        //                Console.Instance.AnswerCommand($"<color=green>We found an button with the index of {i} and a name of '{buttonHover[i].name}'!");
        //            }
        //            else
        //            {
        //                Console.Instance.AnswerCommand($"<color=red>Error: We didnt find any button with the index of {i}!");
        //            }       
        //        }
        //    }
        //}

        buttonHover = body.GetChild(navIndex).GetComponent<MenuPanel>().buttons.ToArray();
    }

    private void ChangeNavPanel(float value)
    {
        if(body.GetChild(navIndex).childCount < 1) { return; }

        GetButtons();

        panelButtonsLenght = 0;

        for (int i = 0; i < buttonHover.Length; i++)
        {
            if (buttonHover[i] != null)
            {
                panelButtonsLenght += 1;
            }
        }

        buttonHover[navPanelButtonsIndex].OnPointerExitedEvent?.Invoke();

        //We pressed the right shoulder buttons aka R1
        if (value > 0)
        {
            navPanelButtonsIndex += 1;
        }
        //We pressed the left shoulder buttons aka L1
        else if (value < 0)
        {
            navPanelButtonsIndex -= 1;
        }
        navPanelButtonsIndex = Mathf.Clamp(navPanelButtonsIndex, 0, panelButtonsLenght - 1);

        buttonHover[navPanelButtonsIndex].OnPointerHoverEvent?.Invoke();

        GetButtons();

        Console.Instance.AnswerCommand(navPanelButtonsIndex.ToString());
    }

    public void ChangeButtonBorderImageOnDeselect()
    {
        foreach (Transform item in buttons.gameObject.transform)
        {
            if (item.GetComponent<NavButton>().index != buttons.GetChild(navIndex).GetComponent<NavButton>().index)
            {
                Debug.Log("Test One " + buttons.GetChild(navIndex).GetComponent<NavButton>().index);
                CanvasGroup canvasGroup = item.Find("Border").GetComponent<CanvasGroup>();
                item.GetComponent<ButtonHover>().AlphaChangeObjectImageZero(canvasGroup);
            }
        }
    }

    public void ChangeNavMouse(int value)
    {
        buttons.GetChild(navIndex).GetComponent<ButtonHover>().OnButtonDeClickedEvent?.Invoke();

        navIndex = value;

        navIndex = Mathf.Clamp(navIndex, 0, buttons.childCount - 1);

        panelGroup.ShowCurrentPanel();
    }

    public void ChangeNavMouseHover(int value)
    {
        navHoverIndex = value;

        navHoverIndex = Mathf.Clamp(navHoverIndex, 0, buttons.childCount - 1);
    }

}
