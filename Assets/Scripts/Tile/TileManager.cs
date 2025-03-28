using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using Tiles;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Tilemaps;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class TileManager : ManagerSingleton<TileManager>,IManager,IAssemblyForeach,IEventCenter
{
    
    private TileManagerConfig _managerConfig;
    
    private TileConfig _tileConfig;
    [ShowInInspector]
    private Dictionary<Type,int> TileIDMap;
    
    [ShowInInspector]
    private GameObject unityTileHandler;
    [ShowInInspector]
    private Grid grid;
    [ShowInInspector]
    private TilemapRenderer tilemapRenderer;
    [ShowInInspector]
    private Tilemap tilemap;

    private BaseTile[,] tiles;
    private RectInt tilesRange;
    private TileFenwickTree2D _fenwickTree2D;
 
    
    private List<Collider2D> areaCheckBuffer;
    private List<Vector2Int> areaGetTileBuffer;
    
    //用于记录对应玩家的得分
    private Dictionary<EntityID, float> scoreCollection;
    
    
    private EventCenterCore eventCenterCore;

    private List<Vector2Int> needRemoveTileBuffer;


    private List<Vector2Int> canUseEmptyRange;

    private TimerCollection timerCollection;

    private bool FreshOneEmptyRange = false;

    private const string FreshIntervelTimer = "FreshIntervelTimer";
    private const string FreshStartShowingTimer = "FreshStartShowingTimer";
    /// <summary>
    /// 游玩逻辑是否在运行中
    /// </summary>
    public bool GameLogicRunning { get;private set; }
    
    public TilePRHandler TilePRHandler { get; private set; }
    
    public void Init()
    {
        _managerConfig = Config.GetConfig<TileManagerConfig>();
        _tileConfig = Config.GetConfig<TileConfig>();

        areaCheckBuffer = new List<Collider2D>();
        areaGetTileBuffer = new List<Vector2Int>();
        
        scoreCollection = new Dictionary<EntityID, float>();
        
        eventCenterCore = new EventCenterCore();
        TileIDMap = new Dictionary<Type, int>();
        needRemoveTileBuffer = new List<Vector2Int>();
        InitTileGrids();
        
        canUseEmptyRange = new List<Vector2Int>();
        
        timerCollection = new TimerCollection();
        
        TilePRHandler = ScriptableObject.CreateInstance<TilePRHandler>();
    }
    
    public void ForeachCurrentAssembly(Type[] types)
    {
        foreach (var type in types)
        {
            if (typeof(BaseTile).IsAssignableFrom(type) && !type.IsAbstract)
            {
                if (_tileConfig.TryGetTileID(type,out var id))
                {
                    TileIDMap.Add(type,id);
                }
                //理论上不该出现这个情况的，编辑器下直接退出Play得了，顺带警告一下
                else
                {
#if UNITY_EDITOR
                    EditorUtils.EditorUtils.ExitEditor();
                    $"出现不在ConfigID表范围内的Tile:{type.FullName},尝试TileConfig的RefreshTileID刷新ID表".WarningSelf();
#endif
                }
            }
        }
    }
    
    public void Update()
    {
       UpdateTile();
    }

    public void LateUpdate()
    {

    }

    public void FixedUpdate()
    {

    }

    public void UpdateTile()
    {
        foreach (var baseTile in tiles)
        {
            if(baseTile is null)
                return;
            baseTile.Update();
        }
    }
    
    public void OnDestroy()
    {
       
    }

    public void OnDrawGizmos()
    {
      
    }

    public void OnDrawGizmosSelected()
    {

    }

    /// <summary>
    /// 检查输入坐标是否在地图格子范围内
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool CheckPositionInGrid(Vector2Int position)
    {
        return tilesRange.Contains(position);
    }
    
    
    
    #region TileMethod

    private void InitTileGrids()
    {
        unityTileHandler = Object.Instantiate(_managerConfig.UnityTileHandler, Vector3.zero, Quaternion.identity,ContainerObject.transform);  
        
        grid = unityTileHandler.GetComponent<Grid>();
        tilemapRenderer = unityTileHandler.GetComponentInChildren<TilemapRenderer>();
        tilemap = unityTileHandler.GetComponentInChildren<Tilemap>();
        TileIDMap = new Dictionary<Type, int>();
        
        
        tiles = new BaseTile[_managerConfig.MapSize.x, _managerConfig.MapSize.y];
        tilesRange = new RectInt(0, 0, _managerConfig.MapSize.x, _managerConfig.MapSize.y);
    }

    /// <summary>
    /// 生成地形
    /// </summary>
    [Button]
    public void GenerateTiles()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                Vector2 offest = new Vector2(Random.Range(0,1f), Random.Range(0,1f));
                bool isAir = Mathf.PerlinNoise(i / 10f + offest.x, j / 10f + offest.y) >0.5f;
                SetTile(isAir ? typeof(AirTile): typeof(CommonTile),new Vector2Int(i,j),EntityID.Empty);
            }
        }

        _fenwickTree2D = new TileFenwickTree2D(tiles);
    }


    #region 获取Tile相关（重载与范围获取）

        /// <summary>
    /// 尝试获取指定坐标位置的Tile
    /// </summary>
    /// <param name="position">指定的格子坐标</param>
    /// <param name="tile">对应的Tile</param>
    /// <returns></returns>
    public bool TryGetTile(Vector2Int position, out BaseTile tile)
    {
        if (!CheckPositionInGrid(position))
        {
            tile = null;
            return false;
        }
        tile = tiles[position.x, position.y];
        return true;
    }
    
    /// <summary>
    /// 尝试获取指定世界坐标位置的Tile
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    /// <param name="tile">对应的Tile</param>
    /// <returns></returns>
    public bool TryGetTile(Vector2 worldPosition, out BaseTile tile)
    {
        return TryGetTile(GetTilePosition(worldPosition), out tile);
    }
    
    /// <summary>
    /// 尝试获取指定格子坐标位置的指定类型Tile
    /// </summary>
    /// <param name="position">指定的格子坐标</param>
    /// <param name="tile">对应的Tile</param>
    /// <typeparam name="T">指定的Tile类型</typeparam>
    /// <returns></returns>
    public bool TryGetTile<T>(Vector2Int position, out T tile) where T : BaseTile
    {
        if (TryGetTile(position, out var _tile) && _tile is T result)
        {
            tile = result;
            return true;
        }
        tile = null;
        return false;
    }
    /// <summary>
    /// 尝试获取指定世界坐标位置的指定类型Tile
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    /// <param name="tile">对应的Tile</param>
    /// <typeparam name="T">指定的Tile类型</typeparam>
    /// <returns></returns>
    public bool TryGetTile<T>(Vector2 worldPosition, out T tile) where T : BaseTile
    {
        return TryGetTile(GetTilePosition(worldPosition), out tile);
    }
    /// <summary>
    /// 尝试获取指定格子坐标位置的指定类型Tile
    /// </summary>
    /// <param name="position">指定的格子坐标</param>
    /// <param name="targetType">指定的Tile类型</param>
    /// <param name="tile">对应的Tile</param>
    /// <returns>是否找到</returns>
    public bool TryGetTile(Vector2Int position, Type targetType, out BaseTile tile)
    {
        if (CheckTypeIsTileType(targetType) && CheckPositionInGrid(position))
        {
            tile = tiles[position.x, position.y];
            if (tile.GetType()== targetType)
            {
                return true;
            }
        }

        tile = null;
        return false;
    }
    /// <summary>
    /// 尝试获取指定世界坐标位置的指定类型Tile
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    /// <param name="targetType">对应的Tile</param>
    /// <param name="tile">指定的Tile类型</param>
    /// <returns>是否找到</returns>
    public bool TryGetTile(Vector2 worldPosition, Type targetType, out BaseTile tile)
    {
        return TryGetTile(GetTilePosition(worldPosition), targetType, out tile);
    }
    
    public bool HasTile(Vector2Int position)
    {
        if (TryGetTile(position,out var tile) &&  TryGetTileID(typeof(AirTile),out var AirID) &&tile.TileID != AirID)
        {
            return true;
        }
        return false;
    }

    public bool HasTile<T>(Vector2Int position) where T : BaseTile
    {
        if (TryGetTile(position, out T _))
        {
            return true;
        }
        return false;
    }
    
    
    
    
    /// <summary>
    /// 尝试获取在碰撞箱范围内所有的Tile
    /// </summary>
    /// <param name="areaCollider">所指定的Collder2d</param>
    /// <param name="areaTiles">搜索结果</param>
    public void GetAreaTiles(Collider2D areaCollider,List<BaseTile> areaTiles)
    {
        areaCheckBuffer.Clear();
        areaGetTileBuffer.Clear();
        GetCoveredCells(areaCollider,areaGetTileBuffer,areaCheckBuffer);
        foreach (var vector2Int in areaGetTileBuffer)
        {
            if (TryGetTile(vector2Int, out var tile))
            {
                areaTiles.Add(tile);
            }
        }
    }
    /// <summary>
    /// 尝试获取在碰撞箱范围内所有的Tile,并指定Tile类型
    /// </summary>
    /// <param name="areaCollider">所指定的Collider2d</param>
    /// <param name="areaTiles">搜索结果</param>
    /// <typeparam name="T">指定的Tile类型</typeparam>
    public void GetAreaTiles<T>(Collider2D areaCollider, List<T> areaTiles) where T : BaseTile
    {
        areaCheckBuffer.Clear();
        areaGetTileBuffer.Clear();
        GetCoveredCells(areaCollider,areaGetTileBuffer,areaCheckBuffer);
        foreach (var vector2Int in areaGetTileBuffer)
        {
            if (TryGetTile<T>(vector2Int, out var tile))
            {
                areaTiles.Add(tile);
            }
        }
    }
    /// <summary>
    /// 尝试获取在碰撞箱范围内所有的Tile,并指定Tile类型
    /// </summary>
    /// <param name="areaCollider">所指定的Collider2d</param>
    /// <param name="targetType">指定的Tile类型</param>
    /// <param name="areaTiles">搜索结果</param>
    public void GetAreaTiles(Collider2D areaCollider,Type targetType ,List<BaseTile> areaTiles)
    {
        areaCheckBuffer.Clear();
        areaGetTileBuffer.Clear();
        GetCoveredCells(areaCollider,areaGetTileBuffer,areaCheckBuffer);
        foreach (var vector2Int in areaGetTileBuffer)
        {
            if (TryGetTile(vector2Int,targetType, out var tile))
            {
                areaTiles.Add(tile);
            }
        }
    }

    #endregion

    
    private bool TryGetTileProperty<T>(out BaseTileProperty tileProperty) where T : BaseTile
    {
        return _tileConfig.TryGetTileProperty<T>(out tileProperty);
    }

    private bool TryGetTileProperty(Type type, out BaseTileProperty tileProperty)
    {
        return _tileConfig.TryGetTileProperty(type, out tileProperty);
    }

    private bool TryGetTileID(Type type, out int id)
    {
        return TileIDMap.TryGetValue(type, out id);
    }

    private bool CheckTypeIsTileType(Type type)
    {
        return TileIDMap.ContainsKey(type);
    }
    

    private bool InnerCreateTile(Type type, out BaseTile tile)
    {
        if (TryGetTileID(type, out var tileID))
        {
            var instance = Activator.CreateInstance(type) as BaseTile;
            instance.TileID = tileID;
            tile = instance;
            return true;
        }

        tile = null;
        return false;
    }

    private bool InnerSetTile(Type type, Vector2Int position,EntityID belongsToID,out BaseTile tileInstance)
    {
        if (!CheckPositionInGrid(position))
        {
            tileInstance = null;
            return false;
        }
        //创建新的Tile
        if (InnerCreateTile(type, out BaseTile tile) && TryGetTileProperty(type, out BaseTileProperty tileProperty))
        {
            tile.TileProperty = tileProperty;
            tile.Initialize(tileProperty);
            tile.TileBelongToID = belongsToID;
            tile.Position = position;
            tiles[position.x, position.y] = tile;
            tileInstance = tile;
            tilemap.SetTile(new Vector3Int(position.x,position.y,0),TilePRHandler);
            return true;
        }

        tileInstance = null;
        return false;
    }

    /// <summary>
    /// 放置一个新的方块，使用此方法会直接覆盖原有Tile，不会触发原有Tile的破坏相关函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="position"></param>
    /// <param name="belongsToID"></param>
    public void SetTile(Type type, Vector2Int position, EntityID belongsToID)
    {
        if (InnerSetTile(type,position,belongsToID, out BaseTile tile))
        {
            //程序化的音效什么的
            SetTileRender(tile,type);
            
            //检查合并
            //CheckTileCanMerge(position);
        }
    }

    public bool TryPlaceTile(Type type, Vector2Int position, EntityID belongsToID)
    {
        if (IsAir(position))
        {
            ReplaceTile(type, position, belongsToID);
            return true;
        }

        return false;
    }
    
    
    /// <summary>
    /// 替换一个新的方块，会先触发原有Tile的破坏相关函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="position"></param>
    /// <param name="belongsToID"></param>
    public void ReplaceTile(Type type, Vector2Int position, EntityID belongsToID)
    {
        if (!CheckPositionInGrid(position))
        {
            return;
        }
        var oldTile = tiles[position.x, position.y];

        int fenwickTreeDelta = 0;
        //变成空气
        if (oldTile.GetType() != typeof(AirTile) && type == typeof(AirTile))
        {
            fenwickTreeDelta = -1;
        }
        //从空气填充
        if (oldTile.GetType() == typeof(AirTile) && type != typeof(AirTile))
        {
            fenwickTreeDelta = 1;
        }
        
        if (TryGetTileProperty(oldTile.GetType(),out var property))
        {
            oldTile.RemoveTileRender(property,tilemap);
        }
        oldTile.Destroy();
        //程序化的执行
        //放置方块
        
        SetTile(type,position,belongsToID);
        _fenwickTree2D.Update(position.x,position.y,fenwickTreeDelta);
        CheckTileCanMerge(position);
    }
    
    /// <summary>
    /// 摧毁方块（实际上是替换为空气Tile）
    /// </summary>
    /// <param name="position"></param>
    public void DestroyTile(Vector2Int position)
    {
        if (TryGetTile(position,out var tile))
        {
            var destoryArgs = new TileEvent.TileDestroyedEventArgs()
            {
                DestroyedTileID = tile.TileID,
                TilePosition = position,
            };
            BroadcastMessage(TileEvent.TileDestroyed,destoryArgs);
        }
        ReplaceTile(typeof(AirTile),position,EntityID.Empty);
    }

    public void DestroyTileByRow(int row)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            Vector2Int position = new Vector2Int(i, row);
            DestroyTile(position);
        }
    }

    public void DestroyTileByColumn(int column)
    {
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            Vector2Int position = new Vector2Int(column, i);
            DestroyTile(position);
        }
    }


    
    /// <summary>
    /// 对方块造成伤害
    /// </summary>
    /// <param name="position"></param>
    /// <param name="damage"></param>
    public void ApplyDamageToTile(Vector2Int position, DamageInfo damageInfo,out BeHittedInfo hittedInfo)
    {
        if (TryGetTile(position,out BaseTile tile))
        {
            tile.Hit(damageInfo,out hittedInfo);
            if (hittedInfo.IsKilledEntity)
            {
                BroadcastMessage(TileEvent.TileBreakedByPlayer,new TileEvent.TileBreakedByPlayerEventArgs(tile.TileID,position,damageInfo.DamageFrom));
                DestroyTile(position);
            }
            return;
        }
        hittedInfo = BeHittedInfo.Default;
    }
    
    
    #endregion

    #region 方块渲染相关

    public void SetTileRender(BaseTile tile,Type tileType)
    {
        if (TryGetTileProperty(tileType,out var property))
        {
            tile.SetTileRender(property,tilemap);
        }
    }

    #endregion

    #region 格子相关

    public Vector2Int GetTilePosition(Vector2 position)
    {
        var v = grid.WorldToCell(position);
        return new Vector2Int(v.x,v.y);
    }
    
    
    // 检测指定碰撞体覆盖的所有格子
    public void GetCoveredCells(Collider2D targetCollider,List<Vector2Int> result,List<Collider2D> checkBuffer)
    {
        if (result == null) return;
        
        // 获取包围盒范围
        Bounds bounds = targetCollider.bounds;
        int startX = Mathf.FloorToInt(bounds.min.x);
        int startY = Mathf.FloorToInt(bounds.min.y);
        int endX = Mathf.FloorToInt(bounds.max.x - 1e-6f); // 避免浮点误差
        int endY = Mathf.FloorToInt(bounds.max.y - 1e-6f);
        
        Vector2 cellSize = new Vector2(1, 1);

        // 遍历每个格子
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                Vector2 cellCenter = new Vector2(x + 0.5f, y + 0.5f);
                if (IsCellOverlapping(targetCollider, cellCenter, cellSize,checkBuffer))
                {
                    result.Add(new Vector2Int(x, y));
                }
            }
        }
    }

    // 检测单个格子是否与碰撞体重叠
    private bool IsCellOverlapping(Collider2D targetCollider, Vector2 center, Vector2 size,List<Collider2D> checkBuffer)
    {
        checkBuffer.Clear();
        Physics2D.OverlapBox(
            center, 
            size, 
            0f, 
            _managerConfig.TileContactFilter, 
            checkBuffer
        );
        foreach (var collider in checkBuffer)
        {
            if (collider == targetCollider) return true;
        }
        return false;
    }

    #endregion


    #region 方块合成相关

    public void CheckTileCanMerge(Vector2Int lastSetTilePosition)
    {
        if (!TryGetTile(lastSetTilePosition, out var lastTile))
        {
            return;
        }
        
        ReInitScoreCollection();
        //检查行
        //满足条件后直接返回
        if (CheckRowIsFull(lastSetTilePosition.y))
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                Vector2Int position = new Vector2Int(i,lastSetTilePosition.y);
                if (TryGetTile(position,out var tile))
                {
                    CalculateEachTileScore(scoreCollection, tile, lastTile);
                }
            }
            //计算完数值，发送合并事件给外界，并尝试清除对应行的tile
            BroadcastTileMergeEvent(true,lastSetTilePosition);
            DestroyTileByRow(lastSetTilePosition.y);
            return;
        }
                
        //检查列
        ReInitScoreCollection();
        if (CheckColumnIsFull(lastSetTilePosition.x))
        {
            for (int i = 0; i < tiles.GetLength(1); i++)
            {
                Vector2Int position = new Vector2Int(lastSetTilePosition.x,i);
                if (TryGetTile(position,out var tile))
                {
                    CalculateEachTileScore(scoreCollection, tile, lastTile);
                }
            }
            //计算完数值，发送合并事件给外界，并尝试清除对应列的tile
            BroadcastTileMergeEvent(false,lastSetTilePosition);
            DestroyTileByColumn(lastSetTilePosition.x);
        }

    }
    /// <summary>
    /// 检测输入的行是否在范围内
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private bool CheckInputRowValueIsVaild(int row)
    {
        return (tilesRange.xMin, tilesRange.xMax).Contains(row);
    }
    
    /// <summary>
    /// 检测指定行是否被填满（没有空气方块）
    /// </summary>
    /// <param name="row">对应行（y轴坐标）</param>
    /// <returns>是否被填满</returns>
    public bool CheckRowIsFull(int row)
    {
        if (!CheckInputRowValueIsVaild(row))
        {
            return false;
        }
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            Vector2Int position = new Vector2Int(i, row);
            if (IsAir(position))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 检测输入的列是否在范围内
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    private bool CheckInputColumnValueIsVaild(int column)
    {
        return (tilesRange.yMin, tilesRange.yMax).Contains(column);
    }
    
    /// <summary>
    /// 检查指定列是否被填满（没有空气方块）
    /// </summary>
    /// <param name="column">对应列（x轴坐标）</param>
    /// <returns>是否被填满</returns>
    public bool CheckColumnIsFull(int column)
    {
        if (!CheckInputColumnValueIsVaild(column))
        {
            return false;
        }
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            Vector2Int position = new Vector2Int(column, i);
            if (IsAir(position))
            {
                return false;
            }

            if (!TryGetTile(position,out var tile) ||!tile.CanBeMerged)
            {
                return false;
            }
        }
        return true;
    }
    

    
    
    public bool IsAir(Vector2Int position)
    {
        return TryGetTile<AirTile>(position, out _);
    }
    
    /// <summary>
    /// 计算每个方块给玩家的能量
    /// </summary>
    /// <param name="scoreCollection"></param>
    /// <param name="currectTile"></param>
    /// <param name="lastTile"></param>
    private void CalculateEachTileScore(Dictionary<EntityID,float> scoreCollection,BaseTile currectTile,BaseTile lastTile)
    {
        //中立方块
        if (currectTile.TileBelongToID == EntityID.Empty)
        {
            //更具最后一个方块属于谁来判断
            //中立则给两者都增加
            if (lastTile.TileBelongToID == EntityID.Empty)
            {
                var allkeys = scoreCollection.Keys.ToList();
                foreach (var id in allkeys)
                {
                    scoreCollection[id] += CalculateTileCurrectScore(currectTile);
                }
            }
            else
            {
                //给最后一个增加
                if (scoreCollection.ContainsKey(lastTile.TileBelongToID))
                {
                    scoreCollection[lastTile.TileBelongToID] += CalculateTileCurrectScore(currectTile);
                }
            }
        }
        else
        {
            if (scoreCollection.ContainsKey(currectTile.TileBelongToID))
            {
                scoreCollection[currectTile.TileBelongToID] += CalculateTileCurrectScore(currectTile);
            }
        }
    }
    /// <summary>
    /// 具体计算单个方块实际上输出的能量，目前只有最基本的，后续补充TODO
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    private float CalculateTileCurrectScore(BaseTile tile)
    {
        return tile.BaseMergeScore;
    }
    /// <summary>
    /// 重新构建scoreCollection
    /// </summary>
    private void ReInitScoreCollection()
    {
        scoreCollection.Clear();
        foreach (var playerID in GetAllPlayerIDs())
        {
            scoreCollection.Add(playerID, 0);
        }
    }

    /// <summary>
    /// 广播一个合并事件，未具体实现TODO
    /// </summary>
    private void BroadcastTileMergeEvent(bool isRowMerge,Vector2Int lastSetTilePosition)
    {
        TileEvent.TileMergeEventArgs eventArgs = new TileEvent.TileMergeEventArgs()
        {
            IsColumnMerge = !isRowMerge,
            IsRowMerge = isRowMerge,
            MergeStartTilePosition = lastSetTilePosition,
            scoreCollection = this.scoreCollection
        };
        BroadcastMessage(TileEvent.TileMerged, eventArgs);
    }
    
    #endregion


    #region Tile属性计算相关
    
    

    #endregion



    #region 获取与玩家相关属性

    /// <summary>
    /// 获取所有的玩家ID，未具体实现TODO
    /// </summary>
    /// <returns></returns>
    public List<EntityID> GetAllPlayerIDs()
    {
        return CharacterManager.Instance.GetAllPlayerID();
    }

    #endregion

    #region 事件中心

    public void AddListener(string messageType, params Action<EventArgs>[] action)
    {
         eventCenterCore.AddListener(messageType, action);
    }

    public void BroadcastMessage(string messageType, EventArgs eventArgs)
    {
        eventCenterCore.BroadcastMessage(messageType, eventArgs);
    }

    public void RemoveListener(string messageType, params Action<EventArgs>[] action)
    {
        eventCenterCore.RemoveListener(messageType, action);
    }

    #endregion
    public void FindEmptyArea(Vector2Int emptySize,out List<Vector2Int> emptyArea)
    {
        canUseEmptyRange.Clear();
        
        for (int i = 0; i <= tiles.GetLength(0) - emptySize.x; i++)
        {

            for (int j = 0; j <= tiles.GetLength(1) - emptySize.y; j++)
            {
                if (_fenwickTree2D.QueryRangeIsAir(i,j,emptySize.x,emptySize.y))
                {
                    canUseEmptyRange.Add(new Vector2Int(i,j));
                }
            }
        }
        emptyArea = canUseEmptyRange;
    }

    public void StartFillRandomRange()
    {
        if (!FreshOneEmptyRange)
        {
            timerCollection
                .CreateTimer(_managerConfig.RefreshEmptyInterval, -1, FreshIntervelTimer)
                .SetOnCompleted(FillTile)
                .Register();
        }
        FreshOneEmptyRange = true;
    }

    private void FillTile(Timer timer)
    {
        FindEmptyArea(_managerConfig.FindEmptySize,out var allEmpty);
        if (allEmpty.Count ==0)
        {
            return;
        }
        var result = allEmpty.ReservoirSampling(1).First();
        for (int i = 0; i < _managerConfig.FindEmptySize.x; i++)
        {
            for (int j = 0; j < _managerConfig.FindEmptySize.y; j++)
            {
                var pos = new Vector2Int(i+result.x, j+result.y);
                
                TryPlaceTile(0.5f.RandomBool() ?typeof(CommonTile) : typeof(SolidTile),pos,EntityID.Empty);
            }
        }
    }
    
    public void StopFillRandomRange()
    {
        if (FreshOneEmptyRange)
        {
            timerCollection.RemoveTimer(FreshIntervelTimer);
        }
        FreshOneEmptyRange = false;
    }

    public void RefreshTileRenderingAndCollison(Vector2Int position)
    {
        var posistion = new Vector3Int(position.x, position.y, 0);
        TileBase tile = tilemap.GetTile(new Vector3Int(position.x, position.y, 0));
        if (tile is TilePRHandler concreteTile) {
            tilemap.RefreshTile(posistion);
        }
    }
}
