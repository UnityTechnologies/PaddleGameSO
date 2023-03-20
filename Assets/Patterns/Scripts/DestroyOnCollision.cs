using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystemsCookbook.Demos.PaddleBall;

namespace GameSystemsCookbook
{
    // Destroy the object 
    public class DestroyOnCollision : MonoBehaviour
    {
        [Tooltip("Only destroy when colliding with objects from this PlayerID")]
        [SerializeField] private bool m_UseTeamID;

        [Header("Broadcast to Event Channels")]
        [SerializeField] private GameObjectEventChannelSO m_DestroyGameObject;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Ball>() == null)
                return;

            if (m_UseTeamID)
            {
                PlayerIDSO id = TeamID.GetTeam(this.gameObject);

                if (!TeamID.AreEqual(collision.collider.gameObject, this.gameObject))
                { 
                    return;
                }

                if (TeamID.AreBothNull(collision.collider.gameObject, this.gameObject))
                {
                    return;
                }
            }

            m_DestroyGameObject.RaiseEvent(gameObject);
            Destroy(gameObject);

        }
    }
}
