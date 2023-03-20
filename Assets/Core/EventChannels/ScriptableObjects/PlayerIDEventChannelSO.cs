using UnityEngine;
using UnityEngine.Events;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This ScriptableObject-based event channel passes a PlayerID as a payload (e.g. used for designating winner,
    /// loser, teams, etc.)
    /// </summary>
    [CreateAssetMenu(menuName = "Events/PlayerID EventChannel", fileName = "PlayerIDEventChannel")]
    public class PlayerIDEventChannelSO : GenericEventChannelSO<PlayerIDSO>
    {
    }
}