using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystemsCookbook.Demos.PaddleBall
{
    /// <summary>
    /// This ScriptableObject-based event passes a List of PlayerScores as a payload. Use this to
    /// facilitate communication between the ScoreManager and ScoreObjectiveSO in the Pong demo.
    ///
    /// Each PlayerScore struct contains a PlayerID and references to the Score UI and Score components.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/ScoreList Event Channel", fileName = "ScoreListEventChannel")]
    public class ScoreListEventChannelSO : GenericEventChannelSO<List<PlayerScore>>
    {
        //public UnityAction<List<PlayerScore>> OnEventRaised;

        //public void RaiseEvent(List<PlayerScore> scoreList)
        //{
        //    if (OnEventRaised != null)
        //        OnEventRaised.Invoke(scoreList);
        //}
    }
}
