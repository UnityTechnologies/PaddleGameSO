using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This runs a UnityEvent in response to receiving a specific event channel.
    /// Use it as a simple means of creating codeless interactivity.
    /// </summary>
    public class VoidEventChannelListener : MonoBehaviour
    {
        [Header("Listen to Event Channels")]
        [Tooltip("The signal to listen for")]
        [SerializeField, Optional] private VoidEventChannelSO m_EventChannel;
        [Space]
        [Tooltip("Responds to receiving signal from Event Channel")]
        [SerializeField] private UnityEvent m_Response;
        [SerializeField] private float m_Delay;

        private void OnEnable()
        {
            if (m_EventChannel != null)
                m_EventChannel.OnEventRaised += OnEventRaised;
        }

        private void OnDisable()
        {
            if (m_EventChannel != null)
                m_EventChannel.OnEventRaised -= OnEventRaised;
        }

        // Raises an event after a delay
        public void OnEventRaised()
        {
            StartCoroutine(RaiseEventDelayed(m_Delay));
        }

        private IEnumerator RaiseEventDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            m_Response.Invoke();
        }
    }
}