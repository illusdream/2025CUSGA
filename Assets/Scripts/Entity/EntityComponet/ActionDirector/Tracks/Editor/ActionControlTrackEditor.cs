using System.Linq;
using ilsFramework;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.Timeline;
using UnityEngine.Timeline;

[CustomTimelineEditor(typeof(ActionControlTrack))]
public class ActionControlTrackEditor : TrackEditor
{
        public override void OnCreate(TrackAsset track, TrackAsset copiedFrom)
        {
                if (track.parent is TimelineAsset tlAsset)
                {
                      var count = tlAsset.GetOutputTracks().Count((_track)=> _track.GetType() == typeof(ActionControlTrack));
                      if (count > 1)
                      {
                                Undo.PerformUndo();
                                EditorUtility.DisplayDialog("多余的控制轨道", "一个Timeline上最多存在一个控制轨道(ActionControl),\n新创建的轨道已自动删除", "确认");
                      }
                }
                base.OnCreate(track, copiedFrom);
        }

        public override void OnTrackChanged(TrackAsset track)
        {
                //稍微有点麻烦
                int clipIndex =0;
                foreach (TimelineClip clip in track.GetClips())
                {
                        if (clip.asset is ActionControlClip actionControlClip)
                        {
                                clipIndex++;
                                actionControlClip.ClipIndex = clipIndex;
                        }
                }
                base.OnTrackChanged(track);
        }
}