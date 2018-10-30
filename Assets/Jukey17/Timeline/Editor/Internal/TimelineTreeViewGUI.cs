using System.Reflection;
using Jukey17.Reflection;
using UnityEngine;

namespace Jukey17.Timeline.Internal.Editor
{
    /// <summary>
    /// internal class UnityEditor.Timeline.TimelineTreeViewGUI
    /// </summary>
    public class TimelineTreeViewGUI : ReflectionClassBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assembly">UnityEditor.Timeline assembly</param>
        public TimelineTreeViewGUI(Assembly assembly) : base(assembly.GetType("UnityEditor.Timeline.TimelineTreeViewGUI"))
        {
            Property.Register("scrollPosition", BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// public Vector2 scrollPosition { get; }
        /// </summary>
        /// <param name="instance">TimelineTreeViewGUI instance.</param>
        /// <returns>scroll position.</returns>
        public Vector2 GetScrollPosition(object instance)
        {
            return Property.GetValue<Vector2>("scrollPosition", instance);
        }

        /// <summary>
        /// public Vector2 scrollPosition { set; }
        /// </summary>
        /// <param name="instance">TimelineTreeViewGUI instance.</param>
        /// <param name="value">scroll position.</param>
        public void SetScrollPosition(object instance, Vector2 value)
        {
            Property.SetValue("scrollPosition", instance, value);
        }

    }
}
