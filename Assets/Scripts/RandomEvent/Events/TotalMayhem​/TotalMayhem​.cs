using ilsFramework;

/// <summary>
/// 彻底疯狂
/// </summary>
public class TotalMayhem : BaseRandomEvent<TotalMayhemConfig>
{
    public override void OnInit()
    {
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            if (playerController.handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer container))
            {
                container.AddBuff(EBuffType.TotalMayhemBuff);
            }
        }
    }

    public override void OnEventStart()
    {
        AudioManager.Instance.Play(AudioChannelName.Sound, Config.ObtainSound);
    }

    public override void OnEventUpdate()
    {
       
    }

    public override void OnEventFixedUpdate()
    {
      
    }

    public override void OnEventEnd()
    {
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            if (playerController.handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer container))
            {
                container.RemoveBuff(EBuffType.TotalMayhemBuff);
            }
        }
    }

    public override void OnEventDestroy()
    {
      
    }
}