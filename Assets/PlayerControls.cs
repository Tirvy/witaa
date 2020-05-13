// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GameplaySoldier"",
            ""id"": ""246259b9-82a1-4b7e-ad41-d3cb438805a7"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2d923dde-8e30-4b3f-a3de-7596960f9356"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""42b20280-ae2f-47c1-8e7d-6e2b603fe401"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""Button"",
                    ""id"": ""d161adc7-e10a-489a-9643-23bfc2a5adf2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""46748391-13f7-413c-be55-f6221b8b50e4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""6b6701fa-8468-4c50-9266-1a42a563ffa7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""c0dd08af-fb9d-4cd1-aab3-e2fb5b1c6dc6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""03883c20-229f-49b8-a206-7ddf35f63452"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6b48fca9-ac1c-4440-b3de-dc459663fd8c"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0594fbc0-ff27-4c54-9ace-738edaaa6def"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GameplaySoldier
        m_GameplaySoldier = asset.FindActionMap("GameplaySoldier", throwIfNotFound: true);
        m_GameplaySoldier_Jump = m_GameplaySoldier.FindAction("Jump", throwIfNotFound: true);
        m_GameplaySoldier_Move = m_GameplaySoldier.FindAction("Move", throwIfNotFound: true);
        m_GameplaySoldier_Fire1 = m_GameplaySoldier.FindAction("Fire1", throwIfNotFound: true);
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

    // GameplaySoldier
    private readonly InputActionMap m_GameplaySoldier;
    private IGameplaySoldierActions m_GameplaySoldierActionsCallbackInterface;
    private readonly InputAction m_GameplaySoldier_Jump;
    private readonly InputAction m_GameplaySoldier_Move;
    private readonly InputAction m_GameplaySoldier_Fire1;
    public struct GameplaySoldierActions
    {
        private @PlayerControls m_Wrapper;
        public GameplaySoldierActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_GameplaySoldier_Jump;
        public InputAction @Move => m_Wrapper.m_GameplaySoldier_Move;
        public InputAction @Fire1 => m_Wrapper.m_GameplaySoldier_Fire1;
        public InputActionMap Get() { return m_Wrapper.m_GameplaySoldier; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplaySoldierActions set) { return set.Get(); }
        public void SetCallbacks(IGameplaySoldierActions instance)
        {
            if (m_Wrapper.m_GameplaySoldierActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnMove;
                @Fire1.started -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_GameplaySoldierActionsCallbackInterface.OnFire1;
            }
            m_Wrapper.m_GameplaySoldierActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fire1.started += instance.OnFire1;
                @Fire1.performed += instance.OnFire1;
                @Fire1.canceled += instance.OnFire1;
            }
        }
    }
    public GameplaySoldierActions @GameplaySoldier => new GameplaySoldierActions(this);
    public interface IGameplaySoldierActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnFire1(InputAction.CallbackContext context);
    }
}
