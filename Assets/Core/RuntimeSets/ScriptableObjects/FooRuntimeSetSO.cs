using UnityEngine;

namespace GameSystemsCookbook
{

    /// <summary>
    /// This demonstrates how to create a new Runtime Set using the generic base class. Simply create a new
    /// empty class that derives from RuntimeSetSO, passing the Type into the angle brackets.
    ///
    /// Here, the Runtime Set tracks custom Foo components. Use Runtime Sets to track enemy units, dropped loot, etc.
    /// </summary>
    [CreateAssetMenu(menuName = "GameSystems/Foo Runtime Set", fileName = "FooRuntimeSet")]
    public class FooRuntimeSetSO : RuntimeSetSO<Foo>
    {

    }
}