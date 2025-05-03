using ilsFramework;

/// <summary>
/// 道具专家来了
/// </summary>
public class PropMasterArrives : BaseRandomEvent<PropMasterArrivesConfig>
{
    public override void OnInit()
    {
        
    }

    public override void OnEventStart()
    {
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            if (playerController.handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer container))
            {
                container.AddEnergy(playerController.EnergyCanBeComeProp * Config.AddPropCount);
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