using UnityEngine;
using System.Collections.Generic;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This structure defines one line as it will appear in the credits.
    /// 
    /// </summary>
    [System.Serializable]
    public class Credit
    {
        public string Name;
        public string Role;
    }

    /// <summary>
    /// A container for text data. This example shows a basic credit screen.
    /// </summary>
    [CreateAssetMenu(fileName = "CreditsSO", menuName = "GameSystems/CreditsSO")]
    public class CreditsSO : DescriptionSO
    {

        [Tooltip("Title at the top of the credits screen")]
        [SerializeField] private string m_Title;
        [Tooltip("Additional descriptive text below the title")]
        [SerializeField] private string m_SubHeading;
        [Tooltip("Full list of Credits to show on screen")]
        [SerializeField] private List<Credit> m_Credits;

        public List<Credit> Credits => m_Credits;
        public string Title => m_Title;
        public string SubHeading => m_SubHeading;
    }
}