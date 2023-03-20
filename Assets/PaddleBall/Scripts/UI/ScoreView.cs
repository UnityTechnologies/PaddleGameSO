using UnityEngine;
using TMPro;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// Manages/updates the user interface TextMeshPro for a score value. Requires a TextMeshPro object.
    /// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro m_TextMesh;

        private void Reset()
        {
            if (m_TextMesh == null)
                m_TextMesh = GetComponent<TextMeshPro>();
        }

        public void UpdateText(string value)
        {
            m_TextMesh.text = value;
        }
    }
}