using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    ///  This controller manages each player's on-screen "paddle" using physics.
    ///  A Vector2 stores the player's up/down input and then converts those
    ///  values into forces applied to a Rigidbody2D.
    /// </summary>
    ///
    [RequireComponent(typeof(Rigidbody2D))]
    public class Paddle : MonoBehaviour
    {
        [Header("Movement Limits")]
        [SerializeField] private float m_MinY = -4;
        [SerializeField] private float m_MaxY = 4;
        // Inspector fields
        [Header("Listen to Event Channels")]
        [Tooltip("Signal to reset paddles to starting position")]
        [SerializeField] private VoidEventChannelSO m_PaddleReset;
        [Tooltip("Required physics component")]
        [SerializeField] private Rigidbody2D m_rigidbody;
        [Tooltip("Event relayer for InputSystem Actions")]
        [SerializeField] private InputReaderSO m_inputReader;

        // Private fields
        private Vector2 m_inputVector;
        private PlayerIDSO m_playerId;
        private GameDataSO m_gameData;


        // Cache the physics Rigidbody
        private void Reset()
        {
            if (m_rigidbody == null)
                m_rigidbody = GetComponent<Rigidbody2D>();
        }

        // Subscribes to events for resetting the paddle
        // Note that subscription to the InputReaderSO happens in Initialize
        private void OnEnable()
        {
            m_PaddleReset.OnEventRaised += ResetPosition;
        }

        // Unsubscribe from events/event channels to prevent errors
        private void OnDisable()
        {
            m_inputReader.P1Moved -= OnP1Moved;
            m_inputReader.P2Moved -= OnP2Moved;

            m_PaddleReset.OnEventRaised -= ResetPosition;
        }

        // Applies physics forces
        private void FixedUpdate()
        {
            CalculateMovement(m_inputVector);
        }

        // Stores dependencies for later use and subscribes to the InputReader's events for paddle movement.
        public void Initialize(GameDataSO gameData, PlayerIDSO playerID, InputReaderSO inputReader)
        {
            m_gameData = gameData;
            m_playerId = playerID;
            m_inputReader = inputReader;

            m_inputReader.P1Moved += OnP1Moved;
            m_inputReader.P2Moved += OnP2Moved;

            InitializeRigidbody();

            // Verify that all necessary fields exist
            NullRefChecker.Validate(this);

        }

        // Move the paddle to its starting position
        private void ResetPosition()
        {
            transform.position = m_gameData.IsPlayer1(m_playerId) ?
                m_gameData.LevelLayout.Paddle1StartPosition : m_gameData.LevelLayout.Paddle2StartPosition;
        }

        // Reset the paddle's physics settings
        private void InitializeRigidbody()
        {
            m_rigidbody.drag = m_gameData.PaddleLinearDrag;
            m_rigidbody.mass = m_gameData.PaddleMass;
            m_rigidbody.gravityScale = 0f;
        }

        // Listen for input from the Input System and store in a Vector2 (Player1)
        private void OnP1Moved(float inputValue)
        {
            if (m_gameData.IsPlayer1(m_playerId))
            {
                m_inputVector = new Vector2(0f, inputValue);
            }
        }

        // Listen for input from the Input System and store in a Vector2 (Player2)
        private void OnP2Moved(float inputValue)
        {
            if (m_gameData.IsPlayer2(m_playerId))
            {
                m_inputVector = new Vector2(0f, inputValue);
            }
        }

        // Moves the paddle based on input and paddle speed values
        private void CalculateMovement(Vector2 m_inputVector)
        {

            if (m_inputVector.y > 0 && transform.position.y <= m_MaxY)
                m_rigidbody.AddForce(m_inputVector * m_gameData.PaddleSpeed);

            if (m_inputVector.y < 0 && transform.position.y >= m_MinY)
                m_rigidbody.AddForce(m_inputVector * m_gameData.PaddleSpeed);


        }
    }
}