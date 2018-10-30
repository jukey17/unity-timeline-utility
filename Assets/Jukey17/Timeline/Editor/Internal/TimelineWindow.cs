using System.Reflection;
using Jukey17.Reflection;

namespace Jukey17.Timeline.Internal.Editor
{
    /// <summary>
    /// internal class UnityEditor.Timeline.TimelineWindow
    /// </summary>
    public class TimelineWindow : ReflectionClassBase
    {
        /// <summary>
        /// public static TimelineWindow instance { get; }
        /// </summary>
        public object Instance
        {
            get { return Property.GetStaticValue<object>("instance"); }
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assembly">UnityEditor.Timeline assembly</param>
        public TimelineWindow(Assembly assembly)
            : base(assembly.GetType("UnityEditor.Timeline.TimelineWindow"))
        {
            Property.Register("instance", BindingFlags.Public | BindingFlags.Static);
            Property.Register("state", BindingFlags.Public | BindingFlags.Instance);
            Property.Register("treeView", BindingFlags.Public | BindingFlags.Instance);
        }
        
        /// <summary>
        /// public WindowState state { get; }
        /// </summary>
        /// <param name="instance">TimelineWindow instance.</param>
        /// <returns>state.</returns>
        public object GetState(object instance)
        {
            return Property.GetValue<object>("state", instance);
        }

        /// <summary>
        /// public TimelineTreeViewGUI treeView { get; }
        /// </summary>
        /// <param name="instance">TimelineWindow instance.</param>
        /// <returns>tree view.</returns>
        public object GetTreeView(object instance)
        {
            return Property.GetValue<object>("treeView", instance);
        }

    }
}