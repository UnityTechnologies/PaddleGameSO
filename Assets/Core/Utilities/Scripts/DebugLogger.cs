using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
	/// Utility to help print messages with a UnityAction.
	/// </summary>
    public class DebugLogger : MonoBehaviour
    {

        public void LogMessage(string message)
        {
            Debug.Log(message);
        }

        public void LogMessage(float value)
        {
            Debug.Log("float value = " + value);
        }
    }
}
