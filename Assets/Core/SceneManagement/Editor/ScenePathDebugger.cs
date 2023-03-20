using UnityEngine;
using UnityEditor;


namespace GameSystemsCookbook
{
    /// <summary>
    /// Simple class for logging Build Settings scene paths to the console.
    /// </summary>
    [InitializeOnLoad]
    public class ScenePathDebugger
    {
        // Toggle to true to enable behavior
        private static bool m_IsEnabled = false;

        // A static constructor runs with InitializeOnLoad attribute
        static ScenePathDebugger()
        {
            EditorBuildSettings.sceneListChanged += DebugScenePaths;
        }

        // Logs the Build Settings scenes to the console
        public static void DebugScenePaths()
        {
            if (!m_IsEnabled)
                return;

            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            for (int i = 0; i < scenes.Length; i++)
            {
                Debug.Log("index: " + i + "  path: " + scenes[i].path);
            }
        }
    }
}
