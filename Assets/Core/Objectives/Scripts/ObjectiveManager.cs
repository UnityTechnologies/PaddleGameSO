using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// Use this to track gameplay goals or objectives. This notifies the m_AllObjectivesCompleted
    /// event channel when each Objective registers as completed. This listens for notifications
    /// that the game has started and that an Objective has been completed.
    /// </summary>
    public class ObjectiveManager : MonoBehaviour
    {

        [Tooltip("List of Objectives needed for win condition.")]
        [SerializeField] private List<ObjectiveSO> m_Objectives = new List<ObjectiveSO>();

        [Header("Broadcast on Event Channels")]
        [Tooltip("Signal that all objectives are complete.")]
        [SerializeField] private VoidEventChannelSO m_AllObjectivesCompleted;

        [Header("Listen to Event Channels")]
        [Tooltip("Gameplay has begun.")]
        [SerializeField] private VoidEventChannelSO m_GameStarted;
        [Tooltip("Signal to update every time a single objective completes.")]
        [SerializeField] private VoidEventChannelSO m_ObjectiveCompleted;

        // Subscribes to event channels for starting the game and for the completion of each Objective
        private void OnEnable()
        {
            if (m_GameStarted != null)
                m_GameStarted.OnEventRaised += OnGameStarted;

            if (m_ObjectiveCompleted != null)
                m_ObjectiveCompleted.OnEventRaised += OnCompleteObjective;
        }

        // Unsubscribes to prevent errors
        private void OnDisable()
        {
            if (m_GameStarted != null)
                m_GameStarted.OnEventRaised -= OnGameStarted;

            if (m_ObjectiveCompleted != null)
                m_ObjectiveCompleted.OnEventRaised -= OnCompleteObjective;
        }

        // Returns true if all objectives are complete
        public bool IsObjectiveListComplete()
        {
            foreach (ObjectiveSO objective in m_Objectives)
            {
                if (!objective.IsCompleted)
                {
                    return false;
                }
            }
            return true;
        }

        // Event-handling methods

        // Reset each Objective when the game begins
        private void OnGameStarted()
        {
            foreach (ObjectiveSO objective in m_Objectives)
            {
                objective.ResetObjective();
            }
        }

        // Check if all Objectives are complete every time one Objective finishes.
        // Broadcasts the m_AllObjectivesCompleted event to the GameManager, if so.
        private void OnCompleteObjective()
        {
            
            if (IsObjectiveListComplete())
            {
                if (m_AllObjectivesCompleted != null)
                    m_AllObjectivesCompleted.RaiseEvent();
            }
        }

    }
}