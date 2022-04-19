using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum BuildState{
        buildMode,
        DemolishMode,
        editMode
    };


    public InputMaster controls;

    public static GameManager Instance { get; private set; }

    [Header("Game")]
    [SerializeField] private GameObject upgradePanel;
    public bool upgradePanelisOpen = false;

    public float timeSpend;
    public float timeBeforeWin;

    [Header("Cash")]
    public int cash;
    [SerializeField] private TextMeshProUGUI cashText;

    [Header("Build Mode")]
    public BuildState buildState;
    [SerializeField] private GameObject buildModeUI;
    [SerializeField] private Transform buildCameraPos;
    [SerializeField] private CinemachineFreeLook normalCam;
    [SerializeField] private CinemachineVirtualCamera buildCam;
    public bool buildMode;
    [SerializeField] private float camSpeed;
    [SerializeField] private float scrollAmmount = 2f;
    [SerializeField] private float rotateAmmount = 2f;


    [SerializeField] private Vector3 moveAmmount;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        cash = PlayerPrefs.GetInt("Cash");
        cashText.text = cash.ToString();
        controls = new InputMaster();

        controls.BuildMode.MoveCam.started += ctx => MoveCam(ctx.ReadValue<Vector2>());
        controls.BuildMode.MoveCam.performed += ctx => MoveCam(ctx.ReadValue<Vector2>());
        controls.BuildMode.MoveCam.canceled += ctx => moveAmmount = Vector3.zero;

        controls.BuildMode.ZoomCam.started += ctx => ZoomCam(ctx.ReadValue<float>());
        controls.BuildMode.ZoomCam.performed += ctx => ZoomCam(ctx.ReadValue<float>());

        controls.BuildMode.RotateCam.performed += ctx => RoateCam(ctx.ReadValue<float>());
        //controls.BuildMode.ZoomCam.canceled += ctx => moveAmmount = Vector3.zero;
    }


    private void Update()
    {
        buildCam.transform.position += moveAmmount.normalized * Time.deltaTime * camSpeed;

        if (buildMode)
        {
            buildModeUI.SetActive(true);
        }
        else
        {
            buildModeUI.SetActive(false);
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            BuildMode();
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (!upgradePanelisOpen)
            {
                upgradePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                upgradePanelisOpen = true;
            }
            else
            {
                upgradePanel.SetActive(false);
                if (!buildMode)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                upgradePanelisOpen = false;
            }
        }
    }

    public void SetBuildMode()
    {
        buildState = BuildState.buildMode;
    }

    public void SetDemolishdMode()
    {
        buildState = BuildState.DemolishMode;
    }

    public void SetEditMode()
    {
        buildState = BuildState.editMode;
    }

    public static int AddCash(int ammount)
    {
        int cashToAdd = Instance.cash + ammount;
        Instance.cashText.text = cashToAdd.ToString();
        PlayerPrefs.SetInt("Cash", cashToAdd);
        return cashToAdd;
    }

    public static int RemoveCash(int ammount)
    {
        int cashToAdd = Instance.cash - ammount;
        Instance.cashText.text = cashToAdd.ToString();
        PlayerPrefs.SetInt("Cash", cashToAdd);
        return cashToAdd;
    }

    void RoateCam(float dir)
    {
        if (!Mouse.current.rightButton.isPressed) { return; }
        if (!buildMode) { return; }

        var buildCamRotation = buildCam.transform.rotation;
        buildCamRotation.y += dir * rotateAmmount;
    }

    void ZoomCam(float direction)
    {
        Debug.Log(direction);
        if (buildCam.m_Lens.FieldOfView >= 20 && buildCam.m_Lens.FieldOfView <= 110)
        {
            if (buildMode)
            {
                if(direction < 0 && buildCam.m_Lens.FieldOfView < 110)
                {
                    Debug.Log("Zoomed In");
                    buildCam.m_Lens.FieldOfView += 1 * scrollAmmount;
                }
                if(direction > 0 && buildCam.m_Lens.FieldOfView > 20)
                {
                    Debug.Log("Zoomed Out");
                    buildCam.m_Lens.FieldOfView -= 1 * scrollAmmount;
                }
               
            }
        }
    }

    private void OnEnable()
    {
        controls.Enable();
        buildCam.m_Lens.FieldOfView = 40;

    }

    private void OnDisable()
    {
        controls.Disable();
        buildCam.m_Lens.FieldOfView = 40;
    }

    void MoveCam(Vector2 direction)
    {
        moveAmmount = new Vector3(direction.x, 0, direction.y) * rotateAmmount;
    }

    public void BuildMode()
    {
        if (!buildMode)
        {
            buildCam.transform.position = new Vector3(37.7000008f, 17.203373f, 41.9f);
            Debug.Log("Gone into build mode.");
            normalCam.enabled = false;
            buildCam.enabled = true;

            buildMode = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Debug.Log("Left build mode.");
            normalCam.enabled = true;
            buildCam.enabled = false;

            buildMode = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
