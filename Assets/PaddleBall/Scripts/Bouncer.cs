using UnityEngine;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// Any surface (wall or paddle) that reflects the ball. Upon collision with a Ball,
    /// it returns the normal direction of the 2D collision.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class Bouncer : MonoBehaviour
    {
        [Header("Broadcast on Event Channels")]
        [Tooltip("Signal when ball collides with this wall or paddle")]
        [SerializeField] private Vector2EventChannelSO m_BallCollided;
        [Tooltip("Optional event that sends location of sound playback")]
        [SerializeField, Optional] private Vector2EventChannelSO m_SoundPlayed;
        // Verify necessary fields in the Inspector
        private void Awake()
        {
            NullRefChecker.Validate(this);
        }

        // Checks if have made contact with a Ball. If so, reflect the normal direction
        // of the collision and then raise an event with the new direction
        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.collider.GetComponent<Ball>() != null)
            {
                // Reverse the normal direction and pass with the event
                Vector3 normalDirection = collision.GetContact(0).normal;
                m_BallCollided.RaiseEvent(-normalDirection);

                if (m_SoundPlayed != null)
                    m_SoundPlayed.RaiseEvent(collision.GetContact(0).point);
            }
        }
    }
}
