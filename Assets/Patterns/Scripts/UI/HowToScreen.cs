using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystemsCookbook.Demos.PaddleBall
{

    /// <summary>
    /// This manages the UI for a simple tutorial screen.  Use this in conjunction with the Command Manager.
    /// The HowToScreen sends messages that the Execute or Undo button have been clicked. It also listens for
    /// the active index from the CommandManager's list of commands. Then it displays a corresponding text message
    /// on-screen.
    /// </summary>
    public class HowToScreen : MonoBehaviour
    {
        // Use these strings to query Visual Elements from UI Document root
        private const string k_BackButton = "how-to__back-button";
        private const string k_NextButton = "how-to__next-button";
        private const string k_MessageText = "how-to__message-text";

        // Inspector fields
        [Tooltip("Set the UI Document here (or get automatically from current GameObject).")]
        [SerializeField] private UIDocument m_Document;

        [Header("Broadcast to Event Channels")]
        [Tooltip("Notify other objects that the Undo button has clicked")]
        [SerializeField] private VoidEventChannelSO m_UndoButtonClicked;
        [Tooltip("Notify other objects that the Execute button has clicked")]
        [SerializeField] private VoidEventChannelSO m_ExecuteButtonClicked;

        [Header("Listen to Event Channels")]
        [Tooltip("Notification that the active index of the command list has changed")]
        [SerializeField] private IntEventChannelSO m_CommandListUpdated;
        [Space]
        [Tooltip("Messages to show")]
        [SerializeField] private List<string> m_Messages = new List<string>();

        // Private fields
        private VisualElement m_Root;
        private Button m_BackButton;
        private Button m_NextButton;
        private Label m_MessageText;

        // MonoBehaviour event functions

        // Subscribes to event channels 
        private void OnEnable()
        {
            m_CommandListUpdated.OnEventRaised += OnCommandListUpdated;
        }

        // Unsubscribes from event channels to prevent errors
        private void OnDisable()
        {
            m_CommandListUpdated.OnEventRaised -= OnCommandListUpdated;
        }

        // This gets the currently associated UIDocument component, if not already set
        private void Awake()
        {

            m_Document = GetComponent<UIDocument>();
            NullRefChecker.Validate(this);
        }

        // Sets up the UI elements and registers the button functionality
        void Start()
        {
            SetVisualElements();
            RegisterButtonCallbacks();
        }

        // Gets a reference to the root Visual Element from the UI Document, then queries the root
        // for the individual UI elements by name.
        private void SetVisualElements()
        {
            m_Root = m_Document.rootVisualElement;
            m_BackButton = m_Root.Q<Button>(k_BackButton);
            m_NextButton = m_Root.Q<Button>(k_NextButton);
            m_MessageText = m_Root.Q<Label>(k_MessageText);
        }

        // Registers the functionality of each button's ClickEvent.
        private void RegisterButtonCallbacks()
        {
            if (m_NextButton != null)
                m_NextButton.RegisterCallback<ClickEvent>(ClickExecuteButton);

            if (m_BackButton != null)
                m_BackButton.RegisterCallback<ClickEvent>(ClickUndoButton);
        }

        // Defines the button callbacks for the Execute button.
        private void ClickExecuteButton(ClickEvent evt)
        {
            if (m_ExecuteButtonClicked != null)
                m_ExecuteButtonClicked.RaiseEvent();
        }

        // Defines the button callbacks for the Execute button.
        private void ClickUndoButton(ClickEvent evt)
        {
            if (m_UndoButtonClicked != null)
                m_UndoButtonClicked.RaiseEvent();
        }

        // Event-handling methods

        // Updates the message text to use the current active index of the command list 
        private void OnCommandListUpdated(int index)
        {
            if (m_MessageText == null || index >= m_Messages.Count)
                return;

            m_MessageText.text = m_Messages[index];
        }
    }
}