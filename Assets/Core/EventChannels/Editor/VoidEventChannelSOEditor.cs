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
    /// Editor script to add a custom Inspector to the VoidEventChannelSO. This uses a custom
    /// ListView to show all subscribed listeners.
    /// </summary>
    [CustomEditor(typeof(VoidEventChannelSO))]
    public class VoidEventChannelSOEditor : Editor
    {

        // Reference to the original event channel (to set up button callback)
        private VoidEventChannelSO m_EventChannel;

        // Label and counter for items in the list
        private Label m_ListenersLabel;
        private ListView m_ListenersListView;
        private Button m_RaiseEventButton;

        private void OnEnable()
        {
            if (m_EventChannel == null)
                m_EventChannel = target as VoidEventChannelSO;
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
            m_RaiseEventButton.RegisterCallback<ClickEvent>(evt => m_EventChannel.RaiseEvent());
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

            string combinedName = listener.gameObject.name + " (" + listener.GetType().Name +")";
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

        // Non-UI Toolkit method: Makes an Inspector button to raise an event for testing

        //public override void OnInspectorGUI()
        //{

        //    DrawDefaultInspector();

        //    VoidEventChannelSO eventChannel = (VoidEventChannelSO)target;

        //    if (GUILayout.Button("Raise Event"))
        //    {
        //        eventChannel.RaiseEvent();
        //    }
        //}
    }
}
