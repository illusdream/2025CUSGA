using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 对应初始化关卡的信息
/// </summary>
public class LevelSetting : MonoBehaviour
{
        public const string LevelSettingGOName = "LevelSetting";
        
        [LabelText("玩家1出生的位置信息")]
        public Transform Player1SpawnTransform;
        [LabelText("玩家2出生的位置信息")]
        public Transform Player2SpawnTransform;
        
        
        //摄像机信息
        
        
        //显示活动区域
        [ToggleLeft]
        [LabelText("显示可游玩的范围")]
        public bool ShowPlayableRange;

        public void Reset()
        {
                gameObject.name = "LevelSetting";
        }


        public void OnDrawGizmos()
        {
                if (ShowPlayableRange)
                {             
                        var playRange = Config.GetConfigInEditor<CharacterConfig>().PlayerCanPlayRange;
                        Gizmos.color = Color.green * new Color(1,1,1,0.1f);
                        Gizmos.DrawWireCube(playRange.center, new Vector3(playRange.size.x, playRange.size.y, 1));
                }
        }
}