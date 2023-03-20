using UnityEngine;
using UnityEngine.Events;

namespace GameSystemsCookbook.Demos.PaddleBall
{

    /// <summary>
    /// This event channel passes a PlayerScore payload (PlayerID, Score, Score UI) for the
    /// PaddleBall demo game. Use this to communicate between the ScoreManager and other components.
    /// 
    /// Each PlayerScore struct contains a PlayerID and references to the Score UI and Score components.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/PlayerScore Event Channel", fileName = "PlayerScore")]
    public class PlayerScoreEventChannelSO : GenericEventChannelSO<PlayerScore>
    {
        //public UnityAction<PlayerScore> OnEventRaised;

        //public void RaiseEvent(PlayerScore playerScore)
        //{
        //    if (OnEventRaised != null)
        //        OnEventRaised.Invoke(playerScore);
        //}
    }
}