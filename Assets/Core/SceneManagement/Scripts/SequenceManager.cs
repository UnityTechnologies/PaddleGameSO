using System.Collections;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This component uses a state machine to load different parts of the application.
    /// Use the "pre-load" time to show a splash screen and load any assets at startup.
    /// </summary>
    ///
    // TO-DO: use a ScriptableObject to define sequences, then use a state machine so that
    // each state can preload its own assets, raise events on enter and exit 

    public class SequenceManager : MonoBehaviour
    {

        // Inspector fields
        [Header("Preload (Splash Screen)")]
        [Tooltip("Prefab assets that load first.")]
        [SerializeField] private GameObject[] m_PreloadedAssets;
        [Tooltip("Minimal time in seconds to show splash screen")]
        [SerializeField] private float m_PreloadDelay = 2f;

        [Header("Broadcast on Event Channels")]
        [Tooltip("Update the loading progress value (percentage)")]
        [SerializeField] private FloatEventChannelSO m_LoadProgressUpdated;
        [Tooltip("Notify listeners that preloading has finished.")]
        [SerializeField] private VoidEventChannelSO m_PreloadComplete;

        [Header("Listen to Event Channels")]
        [SerializeField] private VoidEventChannelSO m_ApplicationQuit;

        // MonoBehaviour event functions

        // Preloads any Prefabs that will be instantiated on start, then loads the next Scene.
        private void Start()
        {
            NullRefChecker.Validate(this);
            InstantiatePreloadedAssets();
            DelaySplashScreen(m_PreloadDelay);
        }

        // Subscribe to event channels
        private void OnEnable()
        {
            m_ApplicationQuit.OnEventRaised += ExitApplication;
        }

        // Unsubscribe from event channels to prevent errors
        private void OnDisable()
        {
            m_ApplicationQuit.OnEventRaised -= ExitApplication;
        }

        // Methods

        // Use this to preload any assets
        private void InstantiatePreloadedAssets()
        {
            foreach (var asset in m_PreloadedAssets)
            {
                Instantiate(asset);
            }
        }

        private void DelaySplashScreen(float delay)
        {
            StartCoroutine(WaitForSplashScreen(delay));
        }

        IEnumerator WaitForSplashScreen(float delay)
        {
            float endSplashScreenTime = Time.time + delay;

            while (Time.time < endSplashScreenTime)
            {
                float progress = 100 * (1 - (endSplashScreenTime - Time.time) / delay);
                progress = Mathf.Clamp(progress, 0f, 100f);

                OnLoadProgressUpdated(progress);

                yield return null;
            }

            OnShowMainMenu();
        }

        // Raises event channel during splash screen to update progress bar
        private void OnLoadProgressUpdated(float progressValue)
        {
            m_LoadProgressUpdated.RaiseEvent(progressValue);
        }

        // Signals that preloading is complete
        private void OnShowMainMenu()
        {
            m_PreloadComplete.RaiseEvent();
        }

        private void ExitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
