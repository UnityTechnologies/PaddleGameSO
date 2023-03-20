using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This specialized UI screen can cover the  
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public class SplashScreen : MonoBehaviour
    {
        public const string k_ParentElement = "splash__background";
        public const string k_ProgressBar = "splash__progress-bar";

        [Tooltip("UI Toolkit Document ")]
        [SerializeField] private UIDocument m_UIDocument;

        [Header("Listen to Event Channels")]
        [Tooltip("Percentage of loading time that is complete")]
        [SerializeField] private FloatEventChannelSO m_LoadProgressUpdated;
        [Tooltip("Is the loading complete?")]
        [SerializeField] private VoidEventChannelSO m_PreloadComplete;

        private VisualElement m_ParentElement;
        private VisualElement m_Root;
        private ProgressBar m_ProgressBar;

        private void Awake()
        {
            NullRefChecker.Validate(this);

            m_Root = m_UIDocument.rootVisualElement;
            SetVisualElements();
        }

        private void Reset()
        {
            if (m_UIDocument == null)
                m_UIDocument = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            m_LoadProgressUpdated.OnEventRaised += UpdateProgressBar;
            m_PreloadComplete.OnEventRaised += Hide;
        }

        private void OnDisable()
        {
            m_LoadProgressUpdated.OnEventRaised -= UpdateProgressBar;
            m_PreloadComplete.OnEventRaised -= Hide;
        }

        private void SetVisualElements()
        {
            m_ParentElement = m_Root.Q<VisualElement>(k_ParentElement);
            m_ProgressBar = m_Root.Q<ProgressBar>(k_ProgressBar);
            m_ParentElement.style.display = DisplayStyle.Flex;
        }

        private void UpdateProgressBar(float value)
        {
            m_ProgressBar.value = value;
        }

        private void Hide()
        {
            m_ParentElement.style.display = DisplayStyle.None;
        }
    }
}
