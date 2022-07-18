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
}
