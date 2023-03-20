using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
    public class ButtonEventBinder : MonoBehaviour
    {


        [Tooltip("The UI Toolkit document.")]
        [SerializeField] private UIDocument m_Document;
        [Tooltip("The name of the Button to query for.")]
        [SerializeField, Optional] private string m_ButtonID;

        [SerializeField] private UnityEvent m_Event;

        [Tooltip("Cooldown window between button clicks")]
        [SerializeField] private float m_Delay = 0.5f;

        private VisualElement m_Root;
        private Button m_Button;
        private float m_TimeToNextEvent;

        private void Awake()
        {
            Initialize();
        }

        private void Reset()
        {
            if (m_Document == null)
                m_Document = GetComponent<UIDocument>();
        }

        private void RaiseEvent()
        {
            if (Time.time < m_TimeToNextEvent)
                return;

            m_Event.Invoke();
            m_TimeToNextEvent = Time.time + m_Delay;
        }

        private void Initialize()
        {
            NullRefChecker.Validate(this);

            m_Root = m_Document.rootVisualElement;
            m_Button = m_Root.Q<Button>(m_ButtonID);

            if (m_Button == null)
                Debug.LogError("UIButtonBinder: Invalid m_ButtonID", this);

            m_Button.clicked += RaiseEvent;
        }

        private void OnDisable()
        {
            m_Button.clicked -= RaiseEvent;
        }

    }
}
