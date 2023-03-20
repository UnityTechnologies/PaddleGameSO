using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GameSystemsCookbook
{
	/// <summary>
	/// This event channel broadcasts and carries Boolean payload.
	/// </summary>
	[CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "Events/BoolEventChannelSO")]
	public class BoolEventChannelSO : GenericEventChannelSO<bool>
	{
	 
	}
}