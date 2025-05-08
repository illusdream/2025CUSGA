using System.Collections.Generic;
using ilsFramework;
using Unity.Mathematics;
using UnityEngine;
using Utils;

public class ExplosionVE: BaseVisualEffectPool<ExplosionVEConfig>
{
 
    public const string PoolName = "ExplosionVE";

    public Dictionary<GameObject, ExplosionController> VEControllers;
    
    public override void InitPool()
    {
        VEControllers = new Dictionary<GameObject, ExplosionController>();
        
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

    public void TryEmittingVE(Vector2 center,Vector2? scale = null)
    {
        var instance = pool.Get();
        if (instance)
        {
            instance.transform.position = center;
            instance.transform.localScale = scale.GetValueOrDefault(Vector2.one);
        }
    }
    
    
    
    private GameObject CreateVE(GameObjectPool pool)
    {
        var go = GameObject.Instantiate(Config.ExplosionVEPrefab);

        if (go && go.TryGetComponent<ExplosionController>(out var controller) && VEControllers.TryAdd(go, controller))
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