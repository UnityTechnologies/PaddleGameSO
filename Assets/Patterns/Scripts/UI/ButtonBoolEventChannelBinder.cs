using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
    public class ButtonBoolEventChannelBinder : MonoBehaviour
    {
        [Tooltip("Pass this value to the Bool Event Channel")]
        [SerializeField] private bool m_State;

        [Header("UI Elements")]
        [Tooltip("The UI Toolkit document.")]
        [SerializeField] private UIDocument m_Document;
        [Tooltip("The name of the Button to query for.")]
        [SerializeField, Optional] private string m_ButtonID;

        [Header("Broadcast on Event Channel")]
        [Tooltip("The event channel to raise.")]
        [SerializeField] private BoolEventChannelSO m_EventChannel;
        [Space]
        [Tooltip("Cooldown window between button clicks.")]
        [SerializeField] private float m_Delay = 0.5f;

        private VisualElement m_Root;
        private Button m_Button;
        private float m_TimeToNextEvent;

        // Valid dependencies (m_Button or m_Document) and log an error if missing
        private void Awake()
        {
            NullRefChecker.Validate(this);
            ValidateButton();
        }

        private void Start()
        {
            m_Root = m_Document.rootVisualElement;
            m_Button = m_Root.Q<Button>(m_ButtonID);

            m_Button.clicked += RaiseEvent;

            // Alternatively, use the RegisterCallback method
            // m_Button.RegisterCallback<ClickEvent>(evt => RaiseEvent());
        }

        private void OnDisable()
        {
            m_Button.clicked -= RaiseEvent;
        }

        private void RaiseEvent()
        {
            if (Time.time < m_TimeToNextEvent)
                return;

            m_EventChannel.RaiseEvent(m_State);
            m_TimeToNextEvent = Time.time + m_Delay;
        }

        private void ValidateButton()
        {
            if (string.IsNullOrEmpty(m_ButtonID))
            {
                Debug.LogError("Missing assignment for field: m_ButtonID in object: " + this.gameObject, transform);
            }
        }

    }
}
