using System.Reflection;
using Jukey17.Reflection;
using UnityEngine;

namespace Jukey17.Timeline.Internal.Editor
{
    /// <summary>
    /// internal class UnityEditor.Timeline.WindowState
    /// </summary>
    public class WindowState : ReflectionClassBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assembly">UnityEditor.Timeline assembly</param>
        public WindowState(Assembly assembly) : base(assembly.GetType("UnityEditor.Timeline.WindowState"))
        {
            Method.Register("OffsetTimeArea", BindingFlags.Public | BindingFlags.Instance, typeof(int));
        }

        /// <summary>
        /// public void OffsetTimeArea(int pixels)
        /// </summary>
        /// <param name="instance">WindowState instance.</param>
        /// <param name="pixels">offset value at pixels</param>
        public void OffsetTimeArea(object instance, int pixels)
        {
            Method.Invoke("OffsetTimeArea", instance, pixels);
        }
    }
}
