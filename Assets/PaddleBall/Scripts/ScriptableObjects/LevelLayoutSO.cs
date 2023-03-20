using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This structure stores Transform data in a human-readable format for modding.
    /// </summary>
    [System.Serializable]
    public struct TransformSaveData
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 localScale;
    }

    /// <summary>
    /// A data container defining the starting transforms for walls, paddles, goals, etc.
    /// Use the ExportToJson method to export a file that can be modded outside of Unity.
    /// </summary>
    [CreateAssetMenu(menuName = "PaddleBall/Level Layout", fileName = "Level Layout")]
    public class LevelLayoutSO : DescriptionSO
    {
        // The default JSON file name and enclosing folder
        private const string k_JsonFilename = "LevelLayout.json";
        private const string k_JsonSubfolder = "Json";

        // These fields contain the starting positions for paddles, goals, and walls
        [Header("Paddle and Ball Start Positions")]
        [Tooltip("Ball spawn position")]
        [SerializeField] private Vector3 m_BallStartPosition;
        [Tooltip("Paddle1 spawn position")]
        [SerializeField] private Vector3 m_Paddle1StartPosition;
        [Tooltip("Paddle2 spawn position")]
        [SerializeField] private Vector3 m_Paddle2StartPosition;
        

        [Header("Goals")]
        [Tooltip("Score goal for Player P1")]
        [SerializeField] private TransformSaveData m_Goal1;
        [Tooltip("Score goal for Player P2")]
        [SerializeField] private TransformSaveData m_Goal2;

        [Header("Walls")]
        [Tooltip("Wall placement from individual transform data")]
        [SerializeField] private TransformSaveData[] m_LevelWalls;
        [Space]
        [Tooltip("Wall placement from Prefabs (optional)")]
        [SerializeField] private GameObject m_LevelPrefab;

        // The JSON file name to export
        [Header("Export")]
        [Tooltip("Json file to write to Application persistent path")]
        [SerializeField] private string m_JsonFilename = "LevelLayout.json";

        // Properties
        public Vector3 BallStartPosition => m_BallStartPosition;
        public Vector3 Paddle1StartPosition => m_Paddle1StartPosition;
        public Vector3 Paddle2StartPosition => m_Paddle2StartPosition;
        public TransformSaveData Goal1 => m_Goal1;
        public TransformSaveData Goal2 => m_Goal2;
        public TransformSaveData[] LevelWalls => m_LevelWalls;
        public string JsonFilename => m_JsonFilename;
        public GameObject LevelPrefab => m_LevelPrefab;

        // Sets a default name if jsonFilename left blank in the Inspector
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(m_JsonFilename))
                m_JsonFilename = k_JsonFilename;
        }

        // This exports a Json file to the Application PersistentPath and returns the string
        public string ExportToJson()
        {
            string json = SaveManager.GetSavePath(k_JsonSubfolder) + k_JsonFilename;
            SaveManager.Save(this, k_JsonFilename, k_JsonSubfolder);
            return json;
        }

    }
}