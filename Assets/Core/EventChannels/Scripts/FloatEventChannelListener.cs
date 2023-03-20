using UnityEngine;
using UnityEngine.Events;

namespace GameSystemsCookbook
{
    public class FloatEventChannelListener : MonoBehaviour
    {

        [Header("Listen to Event Channels")]
        [SerializeField] private FloatEventChannelSO m_EventChannel;
        [Space]
        [Tooltip("Responds to receiving signal from Event Channel")]
        [SerializeField] private UnityEvent<float> m_Response;

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

        public void OnEventRaised(float value)
        {
            if (m_Response != null)
                m_Response.Invoke(value);
        }

    }
}
