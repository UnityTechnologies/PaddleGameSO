using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GameSystemsCookbook
{
    /// <summary>
	/// This component converts the Items.Count of the GameObject Runtime Set into a text mesh.
	/// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class RuntimeSetCounter : MonoBehaviour
    {
        [Tooltip("The Runtime Set that holds a List of GameObjects")]
        [SerializeField] private GameObjectRuntimeSetSO m_BlocksRuntimeSet;

        [Header("Text")]
        [Tooltip("TextMeshPro UI component")]
        [SerializeField] private TextMeshPro m_TextMesh;
        [SerializeField] private string m_MessagePrefix;
        [SerializeField] private string m_MessageSuffix;

        [Header("Listen to Event Channels")]
        [Tooltip("Notification that the Runtime Set has updated")]
        [SerializeField] private VoidEventChannelSO m_RuntimeSetUpdated;

        private void OnEnable()
        {
            m_RuntimeSetUpdated.OnEventRaised += UpdateCounter;
        }

        private void OnDisable()
        {
            m_RuntimeSetUpdated.OnEventRaised -= UpdateCounter;
        }

        private void Start()
        {
            Initialize();
        }

        private void Reset()
        {
            if (m_TextMesh == null)
                m_TextMesh = GetComponent<TextMeshPro>();
        }

        private void UpdateCounter()
        {
            int count = m_BlocksRuntimeSet.Items.Count;
            m_TextMesh.text = m_MessagePrefix + count.ToString() + m_MessageSuffix;
        }

        private void Initialize()
        {
            m_TextMesh.text = m_MessagePrefix + "0" + m_MessageSuffix;
        }
    }
}
