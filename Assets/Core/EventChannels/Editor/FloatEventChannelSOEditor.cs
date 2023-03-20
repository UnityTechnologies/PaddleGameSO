using UnityEngine;
using UnityEditor;

namespace GameSystemsCookbook
{
    /// <summary>
    /// Editor script to add a custom Inspector to the FloatEventChannelSO. This uses a custom
    /// ListView to show all subscribed listeners.
    /// </summary>
    [CustomEditor(typeof(FloatEventChannelSO))]
    public class FloatEventChannelSOEditor : GenericEventChannelSOEditor<float>
    {

    }
}
