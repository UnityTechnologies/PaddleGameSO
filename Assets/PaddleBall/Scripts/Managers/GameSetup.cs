using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// Sets up the paddle, ball, wall, and goal instances for gameplay. This can load the level
    /// from either an external JSON file or a LevelLayout ScriptableObject.
    /// </summary>
    public class GameSetup : MonoBehaviour
    {
        // Parent instantiated walls/goals under a Transform of this name
        public const string k_RootTransform = "Level";

        // Default filename/subfolder for saving persistent data
        private const string k_JsonFilename = "LevelLayout.json";
        private const string k_JsonSubfolder = "Json";
        private const string k_LevelLayoutSOName = "LevelLayoutFromJSON";

        // Enum to choose an external file format
        public enum Mode
        {
            ScriptableObject,
            Json
        }

        [Header("Prefabs")]
        [Tooltip("Prefab for player paddles")]
        [SerializeField] private Paddle m_PaddlePrefab;
        [Tooltip("Prefab for the ball")]
        [SerializeField] private Ball m_BallPrefab;
        [Tooltip("Prefab for setting up level walls")]
        [SerializeField] private GameObject m_WallPrefab;
        [Tooltip("Prefab for setting up level goals")]
        [SerializeField] private ScoreGoal m_GoalPrefab;

        [Header("Level Data")]
        [Tooltip("Use ScriptableObject or Json file")]
        [SerializeField] private Mode m_Mode;

        [Header("ScriptableObject Data")]
        [Tooltip("Game data for wall and goal positions")]
        [SerializeField] private LevelLayoutSO m_LevelLayout;
        [Tooltip("Event relayer for Input System actions")]
        [SerializeField] private InputReaderSO m_InputReader;

        [Header("Json Data")]
        [Tooltip("Json file for level data")]
        [SerializeField] private string m_JsonFilename;

        // Private fields
        private GameDataSO m_GameData;


        private void OnValidate()
        {
            if (m_JsonFilename == string.Empty)
            {
                m_JsonFilename = k_JsonFilename;
            }
        }

        // Sets up dependencies passed in as parameters. Use the specified LevelLayout or generates it from
        // a JSON file. 
        public void Initialize(GameDataSO gameData, InputReaderSO inputReader)
        {
            m_GameData = gameData;
            m_InputReader = inputReader;

            switch (m_Mode)
            {
                // ScriptableObject mode:  Default to the LevelLayout ScriptableObject if none is specified.
                case (Mode.ScriptableObject):
                    if (m_LevelLayout == null)
                        m_LevelLayout = m_GameData.LevelLayout;
                    break;

                // JSON mode: load the level from a JSON file
                case (Mode.Json):
                    m_LevelLayout = InitializeFromJson();
                    break;
            }

            // Check to see if all required fields in the Inspector exist
            NullRefChecker.Validate(this);

        }

        /// <summary>
        /// Loads a JSON file as text and returns a LevelLayout ScriptableObject. This allows the user
        /// to load a "modded" JSON text file and converts that into a new level setup.
        /// </summary>
        /// <returns></returns>
        private LevelLayoutSO InitializeFromJson()
        {
            string loadedFile = SaveManager.LoadTextFile(m_JsonFilename, k_JsonSubfolder);

            LevelLayoutSO tempLevelLayout = ScriptableObject.CreateInstance<LevelLayoutSO>();
            tempLevelLayout.name = k_LevelLayoutSOName;

            JsonUtility.FromJsonOverwrite(loadedFile, tempLevelLayout);
            return tempLevelLayout;
        }

        // Create the ball, paddles, walls, and goals.
        public void SetupLevel()
        {
            CreateBall();

            Paddle p1 = CreatePaddle(m_InputReader, m_GameData.Player1);
            SetPaddleSprite(p1, m_GameData.P1Sprite);

            Paddle p2 = CreatePaddle(m_InputReader, m_GameData.Player2);
            SetPaddleSprite(p2, m_GameData.P2Sprite);
            p2.transform.Rotate(0f, 0f, 180f);

            CreateWalls();
            CreateGoals(m_GameData);
        }

        private void SetPaddleSprite(Paddle paddle, Sprite sprite)
        {
            if (sprite == null || paddle == null)
                return;

            SpriteRenderer[] spriteRenderers = paddle.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sprite = sprite;
            }
        }

        public Paddle CreatePaddle(InputReaderSO inputReader, PlayerIDSO playerID)
        {
            Vector3 startPosition = (m_GameData.IsPlayer1(playerID)) ? m_LevelLayout.Paddle1StartPosition : m_LevelLayout.Paddle2StartPosition;
            Paddle paddleInstance = Instantiate(m_PaddlePrefab, startPosition, Quaternion.identity) as Paddle;
            paddleInstance.Initialize(m_GameData, playerID, inputReader);
            

            return paddleInstance;
        }

        public Ball CreateBall()
        {
            Ball ballInstance = (Ball)Instantiate(m_BallPrefab, m_LevelLayout.BallStartPosition, Quaternion.identity);
            ballInstance.Initialize(m_GameData);
            return ballInstance;
        }

        // Create the level wall layout from ScriptableObject instance
        public void CreateWalls()
        {
            foreach (TransformSaveData wall in m_LevelLayout.LevelWalls)
            {
                GameObject wallInstance = Instantiate(m_WallPrefab, wall.position, Quaternion.Euler(wall.rotation));
                wallInstance.transform.localScale = wall.localScale;
                SetTransformParent(wallInstance.transform, k_RootTransform);
            }

            if (m_LevelLayout.LevelPrefab != null)
            {
                GameObject levelInstance = Instantiate(m_LevelLayout.LevelPrefab, Vector3.zero, Quaternion.identity);
            }
        }

        // Instantiate the goals, scale, and assign PlayerIDs. Parent to specified transform.
        public void CreateGoals(GameDataSO gameData)
        {
            ScoreGoal goal1Instance = Instantiate(m_GoalPrefab, m_LevelLayout.Goal1.position, Quaternion.Euler(m_LevelLayout.Goal1.rotation));
            goal1Instance.transform.localScale = m_LevelLayout.Goal1.localScale;
            goal1Instance.Initialize(gameData.Player2);

            ScoreGoal goal2Instance = Instantiate(m_GoalPrefab, m_LevelLayout.Goal2.position, Quaternion.Euler(m_LevelLayout.Goal2.rotation));
            goal2Instance.transform.localScale = m_LevelLayout.Goal2.localScale;
            goal2Instance.Initialize(gameData.Player1);

            SetTransformParent(goal1Instance.transform, k_RootTransform);
            SetTransformParent(goal2Instance.transform, k_RootTransform);

        }

        // Parents a Transform to a specific transform if it already exists in the Hierarchy. Creates the GameObject if it doesn't exist. 
        private void SetTransformParent(Transform transformToParent, string newParentName)
        {
            GameObject newParent = GameObject.Find(newParentName);

            if (newParent == null)
            {
                newParent = new GameObject(newParentName);
            }

            transformToParent.SetParent(newParent.transform);
        }
    }
}