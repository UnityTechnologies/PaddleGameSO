using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
	/// This component spawns an array of Prefabs in a grid pattern -- useful for generating a wall of blocks.
	/// </summary>
    public class GridSpawner : MonoBehaviour
    {
        [Tooltip("The Prefab GameObject to spawn")]
        [SerializeField] private GameObject m_Prefab;

        [Header("Grid Settings")]
        [Tooltip("Vertical dimension")]
        [SerializeField] private int m_MaxHeight = 8;
        [Tooltip("Horizontal dimension")]
        [SerializeField] private int m_MaxWidth = 6;
        [Tooltip("Multiplier to change the vertical distance of each cell")]
        [SerializeField] private float m_SpacingY = 1f;
        [Tooltip("Multiplier to change the horizontal distance of each cell")]
        [SerializeField] private float m_SpacingX = 1f;
        [Tooltip("Start spawning at this (x,y) coordinate")]
        [SerializeField] private Vector2 m_StartPosition = new Vector2(0f, 0f);
        [Header("Runtime Set")]
        [SerializeField] private GameObjectRuntimeSetSO m_RuntimeSet;

        [Header("Listen for Event Channels")]
        [SerializeField] private VoidEventChannelSO m_SpawnClickedEvent;
        [SerializeField] private GameObjectEventChannelSO m_DestroyBlockEvent;
        [SerializeField] private VoidEventChannelSO m_ClearBlockEvent;

        private const string k_Basename = "Block";

        private void Awake()
        {
            Clear();
            NullRefChecker.Validate(this);
        }

        // Subscribe to event channels
        private void OnEnable()
        {
            m_SpawnClickedEvent.OnEventRaised += SpawnAtNextSpace;
            m_DestroyBlockEvent.OnEventRaised += Despawn;
            m_ClearBlockEvent.OnEventRaised += Clear;
        }

        // Unsubscribe from event channels to prevent errors
        private void OnDisable()
        {
            m_SpawnClickedEvent.OnEventRaised -= SpawnAtNextSpace;
            m_DestroyBlockEvent.OnEventRaised -= Despawn; 
            m_ClearBlockEvent.OnEventRaised -= Clear;
        }

        // Spawn a prefab to fill in a grid pattern. This skips over already occupied positions in the grid.
        public void SpawnAtNextSpace()
        {
            int nextIndex = m_RuntimeSet.Items.Count;
            int maxCells = m_MaxHeight * m_MaxWidth;

            for (int i = 0; i < maxCells; i++)
            {
                int xPos = i % m_MaxWidth;
                int yPos = i / m_MaxWidth;

                Vector2 nextPosition = m_StartPosition + new Vector2(xPos * m_SpacingX, yPos * m_SpacingY);

                bool isSpaceEmpty = true;

                foreach (GameObject item in m_RuntimeSet.Items)
                {
                    if (item.transform.position.x == nextPosition.x && item.transform.position.y == nextPosition.y)
                    {
                        isSpaceEmpty = false;
                        break;
                    }
                }

                if (isSpaceEmpty || m_RuntimeSet.Items.Count == 0)
                {
                    GameObject instance = Instantiate(m_Prefab, nextPosition, Quaternion.identity);
                    instance.name = k_Basename + nextIndex;
                    m_RuntimeSet.Add(instance);
                    return;
                }
            }

            // All positions taken
            Debug.LogWarning("GridSpawner: No available positions in grid.");
        }

        // Remove an item
        public void Despawn(GameObject instance)
        {
            m_RuntimeSet.Remove(instance);
        }

        // Delete the elements and clear the Runtime Set's Items list.
        public void Clear()
        {
            m_RuntimeSet.DestroyItems();
            m_RuntimeSet.Clear();
        }

    }
}
