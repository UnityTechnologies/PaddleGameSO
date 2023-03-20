using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{

    /// <summary>
    /// Base class for managing UI Toolkit-based screens in conjunction with the
    /// UI Manager component. Derive classes to manage the main parts of the UI
    /// (i.e. Settings screen, Tutorial screen, etc.)
    ///
    /// View includes to methods to:
    ///     - Initialize the button click events and document settings
    ///     - Hide and show the parent UI element
    /// </summary>
    public abstract class View : MonoBehaviour
    {

        [Tooltip("Optional reference to the UI Document. Defaults to UIManager's Document if not specified.")]
        [SerializeField] protected UIDocument m_Document;
        [Tooltip("String ID for the top most Visual Element of the UI within the tree")]
        [SerializeField] protected string m_ParentName;

        [Header("Visibility")]
        [Tooltip("Is the UI hidden by default")]
        [SerializeField] protected bool m_HideOnAwake = true;
        [Tooltip("Is the UI partially see through")]
        [SerializeField] protected bool m_IsTransparent;


        protected VisualElement m_ParentElement;

        public VisualElement ParentElement => m_ParentElement;
        public UIDocument Document => m_Document;
        public bool IsTransparent => m_IsTransparent;
        public bool IsHidden => m_ParentElement.style.display == DisplayStyle.None;

        protected virtual void Awake()
        {
            Initialize();

            if (m_HideOnAwake)
                Hide();
        }

        protected void Reset()
        {
            if (m_Document == null)
                m_Document = GetComponent<UIDocument>();
        }

        // Queries a UIDocument to find the UI's topmost element within the Visual Tree Asset.
        //
        // Then, iterates through the m_ButtonData list, querying for each button's unique name and creates
        // a new Action<ClickEvent> for each item.  
        public virtual void Initialize()
        {
            NullRefChecker.Validate(this);

            // Gets reference to the parent container
            VisualElement root = m_Document.rootVisualElement;
            m_ParentElement = root.Q<VisualElement>(m_ParentName);
        }

        // Sets the front/back arrangement order in the UIDocument
        public virtual void SetSortingOrder(int sortingOrder)
        {
            m_Document.sortingOrder = sortingOrder;
        }

        public virtual void Show()
        {
            m_ParentElement.style.display = DisplayStyle.Flex;
        }

        public virtual void Hide()
        {
            m_ParentElement.style.display = DisplayStyle.None;
        }
    }
}
