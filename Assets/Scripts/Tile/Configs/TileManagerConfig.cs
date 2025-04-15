using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

[AutoBuildOrLoadConfig(ConfigFilePath.TileManagerConfigFilePath)]
public class TileManagerConfig : ConfigScriptObject
{
    public struct ConfigFilePath
    {
        public const string TileManagerConfigFilePath = "Tile/ManagerConfig";
        
        public const string TileConfigFilePath = "Tile/TileConfig";
    }
    
    
    public override string ConfigName => "TileManagerConfig";

    public const int TileSystemID = -1;

    public ContactFilter2D TileGridContactFilter2D;
    
    public GameObject UnityTileHandler;
    
    [LabelText("地图大小")]
    public Vector2Int MapSize = new Vector2Int(10, 10);
    [LabelText("每次刷新时查找的范围")]
    public Vector2Int FindEmptySize = new Vector2Int(3, 3);
    [LabelText("刷新间隔")]
    public float RefreshEmptyInterval = 5f;
    [LabelText("刷新前摇(出现视觉特效提示玩家)")]
    public float RefreshBeforeSetTime = 3;
    [LabelText("显示刷新区域的预制体")]
    public GameObject RefreshAreaShow;
    [LabelText("用于显示物块和处理碰撞的预制体")]
    public GameObject TileHandlerPrefab;
    
    
    public bool AutoUpdateTileConfigs = true;

    public LayerMask TileLayerMask;
    
    public ContactFilter2D TileContactFilter;
}