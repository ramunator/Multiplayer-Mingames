using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;
using Mirror;
using Cinemachine;
using Steamworks;
using StarterAssets;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;
using EZCameraShake;
using UnityEngine.UI;

public class NetworkPlayerController : NetworkBehaviour 
{
    public Holdable currentHoldableItem;

    public InputMaster controls;


    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    public Transform popUpMenuInteraction;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = .1f;
    public SkinnedMeshRenderer playerMeshRenderer;
    [SerializeField] private List<Material> playerMaterials = new List<Material>();
    [SerializeField] private RawImage playerProfilePic;
    [SerializeField] private ParticleSystem bloodParticle;
    public GameObject hand;
    [SerializeField] private LayerMask pickupLayer;
    public IPickup Inventory;
    public Transform inventoryObject;

    float turnSmoothVelocity;
    float walkSFXDelay;
    
    public float jumpVel;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveDirection;
    private Vector3 moveDir;
    bool hasObjectInHand;

    [Space(15)]

    public float CameraAngleOverride = 0.0f;

    public GameObject CinemachineCameraTarget;

    public float TopClamp = 70.0f;

    public float BottomClamp = -30.0f;

    private StarterAssetsInputs _input;

    private const float _threshold = 0.01f;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    //GroundCheck

    [Space(15)]
    public float gravity = -9.81f;
    public LayerMask ground;
    public Transform groundCheck;
    private Vector3 vel;
    private bool isGrounded;

    public EXboxOrigin XBoxOrigin;
    public InputHandle_t handles;
    public InputDigitalActionHandle_t action;

    public InputUser input_user_;
    // Start is called before the first frame update
    void Awake()
    {
        

        controls = new InputMaster();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        _input = GetComponent<StarterAssetsInputs>();



        Application.targetFrameRate = 60;
    }

    public static Texture2D GetSteamImageAsTexture2D(int iImage)
    {
        if (!SteamManager.Initialized) { return null; }

        Texture2D ret = null;
        uint ImageWidth;
        uint ImageHeight;
        bool bIsValid = SteamUtils.GetImageSize(iImage, out ImageWidth, out ImageHeight);

        if (bIsValid)
        {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];

            bIsValid = SteamUtils.GetImageRGBA(iImage, Image, (int)(ImageWidth * ImageHeight * 4));
            if (bIsValid)
            {
                ret = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
                ret.LoadRawTextureData(Image);
                ret.Apply();
            }
        }

