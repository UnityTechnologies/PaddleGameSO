using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// General game settings (ball and paddle speed, etc.). Adjust these to customize the gameplay.
    /// </summary>
    [CreateAssetMenu(menuName = "PaddleBall/GameData", fileName = "PaddleBallGameData")]
    public class GameDataSO : DescriptionSO
    {
        [Header("Paddle Data")]
        [Tooltip("Speed for both paddles")]
        [SerializeField] private float m_PaddleSpeed = 80f;
        [Tooltip("Physics drag for both paddles")]
        [SerializeField] private float m_PaddleDrag = 10f;
        [Tooltip("Physics mass for both paddles")]
        [SerializeField] private float m_PaddleMass = 0.5f;

        [Header("Ball Data")]
        [Tooltip("How fast the ball moves")]
        [SerializeField] private float m_BallSpeed = 200f;
        [Tooltip("Bouncing does not add extra force if at this speed limit")]
        [SerializeField] private float m_BallMaxSpeed = 300f;
        [Tooltip("How much the ball speeds up after each hit")] [Range(0.1f,2f)]
        [SerializeField] private float m_BounceMultiplier = 1.1f;

        [Header("Match Data")]
        [SerializeField] private float m_Delay = 1;

        [Header("Player IDs")]
        [SerializeField] private PlayerIDSO m_Player1;
        [SerializeField] private PlayerIDSO m_Player2;

        [Header("Player Sprites (Optional)")]
        [Tooltip("Optional sprite for Player 1")]
        [SerializeField, Optional] private Sprite m_P1Sprite;
        [Tooltip("Optional sprite for Player 2")]
        [SerializeField, Optional] private Sprite m_P2Sprite;

        [Header("Level Layout")]
        [Tooltip("ScriptableObject defining start positions for players and game pieces")]
        [SerializeField] private LevelLayoutSO m_LevelLayout;

        // Properties
        public float PaddleSpeed => m_PaddleSpeed;
        public float PaddleLinearDrag => m_PaddleDrag;
        public float PaddleMass => m_PaddleMass;
        public float BallSpeed => m_BallSpeed;
        public float MaxSpeed => m_BallMaxSpeed;
        public float BounceMultiplier => m_BounceMultiplier;

        public float DelayBetweenPoints => m_Delay;
        public PlayerIDSO Player1 => m_Player1;
        public PlayerIDSO Player2 => m_Player2;
        public LevelLayoutSO LevelLayout => m_LevelLayout;
        public Sprite P1Sprite => m_P1Sprite;
        public Sprite P2Sprite => m_P2Sprite;


        public bool IsPlayer1(PlayerIDSO id)
        {
            return m_Player1 == id;
        }

        public bool IsPlayer2(PlayerIDSO id)
        {
            return m_Player2 == id;
        }

        private void OnEnable()
        {
            // Check to see if all required fields in the Inspector exist
            NullRefChecker.Validate(this);
        }

    }
}