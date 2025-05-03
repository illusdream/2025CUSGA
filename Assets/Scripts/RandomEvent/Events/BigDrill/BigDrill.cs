/// <summary>
/// 大钻头
/// </summary>
public class BigDrill : BaseRandomEvent<BigDrillConfig>
{
    public override void OnInit()
    {
        
    }

    public override void OnEventStart()
    {
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            playerController.CurrenctDigAsset = Config.BigTrillDigAsset;
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
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            playerController.CurrenctDigAsset =playerController.DefaultDigAsset;
        }
    }

    public override void OnEventDestroy()
    {
       
    }
}