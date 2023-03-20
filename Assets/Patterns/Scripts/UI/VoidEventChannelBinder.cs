using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
    /// <summary>
	/// This class connects a UI Elements Button to an event channel that takes no parameters.
	/// </summary>
    public class VoidEventChannelBinder : MonoBehaviour
    {
        [Tooltip("The event channel to raise.")]
        [SerializeField] private VoidEventChannelSO m_EventChannel;
        [Tooltip("The UI Toolkit document.")]
        [SerializeField] private UIDocument m_Document;
        [Tooltip("The name of the Button to query for.")]
        [SerializeField, Optional] private string m_ButtonID;
        [Tooltip("Cooldown window between button clicks")]
        [SerializeField] private float m_Delay = 0.5f;
        
        private VisualElement m_Root;
        private Button m_Button;
        private float m_TimeToNextEvent;

        // Valid dependencies (m_Button or m_Document) and log an error if missing
        private void Awake()
        {
            NullRefChecker.Validate(this);
        }

        private void Start()
        {
            m_Root = m_Document.rootVisualElement;
            m_Button = m_Root.Q<Button>(m_ButtonID);

            m_Button.clicked += RaiseEvent;

            // Alternatively, use the RegisterCallback method
        }

        private void OnDisable()
        {
            m_Button.clicked -= RaiseEvent;
        }

        private void RaiseEvent()
        {
       
            if (Time.time < m_TimeToNextEvent)
                return;

            m_EventChannel.RaiseEvent();
            m_TimeToNextEvent = Time.time + m_Delay;
        }

    }
}
