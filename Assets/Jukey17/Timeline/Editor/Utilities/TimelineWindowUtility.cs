using System.Reflection;
using Jukey17.Timeline.Internal.Editor;
using UnityEditor.Timeline;

namespace Jukey17.Timeline.Editor
{
    public static class TimelineWindowUtility
    {
        static readonly TimelineWindow TimelineWindow;
        static readonly WindowState WindowState;
        static readonly TimelineTreeViewGUI TreeView;

        static TimelineWindowUtility()
        {
            var assembly = Assembly.Load("UnityEditor.Timeline");
            TimelineWindow = new TimelineWindow(assembly);
            WindowState = new WindowState(assembly);
            TreeView = new TimelineTreeViewGUI(assembly);
        }

        public static bool IsOpen
        {
            get { return TimelineWindow.Instance != null; }
        }

        public static void ScrollToHorizontal(int offset)
        {
            if (!IsOpen)
            {
                return;
            }

            var stateInstance = TimelineWindow.GetState(TimelineWindow.Instance);
            WindowState.OffsetTimeArea(stateInstance, offset);
            TimelineEditor.Refresh(RefreshReason.WindowNeedsRedraw);
        }

        public static void ScrollToVertical(int offset)
        {
            if (!IsOpen)
            {
                return;
            }

            var treeViewInstance = TimelineWindow.GetTreeView(TimelineWindow.Instance);
            var scrollPosition = TreeView.GetScrollPosition(treeViewInstance);
            scrollPosition.y -= offset;
            TreeView.SetScrollPosition(treeViewInstance, scrollPosition);
            TimelineEditor.Refresh(RefreshReason.WindowNeedsRedraw);
        }
    }
}