using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This a base class for a generic Runtime Set. Use this to track specific items of type
    /// T at runtime. See the FooRuntimeSetSO for example usage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RuntimeSetSO<T> : ScriptableObject where T: MonoBehaviour
    {
        // Use the Items to track a list of objects of type T at runtime.

        // Note: scene objects do not serialize properly in the Inspector. Hide the Items
        // list with [HideInInspector] or use a public property. Prefab objects show normally.

        // You can add an Editor script to create a custom Property drawer to display scene
        // objects.


        private List<T> m_Items = new List<T>();
        public List<T> Items => m_Items;


        // Adds a specific item to the Runtime Set's Items list.
        public void Add(T thing)
        {
            if (!Items.Contains(thing))
                Items.Add(thing);
        }

        // Removes a specific item from the Runtime Set's Items list.
        public void Remove(T thing)
        {
            if (Items.Contains(thing))
                Items.Remove(thing);
        }


    }
}