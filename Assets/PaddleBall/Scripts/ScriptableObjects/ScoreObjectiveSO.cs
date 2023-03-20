using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This structure connects the Score data component with the UI component and PlayerID. This
    /// includes a PlayerIDSO that identifies a player and a reference to its corresponding ScoreView
    /// component (with a TextMeshPro field). A separate Score object holds the actual score value.
    /// </summary>
    [System.Serializable]
    public struct PlayerScore
    {
        public PlayerIDSO playerID;
        public ScoreView scoreUI;
        public Score score;
    }

    /// <summary>
    /// This objective raises an event, m_TargetScoreReached, when one of the players reaches a minimum
    /// score threshold, m_TargetScore. It also listens for updates from the ScoreManager.
    [CreateAssetMenu(menuName = "Objectives/Score Objective", fileName = "ScoreObjective")]
    public class ScoreObjectiveSO : ObjectiveSO
    {
        [Header("Score Goal")]
        [Tooltip("Score to win")]
        [SerializeField] private int m_TargetScore = 1;

        [Header("Broadcast on Event Channels")]
        [Tooltip("Notify listeners that a Player has reached winning score")]
        [SerializeField] private PlayerScoreEventChannelSO m_TargetScoreReached;

        [Header("Listen to Event Channels")]
        [Tooltip("Signal when ScoreManager updates")]
        [SerializeField] private ScoreListEventChannelSO m_ScoreManagerUpdated;

        // Properties
        public int TargetScore => m_TargetScore;

        // This subscribes to the ScoreManager's updates. 
        private void OnEnable()
        {
            m_ScoreManagerUpdated.OnEventRaised += UpdateScoreManager;

            // Check to see if all required fields in the Inspector exist
            NullRefChecker.Validate(this);
        }

        // This unsubscribes from the ScoreManager's updates to prevent errors.
        private void OnDisable()
        {
            m_ScoreManagerUpdated.OnEventRaised -= UpdateScoreManager;
        }

        // This checks a list of current PlayerScores when the ScoreManager updates.
        // If we have a winner, it notifies the ObjectiveManager (and any other listeners)
        private void UpdateScoreManager(List<PlayerScore> playerScores)
        {
            if (HasReachedTargetScore(playerScores))
            {
                CompleteObjective();
            }
        }

        // Has a player reached the target score? If so, send a PlayerScore
        // score struct (with PlayerID, score, and score UI)
        private bool HasReachedTargetScore(List<PlayerScore> playerScores)
        {
            foreach (PlayerScore playerScore in playerScores)
            {
                if (playerScore.score.Value >= m_TargetScore)
                {
                    m_TargetScoreReached.RaiseEvent(playerScore);
                    return true;
                }
            }
            return false;
        }
    }
}
