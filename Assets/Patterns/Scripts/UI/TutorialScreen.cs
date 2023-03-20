using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
    /// <summary>
	/// Class for showing pages of text on-screen.
	/// </summary>
    public class TutorialScreen : View
    {
        // Default names
        public const string k_NextButton = "tutorial__button-next";
        public const string k_LastButton = "tutorial__button-last";
        public const string k_CurrentPage = "tutorial__page-text";
        public const string k_Title = "tutorial__title";
        public const float k_DestroyDelay = 0.2f;

        [Header("Content")]
        [Tooltip("ScriptableObject that holds the text and events")]
        [SerializeField] private TutorialScreenSO m_TutorialScreenData;

        [Header("Visual Elements")]
        [Tooltip("UI Button to advance to the next page of text")]
        [SerializeField] private string m_NextButtonID;
        [Tooltip("UI Button to go back to the last page of text")]
        [SerializeField] private string m_LastButtonID;
        [Tooltip("Title text label")]
        [SerializeField] private string m_TitleID;
        [Tooltip("Text label containing the content body")]
        [SerializeField] private string m_CurrentPageID;

        private Button m_NextButton;
        private Button m_LastButton;
        private VisualElement m_Root;
        private Label m_CurrentPage;
        private Label m_Title;
        private int m_PageIndex;
        private List<GameObject> m_PrefabInstances = new List<GameObject>();

        // Set up default names in the Inspector fields.
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(m_NextButtonID))
                m_NextButtonID = k_NextButton;

            if (string.IsNullOrEmpty(m_LastButtonID))
                m_LastButtonID = k_LastButton;

            if (string.IsNullOrEmpty(m_CurrentPageID))
                m_CurrentPageID = k_CurrentPage;

            if (string.IsNullOrEmpty(m_TitleID))
                m_TitleID = k_Title;
        }

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        // Set up UI Elements and supporting GameObjects
        public override void Initialize()
        {
            base.Initialize();

            m_Root = m_Document.rootVisualElement;
            SetVisualElements();
            ShowPage(m_PageIndex);
        }

        // Hide the screen the clean up any leftover Prefab instances
        public override void Hide()
        {
            // Destroy after a brief delay to let any objects complete their behavior
            foreach (GameObject instance in m_PrefabInstances)
            {
                Destroy(instance, k_DestroyDelay);
            }

            m_PrefabInstances.Clear();
            base.Hide();
        }

        // Show the UI and any associated Prefabs
        public override void Show()
        {
            base.Show();
            GeneratePrefabs();
        }

        // Instantiate any Prefabs needed for the screen (e.g. non-UI supporting objects)
        private void GeneratePrefabs()
        {
            if (m_PrefabInstances.Count > 0)
            {
                return;
            }

            foreach (GameObject prefab in m_TutorialScreenData.Prefabs)
            {
                GameObject instance = Instantiate(prefab);
                m_PrefabInstances.Add(instance);
            }
        }

        // Locate the UI Elements using string IDs. Then register the Button callbacks.
        private void SetVisualElements()
        {
            m_NextButton = m_Root.Q<Button>(m_NextButtonID);
            m_LastButton = m_Root.Q<Button>(m_LastButtonID);
            m_CurrentPage = m_Root.Q<Label>(m_CurrentPageID);
            m_Title = m_Root.Q<Label>(m_TitleID);

            m_Title.text = m_TutorialScreenData.Title;

            m_NextButton.RegisterCallback<ClickEvent>(evt => ShowNextPage());
            m_LastButton.RegisterCallback<ClickEvent>(evt => ShowLastPage());
        }

        // Unregister the callbacks to prevent errors
        private void OnDisable()
        {
            m_NextButton.UnregisterCallback<ClickEvent>(evt => ShowNextPage());
            m_LastButton.UnregisterCallback<ClickEvent>(evt => ShowLastPage());
        }

        // Show a specific text block in the body UI
        private void ShowPage(int index)
        {
            int clampedIndex = Mathf.Clamp(index, 0, m_TutorialScreenData.BodyText.Count - 1);
            m_CurrentPage.text = m_TutorialScreenData.BodyText[clampedIndex];

            UpdateNextLastButtons();
        }

        // Increment to the next page of text
        private void ShowNextPage()
        {
            m_PageIndex++;
            m_PageIndex = Mathf.Clamp(m_PageIndex, 0, m_TutorialScreenData.BodyText.Count - 1);
            ShowPage(m_PageIndex);
        }

        // Decrement to the previous page of text
        private void ShowLastPage()
        {
            m_PageIndex--;
            m_PageIndex = Mathf.Clamp(m_PageIndex, 0, m_TutorialScreenData.BodyText.Count - 1);
            ShowPage(m_PageIndex);
        }

        // Toggle the m_NextButton and m_LastButton depending on index.
        private void UpdateNextLastButtons()
        {
            if (m_TutorialScreenData.BodyText.Count <= 1)
            {
                FadeElement(m_NextButton, false);
                FadeElement(m_LastButton, false);
                return;
            }

            if (m_PageIndex == 0)
            {
                FadeElement(m_NextButton, true);
                FadeElement(m_LastButton, false);
                return;
            }

            if (m_PageIndex >= m_TutorialScreenData.BodyText.Count - 1)
            {
                FadeElement(m_NextButton, false);
                FadeElement(m_LastButton, true);
                return;
            }

            FadeElement(m_NextButton, true);
            FadeElement(m_LastButton, true);
        }

        // Enable/disable a specific UI Element.
        private void DisplayElement(VisualElement element, bool state)
        {
            element.style.display = (state) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        // Fade an element on or off; use USS transitions for the in-betweens
        private void FadeElement(VisualElement element, bool state)
        {
            element.style.opacity = (state) ? 1f : 0f;
        }

    }
}
