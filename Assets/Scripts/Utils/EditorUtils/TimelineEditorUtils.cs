#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace Utils
{

    public static class TimelineEditorUtils
    {
        private static Texture _texture;
        private static Texture GetGreenDotTex()
        {
            if (_texture)
            {
                return _texture;
            }
            _texture  =EditorGUIUtility.IconContent("sv_icon_dot11_pix16_gizmo").image;
            return _texture;
        }
        public static Texture GreenDot { get => GetGreenDotTex(); }
        private static Dictionary<Object,TimelineClip> slectedClips => TimelineEditor.selectedClips.ToDictionary(clip => clip.asset);
        public static bool CheckTimelineClipIsSelected(this UnityEditor.Editor editor,out TimelineClip clip)
        {
            if (slectedClips.TryGetValue(editor.target, out clip))
            {
                return true;
            }
            clip = null;
            return false;
        }

        public static void DrawGreenDotIcon(Vector2 worldPos, Vector2 baseSize)
        {
            var pos = worldPos;
            var size = baseSize/ HandleUtility.GetHandleSize(pos);
            Handles.BeginGUI();
            Rect rect = new Rect().SetSize(size).SetCenter(HandleUtility.WorldToGUIPoint(pos));
            GUI.DrawTexture(rect, GreenDot);
            Handles.EndGUI();
            EditorGUI.BeginChangeCheck();
        }
    } 

}
#endif