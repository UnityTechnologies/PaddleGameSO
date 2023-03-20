using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This is an example Runtime Set used for tracking one or more GameObjects or components at runtime.
    /// This can replace using singleton instances (which tend to make testing more difficult).
	///
	/// Note: reference the RuntimeSetSO for a Generic base class.
	/// 
    /// </summary>
    [CreateAssetMenu(menuName = "GameSystems/GameObject Runtime Set", fileName = "GameObjectRuntimeSet")]
    public class GameObjectRuntimeSetSO : DescriptionSO
    {
        // Event for the Editor script 
        public System.Action ItemsChanged;

        [Header("Optional")]
        [Tooltip("Notify other objects this Runtime Set has changed")]
        [SerializeField, Optional] private VoidEventChannelSO m_RuntimeSetUpdated;

        // Use the Items to track a list of GameObjects at runtime.
        private List<GameObject> m_Items = new List<GameObject>();
        public List<GameObject> Items {get => m_Items; set => m_Items = value;}

        private void OnEnable()
        {
            if (ItemsChanged != null)
                ItemsChanged();
        }

        // Adds one GameObject to the Items
        public void Add(GameObject thingToAdd)
        {
            if (!m_Items.Contains(thingToAdd))
                m_Items.Add(thingToAdd);

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();

            if (ItemsChanged != null)
                ItemsChanged();
        }

        // Removes one GameObject from the Items
        public void Remove(GameObject thingToRemove)
        {
            if (m_Items.Contains(thingToRemove))
                m_Items.Remove(thingToRemove);

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();

            if (ItemsChanged != null)
                ItemsChanged();

        }

        // Reset the list
        public void Clear()
        {
            m_Items.Clear();

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();

            if (ItemsChanged != null)
                ItemsChanged();
        }

        // Clean up any items after the list is cleared
        public void DestroyItems()
        {
            foreach (GameObject item in m_Items)
            {
                Destroy(item);
            }
        }
    }
}