using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This is a non-functional placeholder class for illustrating how to use generic Runtime Set ScriptableObjects.
    ///
    /// If using the Foo component with a prefab, reference the RuntimeSet asset with the m_FooSet. Then, any instances of
    /// the prefab will automatically include themselves in the m_FooSet at runtime.
    /// </summary>

    public class Foo : MonoBehaviour
    {
        [SerializeField] private RuntimeSetSO<Foo> m_FooSet;

        // MonoBehaviour event functions

        private void OnEnable()
        {
            if (m_FooSet != null)
                m_FooSet.Add(this);
        }

        private void OnDisable()
        {
            if (m_FooSet != null)
                m_FooSet.Remove(this);
        }
    }
}
