using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;


namespace GameSystemsCookbook
{
    /// <summary>
	/// Simple class to select a GameObject or an Asset from a UI Button.
	/// </summary>
    public class ButtonSelectionBinder : MonoBehaviour
    {
        [Tooltip("The GameObject or Asset to select.")]
        [SerializeField, Optional] private Object m_ObjectToSelect;
        [Tooltip("The UI Toolkit document.")]
        [SerializeField] private UIDocument m_Document;
        [Tooltip("The name of the Button to query for.")]
        [SerializeField, Optional] private string m_ButtonID;

        private VisualElement m_Root;
        private Button m_Button;

        private void Reset()
        {
            if (m_Document == null)
                m_Document = GetComponent<UIDocument>();
        }

        private void Awake()
        {
            Initialize();
        }

        // Check null references and register the callback on the button
        private void Initialize()
        {
            NullRefChecker.Validate(this);

            m_Root = m_Document.rootVisualElement;
            m_Button = m_Root.Q<Button>(m_ButtonID);

            if (m_Button == null)
                Debug.LogError("ButtonSelectionBinder: Invalid m_ButtonID", this);

            m_Button.clicked += Select;
        }

        private void OnDisable()
        {
            m_Button.clicked -= Select;
        }

        // Select the GameObject or Asset
        private void Select()
        {
            if (m_ObjectToSelect == null)
            {
                Debug.LogError("ButtonSelectionBinder: m_ObjectToSelect is null.", this);
                return;
            }

            if (!AssetDatabase.Contains(m_ObjectToSelect))
            {
                // The GameObject is in the Hierarchy
                GameObject activeObject = m_ObjectToSelect as GameObject;

                if (activeObject != null)
                {
                    Selection.activeGameObject = activeObject;
                }
                else
                {
                    // If this is an Asset generated at runtime (not common)
                    Debug.LogError("ButtonSelectionBinder: m_ObjectToSelect is not a GameObject.", this);
                }
            }
            else
            {
                // The object is an Asset in the Project
                Selection.activeObject = m_ObjectToSelect;
            }
        }
    }
}
