using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace Editor
{
    public static class TimelineEditorUtils
    {
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

    }
}