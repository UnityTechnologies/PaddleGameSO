//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.0
//     from Assets/PaddleBall/Input/PaddleBallControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    public partial class @PaddleBallControls: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PaddleBallControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PaddleBallControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""c766b44f-dae2-4308-a2f0-d8a624c0d297"",
            ""actions"": [
                {
                    ""name"": ""MoveP1"",
                    ""type"": ""Value"",
                    ""id"": ""7604435b-8f8d-4c39-b175-51939511c710"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveP2"",
                    ""type"": ""Button"",
                    ""id"": ""29aa5953-9ad2-44ce-a1c4-90a593651bf5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RestartGame"",
                    ""type"": ""Button"",
                    ""id"": ""5aa0bf33-b1ab-4b22-bd5a-7dc70c5aded4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""0ce829f1-34bf-4a3b-9581-4cde64bb3b0c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveP1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5571c995-306c-4d2c-bc6a-035c3cea1c4f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7cf182a4-a7af-4853-954b-c0d0fd68f547"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""04d64739-80c0-438f-b54e-ffa28a76dfe8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RestartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""9fece933-c468-4bf5-a739-2d910b9fcb73"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveP2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""95bf8964-cf52-47f6-94d3-a6692e1d9b96"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a88751c4-a0ab-4ac9-afbc-22c4cc24b564"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Menus"",
            ""id"": ""731fc819-2cab-4764-b055-a91dde47db9e"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""47db57ae-2a2c-4d9f-90aa-5bfea632770f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ee268fda-430d-41ed-8e2f-a6c8da80fc10"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_MoveP1 = m_Gameplay.FindAction("MoveP1", throwIfNotFound: true);
            m_Gameplay_MoveP2 = m_Gameplay.FindAction("MoveP2", throwIfNotFound: true);
            m_Gameplay_RestartGame = m_Gameplay.FindAction("RestartGame", throwIfNotFound: true);
            // Menus
            m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
            m_Menus_Newaction = m_Menus.FindAction("New action", throwIfNotFound: true);
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

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
        private readonly InputAction m_Gameplay_MoveP1;
        private readonly InputAction m_Gameplay_MoveP2;
        private readonly InputAction m_Gameplay_RestartGame;
        public struct GameplayActions
        {
            private @PaddleBallControls m_Wrapper;
            public GameplayActions(@PaddleBallControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveP1 => m_Wrapper.m_Gameplay_MoveP1;
            public InputAction @MoveP2 => m_Wrapper.m_Gameplay_MoveP2;
            public InputAction @RestartGame => m_Wrapper.m_Gameplay_RestartGame;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void AddCallbacks(IGameplayActions instance)
            {
                if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
                @MoveP1.started += instance.OnMoveP1;
                @MoveP1.performed += instance.OnMoveP1;
                @MoveP1.canceled += instance.OnMoveP1;
                @MoveP2.started += instance.OnMoveP2;
                @MoveP2.performed += instance.OnMoveP2;
                @MoveP2.canceled += instance.OnMoveP2;
                @RestartGame.started += instance.OnRestartGame;
                @RestartGame.performed += instance.OnRestartGame;
                @RestartGame.canceled += instance.OnRestartGame;
            }

            private void UnregisterCallbacks(IGameplayActions instance)
            {
                @MoveP1.started -= instance.OnMoveP1;
                @MoveP1.performed -= instance.OnMoveP1;
                @MoveP1.canceled -= instance.OnMoveP1;
                @MoveP2.started -= instance.OnMoveP2;
                @MoveP2.performed -= instance.OnMoveP2;
                @MoveP2.canceled -= instance.OnMoveP2;
                @RestartGame.started -= instance.OnRestartGame;
                @RestartGame.performed -= instance.OnRestartGame;
                @RestartGame.canceled -= instance.OnRestartGame;
            }

            public void RemoveCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IGameplayActions instance)
            {
                foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);

        // Menus
        private readonly InputActionMap m_Menus;
        private List<IMenusActions> m_MenusActionsCallbackInterfaces = new List<IMenusActions>();
        private readonly InputAction m_Menus_Newaction;
        public struct MenusActions
        {
            private @PaddleBallControls m_Wrapper;
            public MenusActions(@PaddleBallControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Newaction => m_Wrapper.m_Menus_Newaction;
            public InputActionMap Get() { return m_Wrapper.m_Menus; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
            public void AddCallbacks(IMenusActions instance)
            {
                if (instance == null || m_Wrapper.m_MenusActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_MenusActionsCallbackInterfaces.Add(instance);
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }

            private void UnregisterCallbacks(IMenusActions instance)
            {
                @Newaction.started -= instance.OnNewaction;
                @Newaction.performed -= instance.OnNewaction;
                @Newaction.canceled -= instance.OnNewaction;
            }

            public void RemoveCallbacks(IMenusActions instance)
            {
                if (m_Wrapper.m_MenusActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IMenusActions instance)
            {
                foreach (var item in m_Wrapper.m_MenusActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_MenusActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public MenusActions @Menus => new MenusActions(this);
        public interface IGameplayActions
        {
            void OnMoveP1(InputAction.CallbackContext context);
            void OnMoveP2(InputAction.CallbackContext context);
            void OnRestartGame(InputAction.CallbackContext context);
        }
        public interface IMenusActions
        {
            void OnNewaction(InputAction.CallbackContext context);
        }
    }
}