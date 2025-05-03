using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using UnityEngine;

/// <summary>
/// 陨石雨
/// </summary>
public class MeteorShower : BaseRandomEvent<MeteorShowerConfig>
{
    private TimerCollection timerCollection;
    public override void OnInit()
    {
        timerCollection = new TimerCollection();
        timerCollection.CreateTimer(Config.MeteorSpawnInterval,-1,"Meteor Spawn").SetOnStart(SpawnMeteor).SetOnCompleted(SpawnMeteor).Register();
    }

    public override void OnEventStart()
    {
       
    }

    public override void OnEventUpdate()
    {
       
    }

    public override void OnEventFixedUpdate()
    {
       
    }

    public override void OnEventEnd()
    {
        timerCollection.ClearAllTimers();
    }

    public override void OnEventDestroy()
    {
       
    }

    public void SpawnMeteor(Timer timer)
    {
        Vector2 mapSize = TileManager.Instance.GetTileMapSize().size;
        List<Vector3> positions = new List<Vector3>()
        {
            new Vector3(mapSize.x/4f,mapSize.y/4f,0),
            new Vector3(mapSize.x/4f,mapSize.y/4f *3,0),
            new Vector3(mapSize.x/4f *3,mapSize.y/4f,0),
            new Vector3(mapSize.x/4f *3,mapSize.y/4f *3,0),
        };
        var finalPosition =positions.Shuffle().First();
       Entity.Instantiate(Config.MeteorPrefab,SpawnSource.SystemGenerate, finalPosition,Quaternion.identity);
    }
}