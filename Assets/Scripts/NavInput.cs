// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/NavInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NavInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NavInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""NavInput"",
    ""maps"": [
        {
            ""name"": ""Main Menu"",
            ""id"": ""08935947-b153-4e64-bdf0-bc28117a0585"",
            ""actions"": [
                {
                    ""name"": ""ChangeNavIndex"",
                    ""type"": ""Button"",
                    ""id"": ""eed9e2ff-6b93-4cec-b2f1-13029ee3640a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeNavPanelIndex"",
                    ""type"": ""Button"",
                    ""id"": ""52676e8a-083c-47b9-87bd-65a79dbea5e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerNavPanel"",
                    ""type"": ""Button"",
                    ""id"": ""2f4ce603-8bf5-4f53-89d5-acb791ffb10f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Buttons"",
                    ""id"": ""e3f51174-862b-40c2-abf0-372f08f1944b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeNavIndex"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9693989c-9ffa-428b-8fbe-5e0070ba430b"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeNavIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2d79706b-3122-4bde-99c9-2bd73efa74ca"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeNavIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""f2b28b4a-e285-4d9c-bbf3-e2048f1c8d63"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9ac22f32-61ff-4a1a-be35-369806b6a64c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4c6b8edd-aa25-4ac5-ab93-d22619d406f7"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""1b989462-1260-4d23-96ce-5f24c49056d5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5b41284b-19ce-4fb5-aa40-69da7f8b87b6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1e677a66-a2ba-4ff3-b13b-69c0b2eb0f45"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Buttons"",
                    ""id"": ""4ab0af51-3671-48a3-aae3-6a2ae4f59c5a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b6773f96-5b6f-4d85-8208-d3904571db8b"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0b8a0752-38b4-42dc-89f3-b3c34e2b323a"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeNavPanelIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1edcb053-ad0c-49d0-8bdf-57daee2503ef"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TriggerNavPanel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CharacterSelection"",
            ""id"": ""cbcd592d-b2f7-4c3a-b667-5be1cbeeb924"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""96465664-edc3-44c2-a887-4d1e2b7ad0fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Refresh"",
                    ""type"": ""Button"",
                    ""id"": ""d0d8b599-d087-4da8-bbb1-6a89a639bd85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90f3e652-5a38-4b08-b332-4aad0969fb57"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83fb3289-bd52-4820-a1bc-6d7f664a004c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f96383ee-4e4e-43f5-af77-587c052c8a3a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Refresh"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""858f9acc-642f-44b4-8f6f-e13fa76d13be"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Refresh"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UISelection"",
            ""id"": ""b8960e09-9807-4efe-a641-d50e4b85e714"",
            ""actions"": [
                {
                    ""name"": ""ChangeCurrentSelection"",
                    ""type"": ""Button"",
                    ""id"": ""e1812594-be53-44f9-a179-d9ebecd2ef20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""dc63c246-c133-460b-968e-56d93592aa53"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a83e6ef0-72c7-406b-bfb6-ee643a06d9c2"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5f7d34bb-183c-407f-b41a-7819fb33cb73"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6417f081-81dc-4dd9-ba5f-08c25587720e"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f201fcba-26ac-4409-9879-7c5c5aaa4832"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""0b5e1b16-583a-4aaf-9977-09654b2a1f83"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""62c98821-a20c-4264-9958-4b19b20ad7e3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1d65be35-57d9-4b6b-80e1-07f54e6d1f89"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""85555644-1b50-43dd-9c56-6fa5f79dbdcb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0e2e7f0d-f03d-459c-aef2-7676b213aa0b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ChangeCurrentSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Main Menu
        m_MainMenu = asset.FindActionMap("Main Menu", throwIfNotFound: true);
        m_MainMenu_ChangeNavIndex = m_MainMenu.FindAction("ChangeNavIndex", throwIfNotFound: true);
        m_MainMenu_ChangeNavPanelIndex = m_MainMenu.FindAction("ChangeNavPanelIndex", throwIfNotFound: true);
        m_MainMenu_TriggerNavPanel = m_MainMenu.FindAction("TriggerNavPanel", throwIfNotFound: true);
        // CharacterSelection
        m_CharacterSelection = asset.FindActionMap("CharacterSelection", throwIfNotFound: true);
        m_CharacterSelection_Back = m_CharacterSelection.FindAction("Back", throwIfNotFound: true);
        m_CharacterSelection_Refresh = m_CharacterSelection.FindAction("Refresh", throwIfNotFound: true);
        // UISelection
        m_UISelection = asset.FindActionMap("UISelection", throwIfNotFound: true);
        m_UISelection_ChangeCurrentSelection = m_UISelection.FindAction("ChangeCurrentSelection", throwIfNotFound: true);
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

    // Main Menu
    private readonly InputActionMap m_MainMenu;
    private IMainMenuActions m_MainMenuActionsCallbackInterface;
    private readonly InputAction m_MainMenu_ChangeNavIndex;
    private readonly InputAction m_MainMenu_ChangeNavPanelIndex;
    private readonly InputAction m_MainMenu_TriggerNavPanel;
    public struct MainMenuActions
    {
        private @NavInput m_Wrapper;
        public MainMenuActions(@NavInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangeNavIndex => m_Wrapper.m_MainMenu_ChangeNavIndex;
        public InputAction @ChangeNavPanelIndex => m_Wrapper.m_MainMenu_ChangeNavPanelIndex;
        public InputAction @TriggerNavPanel => m_Wrapper.m_MainMenu_TriggerNavPanel;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterface != null)
            {
                @ChangeNavIndex.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnChangeNavIndex;
                @ChangeNavIndex.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnChangeNavIndex;
                @ChangeNavIndex.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnChangeNavIndex;
                @ChangeNavPanelIndex.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnChangeNavPanelIndex;
                @ChangeNavPanelIndex.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnChangeNavPanelIndex;
                @ChangeNavPanelIndex.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnChangeNavPanelIndex;
                @TriggerNavPanel.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnTriggerNavPanel;
                @TriggerNavPanel.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnTriggerNavPanel;
                @TriggerNavPanel.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnTriggerNavPanel;
            }
            m_Wrapper.m_MainMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChangeNavIndex.started += instance.OnChangeNavIndex;
                @ChangeNavIndex.performed += instance.OnChangeNavIndex;
                @ChangeNavIndex.canceled += instance.OnChangeNavIndex;
                @ChangeNavPanelIndex.started += instance.OnChangeNavPanelIndex;
                @ChangeNavPanelIndex.performed += instance.OnChangeNavPanelIndex;
                @ChangeNavPanelIndex.canceled += instance.OnChangeNavPanelIndex;
                @TriggerNavPanel.started += instance.OnTriggerNavPanel;
                @TriggerNavPanel.performed += instance.OnTriggerNavPanel;
                @TriggerNavPanel.canceled += instance.OnTriggerNavPanel;
            }
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);

    // CharacterSelection
    private readonly InputActionMap m_CharacterSelection;
    private ICharacterSelectionActions m_CharacterSelectionActionsCallbackInterface;
    private readonly InputAction m_CharacterSelection_Back;
    private readonly InputAction m_CharacterSelection_Refresh;
    public struct CharacterSelectionActions
    {
        private @NavInput m_Wrapper;
        public CharacterSelectionActions(@NavInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Back => m_Wrapper.m_CharacterSelection_Back;
        public InputAction @Refresh => m_Wrapper.m_CharacterSelection_Refresh;
        public InputActionMap Get() { return m_Wrapper.m_CharacterSelection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterSelectionActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterSelectionActions instance)
        {
            if (m_Wrapper.m_CharacterSelectionActionsCallbackInterface != null)
            {
                @Back.started -= m_Wrapper.m_CharacterSelectionActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_CharacterSelectionActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_CharacterSelectionActionsCallbackInterface.OnBack;
                @Refresh.started -= m_Wrapper.m_CharacterSelectionActionsCallbackInterface.OnRefresh;
                @Refresh.performed -= m_Wrapper.m_CharacterSelectionActionsCallbackInterface.OnRefresh;
                @Refresh.canceled -= m_Wrapper.m_CharacterSelectionActionsCallbackInterface.OnRefresh;
            }
            m_Wrapper.m_CharacterSelectionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Refresh.started += instance.OnRefresh;
                @Refresh.performed += instance.OnRefresh;
                @Refresh.canceled += instance.OnRefresh;
            }
        }
    }
    public CharacterSelectionActions @CharacterSelection => new CharacterSelectionActions(this);

    // UISelection
    private readonly InputActionMap m_UISelection;
    private IUISelectionActions m_UISelectionActionsCallbackInterface;
    private readonly InputAction m_UISelection_ChangeCurrentSelection;
    public struct UISelectionActions
    {
        private @NavInput m_Wrapper;
        public UISelectionActions(@NavInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangeCurrentSelection => m_Wrapper.m_UISelection_ChangeCurrentSelection;
        public InputActionMap Get() { return m_Wrapper.m_UISelection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UISelectionActions set) { return set.Get(); }
        public void SetCallbacks(IUISelectionActions instance)
        {
            if (m_Wrapper.m_UISelectionActionsCallbackInterface != null)
            {
                @ChangeCurrentSelection.started -= m_Wrapper.m_UISelectionActionsCallbackInterface.OnChangeCurrentSelection;
                @ChangeCurrentSelection.performed -= m_Wrapper.m_UISelectionActionsCallbackInterface.OnChangeCurrentSelection;
                @ChangeCurrentSelection.canceled -= m_Wrapper.m_UISelectionActionsCallbackInterface.OnChangeCurrentSelection;
            }
            m_Wrapper.m_UISelectionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChangeCurrentSelection.started += instance.OnChangeCurrentSelection;
                @ChangeCurrentSelection.performed += instance.OnChangeCurrentSelection;
                @ChangeCurrentSelection.canceled += instance.OnChangeCurrentSelection;
            }
        }
    }
    public UISelectionActions @UISelection => new UISelectionActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IMainMenuActions
    {
        void OnChangeNavIndex(InputAction.CallbackContext context);
        void OnChangeNavPanelIndex(InputAction.CallbackContext context);
        void OnTriggerNavPanel(InputAction.CallbackContext context);
    }
    public interface ICharacterSelectionActions
    {
        void OnBack(InputAction.CallbackContext context);
        void OnRefresh(InputAction.CallbackContext context);
    }
    public interface IUISelectionActions
    {
        void OnChangeCurrentSelection(InputAction.CallbackContext context);
    }
}
