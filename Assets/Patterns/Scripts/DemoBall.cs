using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystemsCookbook.Demos.PaddleBall;

namespace GameSystemsCookbook
{
    [RequireComponent(typeof(Ball))]
    public class DemoBall : MonoBehaviour
    {
        [Tooltip("Gameplay data")]
        [SerializeField] private GameDataSO m_GameData;
        [Tooltip("Required ball component")]
        [SerializeField] private Ball m_Ball;
        [Tooltip("Signal that gameplay has begun")]
        [SerializeField, Optional] private VoidEventChannelSO m_GameStarted;

        private void Awake()
        {
            m_Ball.Initialize(m_GameData);
        }

        private void Reset()
        {
            if (m_Ball == null)
                m_Ball = GetComponent<Ball>();
        }

        private void Start()
        {
            NullRefChecker.Validate(this);

            if (m_GameStarted != null)
                m_GameStarted.RaiseEvent();

        }


    }
}
