using UnityEngine;
using UnityEditor;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This adds an extra Save JSON button to the LevelLayoutSO Inspector to help the user
    /// export a JSON file.
    /// </summary>

    [CustomEditor(typeof(LevelLayoutSO))]
    public class LevelLayoutSOEditor : Editor
    {

        // Draws the LevelLayoutSO Inspector and then adds an extra Button for saving a JSON file.
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelLayoutSO myLevelLayoutSO = (LevelLayoutSO)target;

            if (GUILayout.Button("Save JSON"))
            {
                string resultingString = myLevelLayoutSO.ExportToJson();
            }
        }
    }
}
