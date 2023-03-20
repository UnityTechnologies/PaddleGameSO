using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This controls the flow of gameplay in the PaddleBall demo. The GameManager notifies listeners of
    /// game states. 
    /// </summary>

    [RequireComponent(typeof(GameSetup))]
    public class GameManager : MonoBehaviour
    {
        [Tooltip("Starts the game automatically when loading scene")]
        [SerializeField] private bool m_AutoStart = true;
        [Tooltip("Required component for setup and initialization")]
        [SerializeField] private GameSetup m_GameSetup;

        [Header("ScriptableObjects")]
        [Tooltip("ScriptableObject for general game settings")]
        [SerializeField] private GameDataSO m_GameData;
        [Tooltip("ScriptableObject for relaying input")]
        [SerializeField] private InputReaderSO m_InputReader;

        [Header("Broadcast on Event Channels")]
        [Tooltip("Begin gameplay")]
        [SerializeField] private VoidEventChannelSO m_GameStarted;
        [Tooltip("End gameplay")]
        [SerializeField] private VoidEventChannelSO m_GameEnded;
        [Tooltip("Score point and update UI")]
        [SerializeField] private PlayerIDEventChannelSO m_PointScored;
        [Tooltip("Shows winning player name")]
        [SerializeField] private StringEventChannelSO m_WinnerShown;
        [Tooltip("Notifies listeners to go back to main menu scene")]
        [SerializeField] private VoidEventChannelSO m_SceneUnloaded;
        [Tooltip("Notifies UIs to close all screens and go back to home screen")]
        [SerializeField] private VoidEventChannelSO m_HomeScreenShown;

        [Header("Listen to Event Channels")]
        [Tooltip("The ball has hit a goal")]
        [SerializeField] private PlayerIDEventChannelSO m_GoalHit;
        [Tooltip("A player has reached winning score")]
        [SerializeField] private PlayerScoreEventChannelSO m_ScoreTargetReached;
        [Tooltip("All objectives are complete")]
        [SerializeField] private VoidEventChannelSO m_AllObjectivesCompleted;
        [Tooltip("Reset the score and stop gameplay. Begins gameplay if AutoStart not enabled")]
        [SerializeField] private VoidEventChannelSO m_GameReset;
        [Tooltip("Is the game time paused?")]
        [SerializeField] private BoolEventChannelSO m_IsPaused;
        [Tooltip("Notifies listeners to go back to main menu scene")]
        [SerializeField] private VoidEventChannelSO m_GameQuit;

        private bool m_IsGameOver;

        // Subscribe to event channels
        private void OnEnable()
        {
            m_GoalHit.OnEventRaised += OnGoalHit;
            m_ScoreTargetReached.OnEventRaised += OnTargetScoreReached;
            m_AllObjectivesCompleted.OnEventRaised += OnAllObjectivesCompleted;
            m_GameReset.OnEventRaised += ResetGame;
            m_InputReader.GameRestarted += OnReplay;
            m_IsPaused.OnEventRaised += PauseGame;
            m_GameQuit.OnEventRaised += OnUnloadScene;
        }

        // Unsubscribe from event channels to prevent errors
        private void OnDisable()
        {
            m_GoalHit.OnEventRaised -= OnGoalHit;
            m_ScoreTargetReached.OnEventRaised -= OnTargetScoreReached;
            m_AllObjectivesCompleted.OnEventRaised -= OnAllObjectivesCompleted;
            m_GameReset.OnEventRaised -= ResetGame;
            m_InputReader.GameRestarted -= OnReplay;
            m_IsPaused.OnEventRaised -= PauseGame;
            m_GameQuit.OnEventRaised -= OnUnloadScene;
        }

        // Gets the GameSetup component and initializes any necessary dependencies
        private void Awake()
        {
            Initialize();
        }

        // Plays the game automatically if m_AutoStart is enabled
        private void Start()
        {
            if (m_AutoStart)
                StartGame();
        }

        // Fills in the required GameSetup component if not assigned in the Inspector.
        public void Reset()
        {
            if (m_GameSetup == null)
                m_GameSetup = GetComponent<GameSetup>();
        }

        // Checks if we are missing any necessary components/assets/dependencies to play the game. Passes dependences
        // to the m_GameSetup and then sets up the walls, ball, and paddles.
        private void Initialize()
        {
            NullRefChecker.Validate(this);
            m_GameSetup.Initialize(m_GameData, m_InputReader);
            m_GameSetup.SetupLevel();
        }

        // If the dependencies are initialized properly, start the game and notify
        // any listeners
        public void StartGame()
        {
            m_GameStarted.RaiseEvent();
        }

        // Alternatively, use the expression-bodied syntax:
        //public void StartGame() => m_GameStarted.RaiseEvent();

        // Sets the game over flag and notifies any listeners
        public void EndGame()
        {
            m_IsGameOver = true;
            m_GameEnded.RaiseEvent();
        }

        // Sets the game over flag to false and calls StartGame.
        private void ResetGame()
        {
            m_IsGameOver = false;
            StartGame();
        }

        private void PauseGame(bool state)
        {
            Time.timeScale = (state) ? 0 : 1;
        }

        // Notifies listeners (e.g. ScoreManager) that a player has scored  
        private void OnGoalHit(PlayerIDSO playerID)
        {
            m_PointScored.RaiseEvent(playerID);
        }

        // Sends the win message to the ScoreManager
        private void OnTargetScoreReached(PlayerScore playerScore)
        {
            string message = playerScore.playerID.name.Replace("_SO", "") + " wins";
            m_WinnerShown.RaiseEvent(message);
        }

        // Ends the game if all objectives are complete
        private void OnAllObjectivesCompleted()
        {
            m_IsGameOver = true;
            m_GameEnded.OnEventRaised();
        }

        // Restarts the game from the win screen input. Ignored if not already finished
        // playing. 
        private void OnReplay()
        {
            if (!m_IsGameOver)
                return;

            m_GameStarted.RaiseEvent();
            m_IsGameOver = false;
        }

        private void OnUnloadScene()
        {
            m_SceneUnloaded.RaiseEvent();
            m_HomeScreenShown.RaiseEvent();

        }


    }
}