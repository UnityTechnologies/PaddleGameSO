using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This Editor class creates a custom Inspector for event channels that carry a payload
    /// (e.g., BoolEventChannelSO, FloatEventChannelSO, etc.). A ListView shows the subscribed
    /// listeners in the scene. Click each item to ping it in the Hierarchy.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [CustomEditor(typeof(GenericEventChannelSO<>), true)]
    public abstract class GenericEventChannelSOEditor<T> : Editor
    {
        private GenericEventChannelSO<T> m_EventChannel;

        // Label and counter for items in the list
        private Label m_ListenersLabel;
        private ListView m_ListenersListView;
        private Button m_RaiseEventButton;

        private void OnEnable()
        {
            if (m_EventChannel == null)
                m_EventChannel = target as GenericEventChannelSO<T>;
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            // Draw default elements in the inspector
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            var spaceElement = new VisualElement();
            spaceElement.style.marginBottom = 20;
            root.Add(spaceElement);

            // Add a label
            m_ListenersLabel = new Label();
            m_ListenersLabel.text = "Listeners:";
            m_ListenersLabel.style.borderBottomWidth = 1;
            m_ListenersLabel.style.borderBottomColor = Color.grey;
            m_ListenersLabel.style.marginBottom = 2;
            root.Add(m_ListenersLabel);

            // Add a ListView to show Listeners
            m_ListenersListView = new ListView(GetListeners(), 20, MakeItem, BindItem);
            root.Add(m_ListenersListView);

            // Button to test event
            m_RaiseEventButton = new Button();
            m_RaiseEventButton.text = "Raise Event";
            m_RaiseEventButton.RegisterCallback<ClickEvent>(evt => m_EventChannel.RaiseEvent(default(T)));
            m_RaiseEventButton.style.marginBottom = 20;
            m_RaiseEventButton.style.marginTop = 20;
            root.Add(m_RaiseEventButton);

            return root;
        }

        private VisualElement MakeItem()
        {
            var element = new VisualElement();
            var label = new Label();
            element.Add(label);
            return element;
        }

        private void BindItem(VisualElement element, int index)
        {
            //if (m_RuntimeSet.Items.Count == 0)
            //    return;
            List<MonoBehaviour> listeners = GetListeners();

            var item = listeners[index];

            Label label = (Label)element.ElementAt(0);
            label.text = GetListenerName(item);

            // Attach a ClickEvent to the label
            label.RegisterCallback<MouseDownEvent>(evt =>
            {
                // Ping the item in the Hierarchy
                EditorGUIUtility.PingObject(item.gameObject);
            });

        }

        private string GetListenerName(MonoBehaviour listener)
        {
            if (listener == null)
                return "<null>";

            string combinedName = listener.gameObject.name + " (" + listener.GetType().Name + ")";
            return combinedName;

        }

        // Gets a list of MonoBehaviours that are listening to the event channel
        private List<MonoBehaviour> GetListeners()
        {
            List<MonoBehaviour> listeners = new List<MonoBehaviour>();

            if (m_EventChannel == null || m_EventChannel.OnEventRaised == null)
                return listeners;

            // Get all delegates subscribed to the OnEventRaised action
            var delegateSubscribers = m_EventChannel.OnEventRaised.GetInvocationList();

            foreach (var subscriber in delegateSubscribers)
            {
                // Get the MonoBehaviour associated with each delegate
                var componentListener = subscriber.Target as MonoBehaviour;

                // Append to the list and return
                if (!listeners.Contains(componentListener))
                {
                    listeners.Add(componentListener);
                }
            }

            return listeners;
        }

    }
}

// To create additional custom Inspectors for your event channels, simply derive the 
// corresponding Editor classes from GenericEventChannelSOEditor<T> filling in the
// appropriate type T. Add the CustomEditor Attribute with the concrete class type.

// To use this, the event channel must derive from GenericEventChannelSO<T>.

// For example to create the custom Inspector for FloatEventChannelSO:

//[CustomEditor(typeof(FloatEventChannelSO))]
//public class FloatEventChannelSOEditor : GenericEventChannelSOEditor<float>
//{

//}

// Leave the implementation blank unless it requires extra customization.


