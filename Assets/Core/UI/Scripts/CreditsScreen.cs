using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This component reads a ScriptableObject with text to display on a UIScreen.
 
    /// </summary>

    public class CreditsScreen: MonoBehaviour
    {
        // UI Toolkit IDs for Visual Elements and/or Classes
        public const string k_CreditLineClassName = "credit-line";

        public const string k_DefaultParent = "credits__parent";
        public const string k_Title = "credits__title";
        public const string k_Subheading = "credits__subheading";
        public const string k_Body = "credits__body";
        public const string k_CreditName = "credits__name";
        public const string k_CreditRole = "credits__role";


        [Header("Scriptable Object")]
        [Tooltip("This ScriptableObject stores the credit data")]
        [SerializeField] private CreditsSO m_CreditsData;
        [Header("UI Elements")]
        [Tooltip("The UI Toolkit document.")]
        [SerializeField] private UIDocument m_Document;
        [Space]
        [Tooltip("A UXML template to instantiate for each credit line")]
        [SerializeField] private VisualTreeAsset m_CreditLineAsset;
        [Tooltip("Style to apply to credit line instance")]
        [SerializeField] private string m_CreditLineClassName;
        [Space]
        [Tooltip("The title of the Credits Screen.")]
        [SerializeField] private string m_TitleName;
        [Tooltip("Additional descriptive text under the title.")]
        [SerializeField] private string m_SubheadingName;
        [Tooltip("Visual Element that holds the credits.")]
        [SerializeField] private string m_BodyName;
        [Header("Listen to Event Channels")]
        [Tooltip("Notification that this screen should update")]
        [SerializeField, Optional] private VoidEventChannelSO m_UpdateEvent;

        private VisualElement m_Root;
        private VisualElement m_Body;
        private Label m_Title;
        private Label m_Subheading;

        protected void Awake()
        {
            Initialize();
            ReadScriptableObject();
        }

        private void OnEnable()
        {
            if (m_UpdateEvent != null)
                m_UpdateEvent.OnEventRaised += ReadScriptableObject;
        }

        // Unsubscribe to prevent errors.
        private void OnDisable()
        {
            if (m_UpdateEvent != null)
                m_UpdateEvent.OnEventRaised -= ReadScriptableObject;
        }

        private void Reset()
        {
            if (m_Document == null)
                m_Document = GetComponent<UIDocument>();
        }

        // Sets default values into string fields if left blank
        private void OnValidate()
        {
            if (m_CreditLineClassName == string.Empty)
                m_CreditLineClassName = k_CreditLineClassName;

            if (m_TitleName == string.Empty)
                m_TitleName = k_Title;

            if (m_SubheadingName == string.Empty)
                m_SubheadingName = k_Subheading;

            if (m_BodyName == string.Empty)
                m_BodyName = k_Body;
        }

        // Sets the UI Document if necessary and then caches any needed Visual Elements.
        public void Initialize()
        {
            NullRefChecker.Validate(this);

            m_Root = m_Document.rootVisualElement;
            m_Body = m_Root.Q<VisualElement>(m_BodyName);
            m_Title = m_Root.Q<Label>(m_TitleName);
            m_Subheading = m_Root.Q<Label>(m_SubheadingName);
        }

        // Instantiates a Visual Element from the template, parents it to the hierarchy,
        // and then swaps the text.
        private void GenerateCreditLine(Credit credit)
        {
            // Instantiate a new Visual Element from a template UXML
            TemplateContainer creditLineInstance = m_CreditLineAsset.Instantiate();

            // Attach the instance to the correct Visual Element in the VisualTree
            m_Body.Add(creditLineInstance);

            // Apply the styling using USS Class Selector
            creditLineInstance.AddToClassList(m_CreditLineClassName);

            // Find the Labels within the instance and replace the text
            Label creditName = creditLineInstance.Q<Label>(k_CreditName);
            creditName.text = credit.Name;
            Label creditRole = creditLineInstance.Q<Label>(k_CreditRole);
            creditRole.text = credit.Role;
        }

        // Reads in the title, description text, and credit list.
        private void ReadScriptableObject()
        {

            m_Title.text = m_CreditsData.Title;
            m_Subheading.text = m_CreditsData.SubHeading;

            m_Body.Clear();

            for (int i = 0; i < m_CreditsData.Credits.Count; i++)
            {
                if (m_CreditsData.Credits[i] != null)
                    GenerateCreditLine(m_CreditsData.Credits[i]);
            }
        }
    }
}
