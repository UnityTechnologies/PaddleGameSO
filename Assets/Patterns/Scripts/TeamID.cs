using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
	/// Class that uses a ScriptableObject as an enum-like comparison.
	/// </summary>
    public class TeamID : MonoBehaviour
    {
        [Tooltip("ScriptableObject for comparison")]
        [SerializeField] private PlayerIDSO m_ID;

        public PlayerIDSO ID { get => m_ID; set => m_ID = value; }

        public bool IsEqual(PlayerIDSO id)
        {
            return id == m_ID;
        }

        // Static method to check if two GameObjects have the same TeamID.
		//
		// Example usage:
        // bool areEqual = TeamID.AreEqual(obj1, obj2);

        public static PlayerIDSO GetTeam(GameObject gameObject)
        {
            TeamID team = gameObject.GetComponent<TeamID>();

            if (team == null)
                return null;

            return team.ID;
        }
        public static bool AreEqual(GameObject a, GameObject b)
        {
            TeamID teamA = a.GetComponent<TeamID>();
            TeamID teamB = b.GetComponent<TeamID>();

            if (teamA != null && teamB != null)
                return teamA.ID == teamB.ID;

            return false;
        }

        public static bool AreBothNull(GameObject a, GameObject b)
        {
            TeamID teamA = a.GetComponent<TeamID>();
            TeamID teamB = b.GetComponent<TeamID>();

            if (teamA == null && teamB == null)
                return true;

            return false;
        }
    }
}
