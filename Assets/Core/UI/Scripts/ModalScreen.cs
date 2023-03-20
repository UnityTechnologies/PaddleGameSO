using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// A ModalScreen UI represents one "mode" of the interface. Only one mode can be
    /// active at a time. 
    /// 
    /// Use this component for full-screen or pop-up UIs that hide or deactivate
    /// the rest of the interface.
    /// </summary>
    public class ModalScreen : View
    {
        // Even though there is no extra functionality from this derived class, the
        // UIManager includes a generic GetView method that finds the first View of a
        // certain type. This can help tag and identify different parts of the UI.

    }
}
