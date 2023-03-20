using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This struct pairs an event channel with a View. 
    /// </summary>
    [System.Serializable]
    public struct ViewDisplayEventChannel
    {
        [Tooltip("Signal to listen for")]
        public VoidEventChannelSO eventChannel;

        [Tooltip("UI view to enable")]
        public View viewToDisplay;
    }

    /// <summary>
    /// The UI Manager manages the UI screens (View base class) using event channels
    /// paired with each View. A stack maintains a history of previously shown screens, so
    /// the UI Manager can "go back" until it reaches the default UI screen, the home screen.
    /// 
    /// Adapted for UI Toolkit-based elements from the Runner Template.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // Use a low sorting value so the home screen is always at the bottom
        public const int k_HomeScreenSortOrder = -1;

        [Tooltip("Default starting modal screen (e.g. main menu)")]
        [SerializeField] private View m_HomeScreen;

        [Header("Listen to Event Channels")]
        [Tooltip("Notification to go back one screen")]
        [SerializeField] private VoidEventChannelSO m_ScreenGoBack;
        [Tooltip("Notification to hide all UI screens")]
        [SerializeField] private VoidEventChannelSO m_AllScreensHidden;
        [Tooltip("Notification to show the Home Screen")]
        [SerializeField] private VoidEventChannelSO m_HomeScreenShown;

        [Space]
        [Tooltip("Displays the specified View when receiving notification via the event channel")]
        [SerializeField] private List<ViewDisplayEventChannel> m_DisplayViewOnEvent;

        // The currently active View
        private View m_CurrentView;

        // A stack of previously displayed Views
        private Stack<View> m_History = new Stack<View>();

        // A list of all Views to show/hide
        private List<View> m_Views = new List<View>();


        // Subscribes to event channels:
        //
        // - Shows each View when raising the event channel (via a lambda as a callback)
        // - m_ScreenGoBack returns to the last used screen in the interface
        // - m_AllScreensHidden hides all screens
        // - m_HomeScreenShown shows the default screen

        private void OnEnable()
        {
            foreach (ViewDisplayEventChannel channel in m_DisplayViewOnEvent)
            {
                channel.eventChannel.OnEventRaised += () => Show(channel.viewToDisplay);
            }

            m_ScreenGoBack.OnEventRaised += GoBack;
            m_AllScreensHidden.OnEventRaised += HideViews;
            m_HomeScreenShown.OnEventRaised += ShowHomeScreen;
        }

        // Unregister the callback to prevent errors
        private void OnDisable()
        {
            foreach (ViewDisplayEventChannel channel in m_DisplayViewOnEvent)
            {
                channel.eventChannel.OnEventRaised -= () => Show(channel.viewToDisplay);
            }

            m_ScreenGoBack.OnEventRaised -= GoBack;
            m_AllScreensHidden.OnEventRaised -= HideViews;
            m_HomeScreenShown.OnEventRaised -= ShowHomeScreen;
        }

        private void Start()
        {
            Initialize();
        }

        // Clears history and hides all Views except the home screen
        private void Initialize()
        {
            RegisterViews();

            HideViews();

            ShowHomeScreen();
        }

        // Store each View from m_DisplayViewOnEvent into the master list of Views
        private void RegisterViews()
        {
            if (m_HomeScreen != null && !m_Views.Contains(m_HomeScreen))
                m_Views.Add(m_HomeScreen);

            foreach (ViewDisplayEventChannel channel in m_DisplayViewOnEvent)
            {
                if (channel.viewToDisplay != null && !m_Views.Contains(channel.viewToDisplay))
                    m_Views.Add(channel.viewToDisplay);
            }
        }

        // Sets the home screen's sorting order and shows it
        private void ShowHomeScreen()
        {
            if (m_HomeScreen == null)
                return;

            HideViews();
            m_HomeScreen.Document.sortingOrder = k_HomeScreenSortOrder;
            m_History.Clear();
            m_History.Push(m_HomeScreen);
            m_HomeScreen.Show();
            m_CurrentView = m_HomeScreen;

        }

        // Clear history and hide all Views
        private void HideViews()
        {
            m_History.Clear();
            foreach (View view in m_Views)
            {
                view.Hide();
            }
        }

        // Finds the first registered UI View of the specified type T
        public T GetView<T>() where T : View
        {
            foreach (var view in m_Views)
            {
                if (view is T tView)
                {
                    return tView;
                }
            }
            return null;
        }

        // Shows a View of a specific type T, with the option to add it
        // to the history stack
        public void Show<T>(bool keepInHistory = true) where T : View
        {
            foreach (var view in m_Views)
            {
                if (view is T)
                {
                    Show(view, keepInHistory);
                    break;
                }
            }
        }

        // Shows the View with the option to add it to the history stack
        public void Show(View view, bool keepInHistory = true)
        {
            if (view == null)
                return;

            if (m_CurrentView != null)
            {
                if (keepInHistory)
                {
                    m_History.Push(m_CurrentView);
                }

                if (!view.IsTransparent)
                {
                    m_CurrentView.Hide();
                }

            }

            view.Show();

            m_CurrentView = view;
        }

        // Shows a View with the keepInHistory always enabled
        public void Show(View view)
        {
            Show(view, true);
        }

        // Remove the top UI screen from the stack and make that active (go back one screen)
        public void GoBack()
        {
            if (m_History.Count != 0)
            {
                Show(m_History.Pop(), false);
            }
        }
    }
}

