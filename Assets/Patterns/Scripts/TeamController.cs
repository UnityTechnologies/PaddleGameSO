using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    public class TeamController : MonoBehaviour
    {
        [SerializeField] private TeamID m_TeamID;
        [SerializeField] private SpriteRenderer m_SpriteRenderer;

        [Header("Sprite and Player IDs")]
        [SerializeField] private Sprite[] m_Sprites = new Sprite[2];
        [SerializeField] private PlayerIDSO[] m_PlayerIDS = new PlayerIDSO[2];

        [Header("Listen to Event Channels")]
        [SerializeField] private VoidEventChannelSO m_TeamToggled;

        private int m_Index;

        private void OnEnable()
        {
            m_TeamToggled.OnEventRaised += ToggleTeam;
        }

        private void OnDisable()
        {
            m_TeamToggled.OnEventRaised -= ToggleTeam;
        }

        private void Awake()
        {
            NullRefChecker.Validate(this);
            ToggleTeam();
        }

        private void Reset()
        {
            if (m_TeamID == null)
                m_TeamID = GetComponent<TeamID>();

            if (m_SpriteRenderer == null)
                m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void ToggleTeam()
        {
            m_Index = (m_Index == 0) ? 1 : 0;
            m_TeamID.ID = m_PlayerIDS[m_Index];
            m_SpriteRenderer.sprite = m_Sprites[m_Index];
        }

    }
}
