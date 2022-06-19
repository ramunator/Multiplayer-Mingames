// GENERATED AUTOMATICALLY FROM 'Assets/Player/RotatePlayer.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @RotatePlayer : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @RotatePlayer()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RotatePlayer"",
    ""maps"": [
        {
            ""name"": ""Rotation"",
            ""id"": ""1cdf20c1-5f09-4695-ae9c-d8eede00d321"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""31e3360b-c70f-46ac-82cc-ffd214bc952c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b744345-62a3-46ab-bc77-10f2e2594ffd"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Rotation
        m_Rotation = asset.FindActionMap("Rotation", throwIfNotFound: true);
        m_Rotation_Rotate = m_Rotation.FindAction("Rotate", throwIfNotFound: true);
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

    // Rotation
    private readonly InputActionMap m_Rotation;
    private IRotationActions m_RotationActionsCallbackInterface;
    private readonly InputAction m_Rotation_Rotate;
    public struct RotationActions
    {
        private @RotatePlayer m_Wrapper;
        public RotationActions(@RotatePlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Rotation_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_Rotation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RotationActions set) { return set.Get(); }
        public void SetCallbacks(IRotationActions instance)
        {
            if (m_Wrapper.m_RotationActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_RotationActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_RotationActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_RotationActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_RotationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public RotationActions @Rotation => new RotationActions(this);
    public interface IRotationActions
    {
        void OnRotate(InputAction.CallbackContext context);
    }
}
