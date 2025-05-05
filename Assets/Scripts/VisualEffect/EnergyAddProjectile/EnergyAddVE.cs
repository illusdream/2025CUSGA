using System.Collections.Generic;
using ilsFramework;
using Unity.Mathematics;
using UnityEngine;
using Utils;

public class EnergyAddVE : BaseVisualEffectPool<EnergyAddVEConfig>
{
    
    public const string PoolName = "EnergyAddVE";

    public Dictionary<GameObject, EnergyAddProjectileController> VEControllers;
    
    public override void InitPool()
    {
        VEControllers = new Dictionary<GameObject, EnergyAddProjectileController>();
        
        pool=  GameObjectPoolFactory
            .Create()
            .SetName(PoolName)
            .SetInitialCapacity(Config.InitialPoolSize)
            .SetMaxCapacity(Config.MaxPoolSize)
            .SetCollectionCheck(true)
            .SetGameObjectParent(VisualPoolContainer.transform)
            .SetCreateObjectFunc(CreateVE)
            .SetActionOnGet(PoolGetVE)
            .SetActionOnRecycle(PoolRecyleVE)
            .SetActionOnDestroy(PoolDestroyAudioEmitter)
            .Register();
    }

    public override bool TryGetPool(out GameObject poolObject)
    {
        if (pool.Get() is GameObject result)
        {
            poolObject = result;
            return true;
        }
        poolObject = null;
        return false;
    }

    public override void ReleasePool(GameObject poolObject)
    {
        pool.Recycle(poolObject);
    }

    public override void PoolOnDestroy()
    {
        VEControllers.Clear();
        pool.OnDestroy();
    }

    public override void ClearPool()
    {
        var aos = pool.GetActiveObjects().ToArray();

        foreach (var gameObject in aos)
        {
            pool.Recycle(gameObject);
        }
    }

    public void TryEmittingVE(Vector2 center, Vector2 size, Vector2 velocityRange, float TotalEnergy, int playerID)
    {
        float energyNeedEmtting = TotalEnergy;

        while (energyNeedEmtting >0 )
        {
            var shootEnergy = energyNeedEmtting > Config.SingleVeEnergyMaxCaplity ? Config.SingleVeEnergyMaxCaplity : energyNeedEmtting;

            var vel = Vector2.left.Rotate((0, math.PI * 2).RandomRange()) * (velocityRange.x,velocityRange.y).RandomRange();
            if (CharacterManager.Instance.TryGetPlayerController(playerID,out PlayerController controller))
            {
                var instance = pool.Get();
                if (!instance)
                {

                    if (CharacterManager.Instance.TryGetPlayerController(playerID, out controller))
                    {
                        if (controller.handler.TryGetComponet(EntityComponetUsage.EnergyContainer, out PlayerEnergyContainer container))
                        {
                            energyNeedEmtting -= Config.SingleVeEnergyMaxCaplity;
                            container.AddEnergy(shootEnergy);
                        }
                    }
                    continue;
                }
                if (instance.TryGetComponent<EnergyAddProjectileController>(out var result) && instance.TryGetComponent<Rigidbody2D>(out var rigidbody))
                {
                    var pos = center + new Vector2((-size.x / 2,size.x / 2).RandomRange(), (-size.y / 2,size.y / 2).RandomRange());
                    instance.transform.position = center;
                    result.Initialize(controller.transform,controller,shootEnergy);
                    rigidbody.velocity = vel;
                    energyNeedEmtting -= Config.SingleVeEnergyMaxCaplity;
                }
            }
        }
    }
    
    
    
    private GameObject CreateVE(GameObjectPool pool)
    {
        var go = GameObject.Instantiate(Config.EnergyAddVEPrefab);

        if (go && go.TryGetComponent<EnergyAddProjectileController>(out var controller) && VEControllers.TryAdd(go, controller))
        {
            go.transform.SetParent(pool.PoolViewer.transform);
            go.SetActive(false);
            return go;
        }
        return null;
    }

    private void PoolGetVE(GameObject go)
    {
        go.SetActive(true);
            
    }

    private void PoolRecyleVE(GameObject go)
    {
        go.SetActive(false);
    }

    private void PoolDestroyAudioEmitter(GameObject go)
    {
        VEControllers.Remove(go);   
        GameObject.Destroy(go);
    }
}