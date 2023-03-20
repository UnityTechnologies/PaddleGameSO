using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// General event channel that broadcasts and carries string payload.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/String Event Channel", fileName = "StringEventChannel")]
    public class StringEventChannelSO : GenericEventChannelSO<string>
    {
    }
}