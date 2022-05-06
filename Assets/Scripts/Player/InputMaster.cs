// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""520d19da-c938-45a6-b4a8-3fcadd98a9c6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6ef6f595-a6ac-4be3-b13d-4dd8bef84c1c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pickup"",
                    ""type"": ""Button"",
                    ""id"": ""86caca78-c9d3-4537-a296-8a159a1e53de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""87c0e99b-8ae3-40ca-a04e-45c5dfd76936"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Serve"",
                    ""type"": ""Button"",
                    ""id"": ""8488afaa-18bb-4a34-9f41-8245ca92baea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f6c36a8b-a960-4fb0-b6d6-c7b243f33008"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""62a69375-d67a-4cc1-936d-f08b0e89841e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Movement"",
                    ""id"": ""1db48f0d-d438-4a56-a48c-2a7165a41f02"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a19b47d8-364a-4ff4-9b6a-1f0bbcb7988f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""604c9bdf-ca4b-4044-a9f5-9b54fa8caf85"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b8ab75ff-4e0b-415a-a023-060856fcf60b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7db28bcd-ae24-4f33-8587-9c94b87ad323"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""cc54d271-1852-4736-bf5b-730948a90036"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e585cd5a-a91c-4019-b45d-74de1fd14cbe"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""860d4f01-1193-47da-8313-53c2982723f9"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3e00bdd0-061c-4936-b584-c3ac88e2d550"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d087ae82-6155-4eb0-a2f8-140cd0b5e357"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5f04b2bb-fb40-46e3-90c6-aab525a38a57"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39efee54-3718-4d73-97d1-bec7eab8e29f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32f15b5f-2e87-48b2-b11d-8604b585c542"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""676e32a1-fe40-475c-8b09-7d37b39a5484"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e4f6f84-ba85-4924-a00f-7b29247bae54"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Serve"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0e94e9d-7284-4e96-bfc3-e2d6817ae385"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Serve"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e0bd161-31a8-4754-a165-fa8e2138f869"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abdcc774-1e75-456f-ab68-f7250cf4d389"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f06ba86-a196-485f-bf80-94a6be1d428e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1646fc0-1da9-4a94-9851-025267737e99"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Look"",
            ""id"": ""e6cd0d65-8bcb-47e9-897a-da71deab706f"",
            ""actions"": [
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8f8ce219-cba5-4b69-89fa-720ad869c1d0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GamepadLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d1007a8-dfb0-4551-9861-a10eff4395fa"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""096d625d-ed48-4e83-b386-6e5fc24301fe"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e70e49f0-4639-4fdc-8f2e-a647c96a65bd"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BuildMode"",
            ""id"": ""a7db2dcd-a981-4fcf-a4f8-3cee86e76927"",
            ""actions"": [
                {
                    ""name"": ""MoveCam"",
                    ""type"": ""Value"",
                    ""id"": ""eb7179f3-92d8-4937-bba0-53ccf417680b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaceBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""94aa7c75-3b4c-4115-ac6e-aae130a8c566"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomCam"",
                    ""type"": ""PassThrough"",
                    ""id"": ""915ee678-b0db-4c48-a647-cd8f901552dc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DestroyBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""62f1486b-6564-4f5e-9122-4f3adeea5410"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCam"",
                    ""type"": ""Value"",
                    ""id"": ""69b9296a-991d-47fe-b330-82fe2ec46efe"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""MoveCam"",
                    ""id"": ""83866bae-7b3a-448a-9084-5e378792b7d3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCam"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""83f7fb1c-a6db-4213-90d0-a7dc1bdf622e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""36f796c7-0465-4389-b4d0-7fc13cf84569"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2863d520-1e0b-4daa-859e-de76c10b2c6c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""93ae50ec-8f89-4f8a-9fed-578c7b9a9320"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""MoveCam"",
                    ""id"": ""d148fb70-6771-4339-9e6d-00c5093ba9d8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCam"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7dda1f4a-96f6-40bc-bf92-4d4c864c689c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""66d3ee4e-8a45-4176-99ea-5920a436f008"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""876e4694-b5e4-4162-89bc-a3e0b8615d97"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9da376d0-b8e2-489d-8c7a-73f3727fac12"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4e145677-28db-4ef1-8875-e8f9035f7d85"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PlaceBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d8a8985-d901-4f4a-ad3b-f595a4fe7ded"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PlaceBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b754a5d-2313-4004-9581-4d9b2c054016"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ZoomCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""692c3a49-9334-49a9-a46b-5a45cc3e2d82"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ZoomCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""259c7d56-b7b7-40a8-872b-83b59874c497"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DestroyBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9388434e-6442-4ade-9df7-ae83ae5ae237"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""DestroyBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1f9eecb-588d-4a20-b9af-ba009674e4bf"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""RotateCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13ac7aee-73e1-46ef-90dd-f846e29358f4"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard And Mouse"",
            ""bindingGroup"": ""Keyboard And Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Pickup = m_Player.FindAction("Pickup", throwIfNotFound: true);
        m_Player_Drop = m_Player.FindAction("Drop", throwIfNotFound: true);
        m_Player_Serve = m_Player.FindAction("Serve", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        // Look
        m_Look = asset.FindActionMap("Look", throwIfNotFound: true);
        m_Look_MouseLook = m_Look.FindAction("MouseLook", throwIfNotFound: true);
        m_Look_GamepadLook = m_Look.FindAction("GamepadLook", throwIfNotFound: true);
        // BuildMode
        m_BuildMode = asset.FindActionMap("BuildMode", throwIfNotFound: true);
        m_BuildMode_MoveCam = m_BuildMode.FindAction("MoveCam", throwIfNotFound: true);
        m_BuildMode_PlaceBuilding = m_BuildMode.FindAction("PlaceBuilding", throwIfNotFound: true);
        m_BuildMode_ZoomCam = m_BuildMode.FindAction("ZoomCam", throwIfNotFound: true);
        m_BuildMode_DestroyBuilding = m_BuildMode.FindAction("DestroyBuilding", throwIfNotFound: true);
        m_BuildMode_RotateCam = m_BuildMode.FindAction("RotateCam", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Pickup;
    private readonly InputAction m_Player_Drop;
    private readonly InputAction m_Player_Serve;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Shoot;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Pickup => m_Wrapper.m_Player_Pickup;
        public InputAction @Drop => m_Wrapper.m_Player_Drop;
        public InputAction @Serve => m_Wrapper.m_Player_Serve;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Pickup.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Drop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Serve.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnServe;
                @Serve.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnServe;
                @Serve.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnServe;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Pickup.started += instance.OnPickup;
                @Pickup.performed += instance.OnPickup;
                @Pickup.canceled += instance.OnPickup;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @Serve.started += instance.OnServe;
                @Serve.performed += instance.OnServe;
                @Serve.canceled += instance.OnServe;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Look
    private readonly InputActionMap m_Look;
    private ILookActions m_LookActionsCallbackInterface;
    private readonly InputAction m_Look_MouseLook;
    private readonly InputAction m_Look_GamepadLook;
    public struct LookActions
    {
        private @InputMaster m_Wrapper;
        public LookActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLook => m_Wrapper.m_Look_MouseLook;
        public InputAction @GamepadLook => m_Wrapper.m_Look_GamepadLook;
        public InputActionMap Get() { return m_Wrapper.m_Look; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LookActions set) { return set.Get(); }
        public void SetCallbacks(ILookActions instance)
        {
            if (m_Wrapper.m_LookActionsCallbackInterface != null)
            {
                @MouseLook.started -= m_Wrapper.m_LookActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_LookActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_LookActionsCallbackInterface.OnMouseLook;
                @GamepadLook.started -= m_Wrapper.m_LookActionsCallbackInterface.OnGamepadLook;
                @GamepadLook.performed -= m_Wrapper.m_LookActionsCallbackInterface.OnGamepadLook;
                @GamepadLook.canceled -= m_Wrapper.m_LookActionsCallbackInterface.OnGamepadLook;
            }
            m_Wrapper.m_LookActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @GamepadLook.started += instance.OnGamepadLook;
                @GamepadLook.performed += instance.OnGamepadLook;
                @GamepadLook.canceled += instance.OnGamepadLook;
            }
        }
    }
    public LookActions @Look => new LookActions(this);

    // BuildMode
    private readonly InputActionMap m_BuildMode;
    private IBuildModeActions m_BuildModeActionsCallbackInterface;
    private readonly InputAction m_BuildMode_MoveCam;
    private readonly InputAction m_BuildMode_PlaceBuilding;
    private readonly InputAction m_BuildMode_ZoomCam;
    private readonly InputAction m_BuildMode_DestroyBuilding;
    private readonly InputAction m_BuildMode_RotateCam;
    public struct BuildModeActions
    {
        private @InputMaster m_Wrapper;
        public BuildModeActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCam => m_Wrapper.m_BuildMode_MoveCam;
        public InputAction @PlaceBuilding => m_Wrapper.m_BuildMode_PlaceBuilding;
        public InputAction @ZoomCam => m_Wrapper.m_BuildMode_ZoomCam;
        public InputAction @DestroyBuilding => m_Wrapper.m_BuildMode_DestroyBuilding;
        public InputAction @RotateCam => m_Wrapper.m_BuildMode_RotateCam;
        public InputActionMap Get() { return m_Wrapper.m_BuildMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildModeActions set) { return set.Get(); }
        public void SetCallbacks(IBuildModeActions instance)
        {
            if (m_Wrapper.m_BuildModeActionsCallbackInterface != null)
            {
                @MoveCam.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMoveCam;
                @MoveCam.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMoveCam;
                @MoveCam.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMoveCam;
                @PlaceBuilding.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnPlaceBuilding;
                @PlaceBuilding.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnPlaceBuilding;
                @PlaceBuilding.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnPlaceBuilding;
                @ZoomCam.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnZoomCam;
                @ZoomCam.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnZoomCam;
                @ZoomCam.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnZoomCam;
                @DestroyBuilding.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnDestroyBuilding;
                @DestroyBuilding.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnDestroyBuilding;
                @DestroyBuilding.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnDestroyBuilding;
                @RotateCam.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnRotateCam;
                @RotateCam.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnRotateCam;
                @RotateCam.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnRotateCam;
            }
            m_Wrapper.m_BuildModeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCam.started += instance.OnMoveCam;
                @MoveCam.performed += instance.OnMoveCam;
                @MoveCam.canceled += instance.OnMoveCam;
                @PlaceBuilding.started += instance.OnPlaceBuilding;
                @PlaceBuilding.performed += instance.OnPlaceBuilding;
                @PlaceBuilding.canceled += instance.OnPlaceBuilding;
                @ZoomCam.started += instance.OnZoomCam;
                @ZoomCam.performed += instance.OnZoomCam;
                @ZoomCam.canceled += instance.OnZoomCam;
                @DestroyBuilding.started += instance.OnDestroyBuilding;
                @DestroyBuilding.performed += instance.OnDestroyBuilding;
                @DestroyBuilding.canceled += instance.OnDestroyBuilding;
                @RotateCam.started += instance.OnRotateCam;
                @RotateCam.performed += instance.OnRotateCam;
                @RotateCam.canceled += instance.OnRotateCam;
            }
        }
    }
    public BuildModeActions @BuildMode => new BuildModeActions(this);
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard And Mouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnServe(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
    public interface ILookActions
    {
        void OnMouseLook(InputAction.CallbackContext context);
        void OnGamepadLook(InputAction.CallbackContext context);
    }
    public interface IBuildModeActions
    {
        void OnMoveCam(InputAction.CallbackContext context);
        void OnPlaceBuilding(InputAction.CallbackContext context);
        void OnZoomCam(InputAction.CallbackContext context);
        void OnDestroyBuilding(InputAction.CallbackContext context);
        void OnRotateCam(InputAction.CallbackContext context);
    }
}
