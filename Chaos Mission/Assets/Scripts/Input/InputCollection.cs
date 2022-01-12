// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/InputCollection.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ChaosMission.Input
{
    public class @InputCollection : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputCollection()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputCollection"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""cec8b14f-6e9b-4aea-8001-8faf46d13359"",
            ""actions"": [
                {
                    ""name"": ""Moving"",
                    ""type"": ""Button"",
                    ""id"": ""9c4b376f-2b00-4a2c-8c24-5ed2694609f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shooting"",
                    ""type"": ""Value"",
                    ""id"": ""d031ab5e-46b9-4e9b-ae74-5e2d1b354986"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jumping"",
                    ""type"": ""Button"",
                    ""id"": ""b0470950-05a3-4cb6-820e-c44802b77cf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""9644f0ec-12e0-49d1-aef4-ab2d86ef79e2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b6490bed-cf14-4234-a490-f2941b478bf7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4d8540d3-b9f6-45c5-b373-5a60b9192aaa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2e6d3588-340c-4091-b093-42c8f8a0fce4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Shooting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e32aa39-e423-4aa5-b2a8-30e2c30370fa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseKeyboard"",
            ""bindingGroup"": ""MouseKeyboard"",
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
        },
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
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Moving = m_Player.FindAction("Moving", throwIfNotFound: true);
            m_Player_Shooting = m_Player.FindAction("Shooting", throwIfNotFound: true);
            m_Player_Jumping = m_Player.FindAction("Jumping", throwIfNotFound: true);
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
        private readonly InputAction m_Player_Moving;
        private readonly InputAction m_Player_Shooting;
        private readonly InputAction m_Player_Jumping;
        public struct PlayerActions
        {
            private @InputCollection m_Wrapper;
            public PlayerActions(@InputCollection wrapper) { m_Wrapper = wrapper; }
            public InputAction @Moving => m_Wrapper.m_Player_Moving;
            public InputAction @Shooting => m_Wrapper.m_Player_Shooting;
            public InputAction @Jumping => m_Wrapper.m_Player_Jumping;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Moving.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoving;
                    @Moving.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoving;
                    @Moving.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoving;
                    @Shooting.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShooting;
                    @Shooting.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShooting;
                    @Shooting.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShooting;
                    @Jumping.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumping;
                    @Jumping.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumping;
                    @Jumping.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumping;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Moving.started += instance.OnMoving;
                    @Moving.performed += instance.OnMoving;
                    @Moving.canceled += instance.OnMoving;
                    @Shooting.started += instance.OnShooting;
                    @Shooting.performed += instance.OnShooting;
                    @Shooting.canceled += instance.OnShooting;
                    @Jumping.started += instance.OnJumping;
                    @Jumping.performed += instance.OnJumping;
                    @Jumping.canceled += instance.OnJumping;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        private int m_MouseKeyboardSchemeIndex = -1;
        public InputControlScheme MouseKeyboardScheme
        {
            get
            {
                if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("MouseKeyboard");
                return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
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
            void OnMoving(InputAction.CallbackContext context);
            void OnShooting(InputAction.CallbackContext context);
            void OnJumping(InputAction.CallbackContext context);
        }
    }
}