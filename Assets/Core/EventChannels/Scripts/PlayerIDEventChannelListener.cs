using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This script creates a listener for a PlayerIDEventChannelSO event channel and 
    /// defines a UnityEvent, "m_Response," that will be invoked when a signal is received from the event channel. 
    /// We also subscribe and unsubscribe the OnEventRaised method to/from the event channel and invoke it 
    /// </summary>
    public class PlayerIDEventChannelListener : MonoBehaviour
    {
        [Header("Listen to Event Channels")]
        [SerializeField] private PlayerIDEventChannelSO m_EventChannel;
        [Space]
        [Tooltip("Responds to receiving signal from Event Channel")]
        [SerializeField] private UnityEvent m_Response;

        // Subscribes to the event channels' OnEventRaised method
        private void OnEnable()
        {
            if (m_EventChannel != null)
                m_EventChannel.OnEventRaised += OnEventRaised;
        }

        // Unsubscribes from the event channel's OnEventRaised method to prevent errors
        private void OnDisable()
        {
            if (m_EventChannel != null)
                m_EventChannel.OnEventRaised -= OnEventRaised;
        }

        // Invokes the m_Response UnityEvent if it is not null
        public void OnEventRaised(PlayerIDSO playerID)
        {
            if (m_Response != null)
                m_Response.Invoke();
        }
    }
}