using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This component manages the end screen shown after gameplay has ended. This responds to events from
    /// the GameManager (m_GameEnded, m_GameStarted, m_WinnerShown).
    /// </summary>

    public class WinScreen : MonoBehaviour
    {
        [Tooltip("Player P1 or P2 wins text message")]
        [SerializeField] private TextMeshPro m_PlayerWinText;
        [Tooltip("Any other decorative elements (Game Over text) to toggle")]
        [SerializeField] private List<Renderer> m_RenderedElements = new List<Renderer>();

        [Header("Listen to Event Channels")]
        [Tooltip("Notification that a player has won")]
        [SerializeField] private StringEventChannelSO m_WinnerAnnounced;
        [Tooltip("Notification to disable win text and other decorative elements")]
        [SerializeField] private VoidEventChannelSO m_GameStarted;
        [Tooltip("Notification to enable win text and other decorative elements")]
        [SerializeField] private VoidEventChannelSO m_GameEnded;

        // MonoBehaviour event functions

        // Append the player win TextMeshPro MeshRenderer to the list of renderers to toggle on/off
        private void Awake()
        {
            NullRefChecker.Validate(this);

            Renderer winTextRenderer = m_PlayerWinText.GetComponent<Renderer>();

            if (!m_RenderedElements.Contains(winTextRenderer))
                m_RenderedElements.Add(winTextRenderer);

        }

        // Subscribe to event channels
        private void OnEnable()
        {
            m_WinnerAnnounced.OnEventRaised += OnPlayerWinMessage;
            m_GameStarted.OnEventRaised += OnGameStarted;
            m_GameEnded.OnEventRaised += OnGameEnded;
        }

        // Unsubscribe to event channels to prevent errors
        private void OnDisable()
        {
            m_WinnerAnnounced.OnEventRaised -= OnPlayerWinMessage;
            m_GameStarted.OnEventRaised -= OnGameStarted;
            m_GameEnded.OnEventRaised -= OnGameEnded;
        }

        // Event handling methods

        // Displays text to show the winner after receiving the OnPlayerWinMessage event
        private void OnPlayerWinMessage(string message)
        {
            m_PlayerWinText.text = message;
        }

        // Shows the list of rendered elements after receiving the m_GameEnded event
        private void OnGameEnded()
        {
            foreach (Renderer renderer in m_RenderedElements)
            {
                renderer.enabled = true;
            }
        }

        // Hides the rendered elements after receiving the m_GameStarted event
        private void OnGameStarted()
        {
            foreach (Renderer renderer in m_RenderedElements)
            {
                renderer.enabled = false;
            }
        }
    }
}
