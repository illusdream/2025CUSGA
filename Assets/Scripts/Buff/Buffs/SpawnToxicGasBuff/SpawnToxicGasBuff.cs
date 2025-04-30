using ilsFramework;
using Props;
using UnityEngine;

public class SpawnToxicGasBuff : BaseBuff<SpawnToxicGasBuffConfig>
{
    TimerCollection timerCollection = new TimerCollection();

    private GasCanisterPropConfig PropConfig;

    private EntityHandler Handler;
    protected override void OnAddBuff(EntityHandler handler)
    {
        PropManager.Instance.TryGetPropConfig(typeof(GasCanisterProp),out PropConfig);
        timerCollection.CreateTimer(Config.SpawnGasInterval, -1, "SpawnToxicGas").SetOnCompleted(SpawnToxicGas).Register();
        this.Handler = handler;
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        timerCollection.ClearAllTimers();
    }

    public override void OnResetBuffTimer()
    {
        
    }

    private void SpawnToxicGas(Timer timer)
    {
        EntityManager.Instance.Instantiate(PropConfig.ToxicGasPrefab, Handler.SpawnEntityBySelf(), Handler.transform.position, Quaternion.identity);
    }
}