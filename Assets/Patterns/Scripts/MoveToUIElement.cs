using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameSystemsCookbook
{
    public class MoveToUIElement : MonoBehaviour
    {
        [SerializeField] private UIDocument m_Document;
        [SerializeField] private string m_ElementName;
        [SerializeField] private Camera m_Camera;
        [SerializeField] private float m_ZDepth = 10f;

        private VisualElement m_Root;
        private VisualElement m_Element;

        private void Awake()
        {
            NullRefChecker.Validate(this);
            ValidateElement();
        }

        private void Reset()
        {
            if (m_Document == null)
                m_Document = GetComponent<UIDocument>();

        }

        private void Start()
        {
            m_Root = m_Document.rootVisualElement;
            MatchElementPosition();

        }

        private void ValidateElement()
        {
            if (string.IsNullOrEmpty(m_ElementName))
            {
                Debug.LogError("Missing assignment for field: m_ElementName in object: " + this.gameObject.name);
            }
        }

        private void MatchElementPosition()
        {
            m_Element = m_Root.Q<VisualElement>(m_ElementName);

            transform.position = m_Element.transform.position;

            


            Vector2 localPosition = m_Element.layout.center;
            Vector2 screenPosition = m_Element.ChangeCoordinatesTo(m_Root, localPosition);
            //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            Vector3 worldPos = screenPosition.ScreenPosToWorldPos(m_Camera, m_ZDepth);

            //transform.position = worldPos;

            Debug.Log("Moving transform.position to " + worldPos);

        }

    }

}
