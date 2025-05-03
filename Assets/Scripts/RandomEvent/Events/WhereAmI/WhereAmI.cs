using ilsFramework;

/// <summary>
/// 我在哪
/// </summary>
public class WhereAmI : BaseRandomEvent<WhereAmIConfig>
{
    private TimerCollection _timerCollection;
    public override void OnInit()
    {
        this._timerCollection = new TimerCollection();
    }

    public override void OnEventStart()
    {
        Swap();

        _timerCollection.CreateTimer(5, 2, "WhereAmI").SetOnCompleted(_ => Swap()).Register();
    }

    public override void OnEventUpdate()
    {
        
    }

    public override void OnEventFixedUpdate()
    {
       
    }

    public override void OnEventEnd()
    {
       _timerCollection.ClearAllTimers();
    }

    public override void OnEventDestroy()
    {
       
    }
    
    private void Swap()
    {
        var p1Transform = CharacterManager.Instance.Player1Controller.transform;
        var p2Transform = CharacterManager.Instance.Player2Controller.transform;
             
        (p1Transform.position, p2Transform.position) = (p2Transform.position, p1Transform.position);
    }
}