using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

namespace GameSystemsCookbook.Demos.PaddleBall
{

    /// <summary>
    /// This manager/presenter class maintains score values and notifies the UI to update.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        [Tooltip("A collection of corresponding PlayerIDs and ScoreView components")]
        [SerializeField] private List<PlayerScore> m_PlayerScores = new List<PlayerScore>();

        [Header("Broadcast on Event Channels")]
        [Tooltip("Notifies other objects that scores have changed")]
        [SerializeField] private ScoreListEventChannelSO m_ScoreManagerUpdated;
        [Tooltip("Sends winning player/score to GameManager")]
        [SerializeField] private PlayerScoreEventChannelSO m_WinScoreReached;

        [Header("Listen to Event Channels")]
        [Tooltip("Signal to end gameplay")]
        [SerializeField] private VoidEventChannelSO m_GameEnded;
        [Tooltip("Signal from GameManager to start/restart the game")]
        [SerializeField] private VoidEventChannelSO m_GameStarted;
        [Tooltip("Signal from GameManager to award points")]
        [SerializeField] private PlayerIDEventChannelSO m_PointScored;

        private bool m_IsGameOver;

        // Subscribes to events 
        private void OnEnable()
        {
            m_PointScored.OnEventRaised += OnPointScored;
            m_GameEnded.OnEventRaised += EndGame;
            m_GameStarted.OnEventRaised += InitializeScores;
        }

        // Unsubscribes from events to avoid errors
        private void OnDisable()
        {
            m_PointScored.OnEventRaised -= OnPointScored;
            m_GameEnded.OnEventRaised -= EndGame;
            m_GameStarted.OnEventRaised -= InitializeScores;
        }

        // Set up dependencies and verify fields in Inspector
        private void Start()
        {
            InitializeScores();
            NullRefChecker.Validate(this);
            ValidatePlayerScores();
        }

        // Sets up new score objects for the players and stores them.
        private void InitializeScores()
        {
            m_IsGameOver = false;

            for (int i = 0; i < m_PlayerScores.Count; i++)
            {
                PlayerScore playerScore = m_PlayerScores[i];
                playerScore.score = new Score();
                playerScore.scoreUI.UpdateText(playerScore.score.Value.ToString());
                m_PlayerScores[i] = playerScore;
            }
        }

        // Additional checks if the m_PlayerScores is setup in the Inspector
        private void ValidatePlayerScores()
        {
            if (m_PlayerScores.Count == 0)
            {
                Debug.Log("ScoreManager " + gameObject.name + " needs PlayerScores assigned", transform);
                return;
            }

            foreach (PlayerScore playerScore in m_PlayerScores)
            {
                if (playerScore.scoreUI == null)
                {
                    Debug.LogWarning("ScoreManager PlayerScore " + playerScore.playerID + " needs a ScoreView assigned", transform);
                    return;
                }
            }
        }

        // Increments the score for one player and notifies the UI
        private void OnPointScored(PlayerIDSO playerToIncrement)
        {
            // Don't allow scoring after the game is over
            if (m_IsGameOver)
                return;

            // Finds the correct player score and increments its score value
            PlayerScore playerScore = m_PlayerScores.Find(x => x.playerID == playerToIncrement);
            playerScore.score.IncrementScore();

            // Notify the UI and other listening objects
            string newScoreText = playerScore.score.Value.ToString();
            playerScore.scoreUI.UpdateText(newScoreText);

            m_ScoreManagerUpdated.RaiseEvent(m_PlayerScores);
        }

        // Sets a flag that the game is over.
        private void EndGame()
        {
            m_IsGameOver = true;
        }
    }
}