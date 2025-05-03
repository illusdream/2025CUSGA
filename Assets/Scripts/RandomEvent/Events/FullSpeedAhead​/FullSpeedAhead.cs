using System.Collections.Generic;

/// <summary>
/// 全员加速中
/// </summary>
public class FullSpeedAhead : BaseRandomEvent<FullSpeedAheadConfig>
{

    private HashSet<EntityHandler> entityHandlers;
    
    public override void OnInit()
    {
        entityHandlers  = new HashSet<EntityHandler>();
    }

    public override void OnEventStart()
    {
        
    }

    public override void OnEventUpdate()
    {
        entityHandlers.Clear();
            
        var mapCenter = TileManager.Instance.GetTileMapSize().center;
        var mapSize =TileManager.Instance.GetTileMapSize().size;
            
        EntityManager.Instance.GetEntityOverlapBox(mapCenter, mapSize,0,Config.EffectToEntity,entityHandlers);
            
        ProcessEntity(entityHandlers);
    }

    public override void OnEventFixedUpdate()
    {
        
    }

    public override void OnEventEnd()
    {
        
    }

    public override void OnEventDestroy()
    {
       
    }
    
    public void ProcessEntity(HashSet<EntityHandler> findEntity)
    {
        foreach (var entityHandler in findEntity)
        {
            if (entityHandler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer baseBuffContainer))
            {
                baseBuffContainer.AddBuff(EBuffType.FullSpeedAheadBuff);
            }
        }
    }
}