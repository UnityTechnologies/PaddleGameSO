using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace GameSystemsCookbook
{
	/// <summary>
	/// Use this to store explanatory text and Prefabs for the tutorial screens in the demo.
	/// </summary>
	[CreateAssetMenu(fileName = "TutorialScreenSO", menuName = "GameSystems/TutorialScreenSO")]
	public class TutorialScreenSO : DescriptionSO
	{
		[SerializeField] private string m_Title;
		[TextArea(5, 10)]
		[SerializeField] private List<string> m_BodyText;
		[SerializeField] private List<GameObject> m_Prefabs;

		public string Title => m_Title;
		public List<string> BodyText => m_BodyText;
		public List<GameObject> Prefabs => m_Prefabs;
		

	}
}