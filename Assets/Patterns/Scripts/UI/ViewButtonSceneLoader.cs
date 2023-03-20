using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
    [System.Serializable]
    public class ViewButtonSceneLoadData
    {
        [Tooltip("Unique name of the UI Button from the Visual Tree Asset")]
        public string buttonName;

        // Reference to the UIElements.Button element (unseen in Inspector)
        public Button button;

        [Tooltip("Scene path to load")]
        public string scenePath;

        // Event channel that will be raised when the button is clicked
        [Tooltip("Event channel to notify SceneLoader")]
        public StringEventChannelSO sceneLoadChannel;

    }

    [RequireComponent(typeof(View))]
    public class ViewButtonSceneLoader : MonoBehaviour
    {
        // Inspector fields
        [SerializeField] private ViewButtonSceneLoadData m_ButtonSceneLoadData;

        // Example scene path from EditorBuildSettings.scenes
        public const string k_DefaultScenePath = "Assets/GameSystems/Demos/PaddleBall/Scenes/PaddleBall.unity";

        private View m_View;
        private UIDocument m_Document;

        private void Awake()
        {
            m_View = GetComponent<View>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {

            // Gets a reference to the UIElements Button
            m_Document = m_View.Document;
            VisualElement root = m_Document.rootVisualElement;

            m_ButtonSceneLoadData.button = root.Q<Button>(m_ButtonSceneLoadData.buttonName);

            if (m_ButtonSceneLoadData.button == null)
                Debug.LogError("ViewButtonSceneLoader: Invalid UI Button specified", transform);

            // Register the event channel's RaiseEvent as a callback for the button's click event
            m_ButtonSceneLoadData.button.clicked += () => m_ButtonSceneLoadData.sceneLoadChannel.RaiseEvent(m_ButtonSceneLoadData.scenePath);
        }

    }
}
