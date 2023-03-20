using UnityEngine;
using UnityEngine.Events;

namespace GameSystemsCookbook
{ 
    /// <summary>
    /// General Event Channel that broadcasts and carries Vector2 payload.
    /// </summary>
    /// 
    [CreateAssetMenu(menuName = "Events/Vector2 Event Channel", fileName = "Vector2EventChannel")]
    public class Vector2EventChannelSO : GenericEventChannelSO<Vector2>
    {
 
    }
}