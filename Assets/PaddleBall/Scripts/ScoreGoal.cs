using UnityEngine;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// Use this component on a Collider2D to represent the score zone for each player. It notifies the
    /// m_GoalHit event channel with a PlayerID ScriptableObject when a Ball component makes contact. This should
    /// score a point for that player.
    /// </summary>

    [RequireComponent(typeof(Collider2D))]
    public class ScoreGoal : MonoBehaviour
    {
        // Inspector fields
        [Tooltip("PlayerID to increment when scoring, assigned with GameSetup")]
        [SerializeField] private PlayerIDSO m_ScoreForPlayerID;

        [Header("Broadcast on Event Channels")]
        [Tooltip("Event Channel invoked on contact")]
        [SerializeField] private PlayerIDEventChannelSO m_GoalHit;

        // Properties
        public PlayerIDSO ScoreForPlayerID => m_ScoreForPlayerID;

        // Called in the GameSetup
        public void Initialize(PlayerIDSO playerID)
        {
            m_ScoreForPlayerID = playerID;

            // Check to see if all required fields in the Inspector exist
            NullRefChecker.Validate(this);
        }

        // Checks if we make contact with a valid Ball component. If so, notify any objects listening to m_GoalHit
        // with the m_ScoreForPlayerID that has scored
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Ball>() != null)
            {
                m_GoalHit.RaiseEvent(m_ScoreForPlayerID);
            }
        }
    }
}