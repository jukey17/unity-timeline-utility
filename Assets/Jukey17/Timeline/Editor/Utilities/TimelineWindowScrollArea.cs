using UnityEditor;
using UnityEngine;

namespace Jukey17.Timeline.Editor
{
    public class TimelineWindowScrollArea : EditorWindow
    {
        static readonly Vector2 MinSize = Vector2.one * 10;

        enum Direction
        {
            Vertical,
            Horizontal,
        }

        bool settingVisible;
        Direction detectDirection = Direction.Vertical;
        Direction scrollDirection = Direction.Vertical;
        bool deltaPositive = true;
        int coefficient = 1;

        [MenuItem("Jukey17/Timeline/Open TimelineWindow Scroll Area")]
        static void Open()
        {
            var window = CreateInstance<TimelineWindowScrollArea>();
            window.Show();
        }

        void OnEnable()
        {
            UpdateTitleContent();
            minSize = MinSize;
        }

        void OnGUI()
        {
            HandleEvent();

            if (settingVisible)
            {
                DrawSetting();
            }
        }

        void DrawSetting()
        {
            detectDirection = (Direction) EditorGUILayout.EnumPopup("Detect Direction", detectDirection);

            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                scrollDirection = (Direction) EditorGUILayout.EnumPopup("Scroll Direction", scrollDirection);
                if (scope.changed)
                {
                    UpdateTitleContent();
                }
            }

            deltaPositive = EditorGUILayout.Toggle("Delta Positive", deltaPositive);
            coefficient = Mathf.Clamp(EditorGUILayout.IntField("Coefficient", coefficient), 1, 100);
        }

        void HandleEvent()
        {
            var current = Event.current;
            switch (current.type)
            {
                case EventType.ScrollWheel:
                    Scroll(current);
                    break;
                case EventType.ContextClick:
                    ContextClick(current);
                    break;
            }
        }

        void Scroll(Event current)
        {
            var value = coefficient * (deltaPositive ? 1f : -1f);

            if (detectDirection == Direction.Vertical)
            {
                value *= current.delta.y;
            }
            else
            {
                value *= current.delta.x;
            }

            if (scrollDirection == Direction.Vertical)
            {
                TimelineWindowUtility.ScrollToVertical((int) value);
            }
            else
            {
                TimelineWindowUtility.ScrollToHorizontal((int) value);
            }
        }

        void ContextClick(Event current)
        {
            var menu = new GenericMenu();
            var content = new GUIContent((settingVisible ? "Disable" : "Visible") + " Setting");
            menu.AddItem(content, false, SwitchSettingVisible);
            menu.ShowAsContext();
        }

        void SwitchSettingVisible()
        {
            settingVisible = !settingVisible;
        }

        void UpdateTitleContent()
        {
            titleContent = new GUIContent(scrollDirection == Direction.Vertical ? "VScroll" : "HScroll");
        }
    }
}