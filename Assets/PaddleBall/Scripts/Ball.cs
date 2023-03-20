using UnityEngine;
using System.Collections;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This controls the ball motion and applies 2D physics forces. The ball responds
    /// to gameplay start and end events, scoring points, and collisions with the walls/paddles.
    /// </summary>  
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Ball : MonoBehaviour
    {

        [SerializeField] private Rigidbody2D m_Rigidbody;
        [SerializeField] private SpriteRenderer m_SpriteRenderer;

        // Inspector fields
        [Header("Listen to Event Channels")]
        [Tooltip("Signal that gameplay has begun")]
        [SerializeField] private VoidEventChannelSO m_GameStarted;
        [Tooltip("Notification that a point is scored")]
        [SerializeField] private PlayerIDEventChannelSO m_PointScored;
        [Tooltip("Notification that this ball has hit a wall or paddle")]
        [SerializeField] private Vector2EventChannelSO m_BallCollided;
        [Tooltip("Signal that gameplay has ended")]
        [SerializeField] private VoidEventChannelSO m_GameOver;

        // Private fields
        private GameDataSO m_GameData;
        private bool m_IsGameOver;
        private Vector2 m_BounceVector;

        // MonoBehaviour event-functions

        // Caches the SpriteRenderer and Rigidbody2D
        private void Reset()
        {
            if (m_Rigidbody == null)
                m_Rigidbody = GetComponent<Rigidbody2D>();

            if (m_SpriteRenderer == null)
                m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Subscribes to event channels for game starting/ending, scoring points, and bounces
        private void OnEnable()
        {
            m_GameStarted.OnEventRaised += BeginGamePlay;
            m_PointScored.OnEventRaised += ResetServe;
            m_BallCollided.OnEventRaised += Bounce;
            m_GameOver.OnEventRaised += EndGame;
        }

        // Unsubscribes from event channels to avoid errors
        private void OnDisable()
        {
            m_GameStarted.OnEventRaised -= BeginGamePlay;
            m_PointScored.OnEventRaised -= ResetServe;
            m_BallCollided.OnEventRaised -= Bounce;
            m_GameOver.OnEventRaised -= EndGame;
        }

        // Applies physics forces
        private void FixedUpdate()
        {
            // Apply the bounce from a reflector collision, if necessary
            // applies the speed limit to stop accumulating bounce force
            if (m_BounceVector != Vector2.zero && m_Rigidbody.velocity.sqrMagnitude < m_GameData.MaxSpeed)
            {
                // Add the force this frame, then reset
                m_Rigidbody.AddForce(m_BounceVector.normalized * m_GameData.BounceMultiplier, ForceMode2D.Impulse);
                m_BounceVector = Vector2.zero;
            }
        }

        // Sets up and verify dependencies (called externally by the GameManager).
		// Hides the Ball sprite by default.
        public void Initialize(GameDataSO gameData)
        {
            m_GameData = gameData;
            NullRefChecker.Validate(this);

            m_SpriteRenderer.enabled = false;
        }

        // Disables the physics settings and sprite renderer, resets the ball position
        private void Hide()
        {
            m_Rigidbody.velocity = Vector2.zero;
            transform.position = m_GameData.LevelLayout.BallStartPosition;
            m_SpriteRenderer.enabled = false;
        }

        // Puts the ball in play if our dependencies are set up and the game is not over
        private void BeginGamePlay()
        {
            m_IsGameOver = false;
            StartCoroutine(ServeAfterDelay());
        }

        // Re-serve the ball after a point is scored
        private void ResetServe(PlayerIDSO player)
        {
            if (!m_IsGameOver)
                StartCoroutine(ServeAfterDelay());
        }

        // Waits for a short delay before showing the ball, then serves the ball if the game is not over
        IEnumerator ServeAfterDelay()
        {
            Hide();

            yield return new WaitForSeconds(m_GameData.DelayBetweenPoints);

            if (!m_IsGameOver)
                ServeBall();
        }

        // Shoots the ball in a random left or right direction (while avoiding perfectly horizontal angle).
        // Uses physics to apply forces in order to move the ball.
        private void ServeBall()
        {
            // Random left or right direction
            float xDirection = (Random.value > 0.5f ? -1f : 1f);

            // Vertical direction 
            float yDirection = Random.value > 0.5f ? Random.Range(-1f, -0.5f) : Random.Range(0.5f, 1f);
            Vector2 serveDirection = new Vector2(xDirection, yDirection);

            m_SpriteRenderer.enabled = true;
            m_Rigidbody.AddForce(m_GameData.BallSpeed * serveDirection);
        }


        // Stores the bounce direction from reflector collision
        private void Bounce(Vector2 bounceVector)
        {
            m_BounceVector = (m_IsGameOver) ? Vector2.zero : bounceVector;
        }

        // Hides the ball and sets the game over flag
        private void EndGame()
        {
            m_IsGameOver = true;
            Hide();
        }
    }
}
