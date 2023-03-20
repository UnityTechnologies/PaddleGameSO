using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;

namespace GameSystemsCookbook
{
    /// <summary>
    /// Use this basic helper for loading scenes by name, index, etc.
    /// 
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        // Fields
        [Header("Listen to Event Channels")]
        [Tooltip("Loads a scene by its Scene path string")]
        [SerializeField, Optional] private StringEventChannelSO m_LoadScenePathEventChannel;
        [Tooltip("Reloads the current scene")]
        [SerializeField, Optional] private VoidEventChannelSO m_ReloadSceneEventChannel;
        [Tooltip("Loads the next scene by index in the Build Settings")]
        [SerializeField, Optional] private VoidEventChannelSO m_LoadNextSceneEventChannel;

        [Tooltip("Unloads the last scene, stops gameplay")]
        [SerializeField, Optional] private VoidEventChannelSO m_LastSceneUnloaded;

        // Default loaded scene that serves as the entry point and does not unload
        private Scene m_BootstrapScene;

        // The previously loaded scene
        private Scene m_LastLoadedScene;

        public Scene BootstrapScene => m_BootstrapScene;

        private void OnEnable()
        {
            if (m_LoadScenePathEventChannel != null)
                m_LoadScenePathEventChannel.OnEventRaised += LoadSceneByPath;

            if (m_ReloadSceneEventChannel != null)
                m_ReloadSceneEventChannel.OnEventRaised += ReloadScene;

            if (m_LastSceneUnloaded != null)
                m_LastSceneUnloaded.OnEventRaised += UnloadScene;
        }

        private void OnDisable()
        {
            if (m_LoadScenePathEventChannel != null)
                m_LoadScenePathEventChannel.OnEventRaised -= LoadSceneByPath;

            if (m_ReloadSceneEventChannel != null)
                m_ReloadSceneEventChannel.OnEventRaised -= ReloadScene;

            if (m_LastSceneUnloaded != null)
                m_LastSceneUnloaded.OnEventRaised -= UnloadScene;
        }

        // Event-handling methods
        public void LoadSceneByPath(string scenePath)
        {
            StartCoroutine(LoadScene(scenePath));
        }

        public void UnloadScene()
        {
            StartCoroutine(UnloadLastScene());
        }

        // Reload the current scene
        public void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        // Load the next scene by index in the Build Settings
        public void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        // Load a scene by its index number (non-additively)
        public void LoadScene(int buildIndex)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);

            if (string.IsNullOrEmpty(scenePath))
            {
                Debug.LogError("SceneLoader.LoadScene: invalid sceneBuildIndex");
                return;
            }

            SceneManager.LoadScene(scenePath);
        }


        // Coroutine to unload the previous scene and then load a new scene by scene path string
        private IEnumerator LoadScene(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
            {
                Debug.LogError("SceneLoader: Invalid scene name");
                yield break;
            }

            yield return UnloadLastScene();
            yield return LoadSceneAsync(scenePath);

        }

        // Coroutine to load a scene asynchronously by scene path string in Additive mode,
        // keeps the original scene as the active scene.
        private IEnumerator LoadSceneAsync(string scenePath)
        {
            //Debug.Log("System IO path = " + Path.GetFileName(scenePath));

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                float progress = asyncLoad.progress;
                yield return null;
            }

            m_LastLoadedScene = SceneManager.GetSceneByPath(scenePath);
            SceneManager.SetActiveScene(m_LastLoadedScene);
        }


        // Unloads the previously loaded scene if it's not the bootstrap scene
        private IEnumerator UnloadLastScene()
        {
            if (m_LastLoadedScene != m_BootstrapScene)
                yield return UnloadScene(m_LastLoadedScene);
        }

        // Coroutine to unload a specific Scene asynchronously
        private IEnumerator UnloadScene(Scene scene)
        {
            if (!m_LastLoadedScene.IsValid())
                yield break;

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }

    }
}

