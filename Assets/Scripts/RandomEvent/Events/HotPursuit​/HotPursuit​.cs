using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using UnityEngine;

/// <summary>
/// 火烧屁股
/// </summary>
public class HotPursuit : BaseRandomEvent<HotPursuitConfig>
{
    private GameObject hotPursuitGO;
    
    AudioEmitter emitter;
    public override void OnInit()
    {
        
    }

    public override void OnEventStart()
    {
        var instance = GameObject.Instantiate(Config.Prefab);

        var mapSize = TileManager.Instance.GetTileMapSize();

        List<(Vector2, Vector2)> randomSelect = new()
        {
            (Vector2.right, new Vector2(0, mapSize.height / 2f)),
            (Vector2.left, new Vector2(mapSize.width, mapSize.height / 2f)),
            (Vector2.down, new Vector2(mapSize.width/2f, 0)),
            (Vector2.up, new Vector2(mapSize.width/2f,  mapSize.height)),
        };

        var final = randomSelect.Shuffle().First();
        if (instance)
        {
            hotPursuitGO = instance;
            if (instance.TryGetComponent<HotPursuitGOController>(out var result))
            {
                result.Initialize(final.Item1,Config.DamagePerSec);
            }
            instance.transform.position = final.Item2;
        }

        emitter = AudioManager.Instance.Play(AudioChannelName.Sound, Config.fireSound);
    }

    public override void OnEventUpdate()
    {
       
    }

    public override void OnEventFixedUpdate()
    {
       
    }

    public override void OnEventEnd()
    {
       GameObject.Destroy(hotPursuitGO);
       emitter.Stop();
    }

    public override void OnEventDestroy()
    {
      
    }
}