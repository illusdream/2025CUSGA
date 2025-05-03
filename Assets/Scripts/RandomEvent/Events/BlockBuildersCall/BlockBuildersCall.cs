/// <summary>
/// 砖瓦工来了
/// </summary>
public class BlockBuildersCall : BaseRandomEvent<BlockBuildersCallConfig>
{
    public override void OnInit()
    {
        
    }

    public override void OnEventStart()
    {
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            if (playerController.handler.TryGetComponet(EntityComponetUsage.playerTileHandler,out PlayerTileHandler handler))
            {
                handler.AddPlayerHasTile(Config.PlayerAddTileCount);
            }
        }
    }

    public override void OnEventUpdate()
    {
       
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
}