        return ret;
    }

    public void CheckForEnablePlayerComp()
    {
        if (hasAuthority && SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            PlayerSpawnManager.SearchForSpawns();
            PlayerSpawnManager.PlayerSpawnPos(PlayerSpawnManager.SpawnState.Random, gameObject);
 
            transform.Find("CM FreeLook1").GetComponent<CinemachineVirtualCamera>().enabled = true;
            GetComponent<playerObjectController>().playerNameText.enabled = false;
            playerProfilePic.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            controls.Player.Move.started += ctx => Move(ctx.ReadValue<Vector2>());
            controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
            controls.Player.Move.canceled += ctx => moveDirection = Vector3.zero;

            controls.Player.Jump.performed += ctx => CmdJump();

            controls.Player.Shoot.performed += ctx => CmdShoot();

        }
    }

    public void Test(EXboxOrigin origin)
    {
        //Debug.Log(SteamInput.GetDigitalActionData(handles, action).bState);
        InputActionSetHandle_t setHandle = SteamInput.GetActionSetHandle("Move");
        //Debug.Log(setHandle);
    }

    public override void OnStartAuthority()
    {
        enabled = true;

        GetComponent<playerObjectController>().SetPlayerValues();
    }
    
    private void LateUpdate()
    {
        CameraRotation();
    }

    [ClientCallback]
    private void OnEnable()
    {
        controls.Enable();
    }

    [ClientCallback]
    private void OnDisable()
    {
        controls.Disable();
    }

    public void RemoveObjectInHand()
    {
        Inventory = null;
        inventoryObject = null;
        hasObjectInHand = false;
    }

    [Command()]
    private void CmdJump()
    {
        if (!isGrounded || !hasAuthority) { return; }

        anim.SetBool("Jump", true);

        AudioManager.instance.Play("PlayerJump");

        vel.y = Mathf.Sqrt(jumpVel * -2.0f * gravity);
    }

    [Command()]
    private void CmdShoot()
    {
        if(!GetComponent<playerObjectController>().gun.TryGetComponent<Gun>(out Gun gun)) { return; }

        if(gun != null)
        {
            StartCoroutine(gun.Attack());
        }
    }

    private void CameraRotation()
    {

        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= _threshold)
        {
            _cinemachineTargetYaw += _input.look.x * Time.deltaTime;
            _cinemachineTargetPitch += _input.look.y * Time.deltaTime;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }
 
    public IEnumerator Die()
    {
        AudioManager.instance.Play("PlayerDie");
        var bloodParticleInstance = Instantiate(bloodParticle);
        bloodParticleInstance.transform.position = transform.Find("Root Transform").position;
        NetworkServer.Destroy(this.gameObject);
        Destroy(this.gameObject);

        CameraShaker.Instance.ShakeOnce(.4f, .4f, .1f, .75f);

        yield return new WaitForSeconds(.25f);
        Destroy(bloodParticleInstance);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    [Client]
    private void Move(Vector2 direction)
    {
        if (!isLocalPlayer) { return; }

        moveDirection = new Vector3(direction.x, 0, direction.y);
    }

    private void CheckPickup()
    {
        if (!isLocalPlayer) { return; }

        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, pickupLayer);    

        if(colliders.Length <= 0) { popUpMenuInteraction.gameObject.SetActive(false); return; }

        if (colliders[0].TryGetComponent<PlayerInteraction>(out PlayerInteraction interaction))
        {
            popUpMenuInteraction.gameObject.SetActive(true);

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                popUpMenuInteraction.gameObject.SetActive(false);
                interaction.DoStuff();
            }
        }
    }

    [Command]
    void CmdSyncPosRot(Vector3 localPosition, Quaternion localRotation)
    {
        RpcSyncPosRot(localPosition, localRotation);
    }

    [ClientRpc]
    void RpcSyncPosRot(Vector3 localPosition, Quaternion localRotation)
    {
        if (!isLocalPlayer)
        {
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
        }
    }

    [ClientCallback]
    private void Update()
    {
        CheckPickup();

        walkSFXDelay += Time.deltaTime;

        if (isLocalPlayer && SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            if(GetComponent<CharacterController>().enabled == false)
            {
                CheckForEnablePlayerComp();
            }
        }

        if (moveDirection.magnitude >= 0.1f)
        {
            cam = Camera.main.transform;
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            anim.SetBool("Running", true);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            if (walkSFXDelay > Random.Range(.3f, .55f) && isGrounded)
            {
                AudioManager.instance.Play("PlayerWalk");
                walkSFXDelay = 0;
            }
        }
        else if(moveDirection.magnitude <= 0.1f)
        {
            anim.SetBool("Running", false);
            moveDirection = Vector3.zero;
        }

        else if (!hasAuthority && SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            GetComponent<playerObjectController>().playerNameText.enabled = true;
        }
        else if (!SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            return;
        }

        if (isServer)
        {
            RpcSyncPosRot(transform.localPosition, transform.localRotation);
        }
        else
        {
            CmdSyncPosRot(transform.localPosition, transform.localRotation);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, .25f, ground);

        if (isGrounded && vel.y < 0)
        {
            vel.y = -2f;
            anim.SetBool("Jump", false);
        }

        if(!isGrounded)
            vel.y += gravity * Time.deltaTime;

        controller.Move(vel * Time.deltaTime);

        if (inventoryObject != null && hasObjectInHand)
        {
            inventoryObject.transform.position = hand.transform.position;
            inventoryObject.GetComponent<BoxCollider>().enabled = false;

        }
    }
}

