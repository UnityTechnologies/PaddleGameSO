using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System.Collections;

namespace GameSystemsCookbook
{
    /// <summary>
	/// This class overrides the default Editor behavior to show the elements of the runtime set.
    /// This fixes the 'Type Mismatch' that appears in the Inspector when an Asset tries to show a 
    /// GameObject from the Hierarchy.
	/// </summary>
    [CustomEditor(typeof(GameObjectRuntimeSetSO))]
    public class GameObjectRuntimeSetSOEditor : Editor
    {

        // Reference to the target, cast as a RuntimeSet
        private GameObjectRuntimeSetSO m_RuntimeSet;

        // UI to show the contents of a list
        private ListView m_ItemsListView;

        // Label and counter for items in the list
        private Label m_ListLabel;

        private void OnEnable()
        {
            // Reference to the runtime set we are inspecting
            if (m_RuntimeSet == null)
                m_RuntimeSet = target as GameObjectRuntimeSetSO;

            // Subscribe to the ItemsChanged event.
            m_RuntimeSet.ItemsChanged += OnItemsChanged;
        }

        private void OnDisable()
        {
            // Unsubscribe from the ItemsChanged event to prevent errors.
            m_RuntimeSet.ItemsChanged -= OnItemsChanged;
        }

        // Draw the custom Inspector
        public override VisualElement CreateInspectorGUI()
        {

            var root = new VisualElement();

            // Draw default elements in the inspector
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            // Add a spacer
            var spaceElement = new VisualElement();
            spaceElement.style.marginBottom = 10;
            root.Add(spaceElement);

            // Add a title label
            m_ListLabel = new Label();
            m_ListLabel.text = "Runtime Set Items:";
            root.Add(m_ListLabel);

            // Add a horizontal line / spacer
            var separator = new VisualElement();
            separator.style.borderBottomWidth = 1;
            separator.style.borderBottomColor = Color.grey;
            separator.style.marginBottom = 10;
            root.Add(separator);

            m_ItemsListView = new ListView(m_RuntimeSet.Items, 20, MakeItem, BindItem);

            // Add the ListView to the root element
            root.Add(m_ItemsListView);
            return root;
        }

        // Generates a simple text VisualElement for each element in the ListView
        private VisualElement MakeItem()
        {
            var element = new VisualElement();
            var label = new Label();
            element.Add(label);
            return element;
        }

        // Logic to sync the ListView label text to the RuntimeSet.Items list
        private void BindItem(VisualElement element, int index)
        {
            if (m_RuntimeSet.Items.Count == 0)
                return;

            var item = m_RuntimeSet.Items[index];

            Label label = (Label)element.ElementAt(0);
            label.text = item ? item.name : "<null>";

            // Attach a ClickEvent to the label
            label.RegisterCallback<MouseDownEvent>(evt =>
            {
                // Ping the item in the Hierarchy
                EditorGUIUtility.PingObject(item);

                // Alternatively, change this so the Selection.activeObject = item;
            });

        }

        // Invokes every time the runtime set list changes
        private void OnItemsChanged()
        {
            // Format the label with an item counter
            if (m_ListLabel != null)
            {
                string message = "Runtime Set Items";
                message = (m_RuntimeSet == null) ? message + ":" : message + " (Count " + m_RuntimeSet.Items.Count + "):";
                m_ListLabel.text = message;
            }

            // Force refresh of the Inspector when the Items list changes
            if (m_ItemsListView != null)
                m_ItemsListView.Rebuild();
        }
    }
}
