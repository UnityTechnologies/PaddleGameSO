using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This invokes a UnityEvent in response to receiving a specific event channel (Vector2EventChannelSO).
    /// This is a simple means of creating codeless interactivity (e.g. bounce sounds in the PaddleBall demo).
    /// </summary>
    public class Vector2EventChannelListener : MonoBehaviour
    {
        [FormerlySerializedAs("")]
        [Header("Listen to Event Channels")]
        [SerializeField] private Vector2EventChannelSO m_EventChannel;
        [Space]
        [Tooltip("Responds to receiving signal from Event Channel")]
        [SerializeField] private UnityEvent<Vector2> m_Response;

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

        public void OnEventRaised(Vector2 vector2)
        {
            if (m_Response != null)
                m_Response.Invoke(vector2);
        }
    }
}