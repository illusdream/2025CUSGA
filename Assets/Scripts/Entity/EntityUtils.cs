using UnityEngine;

public static class Entity
{
        public static GameObject Instantiate(GameObject prefab,SpawnSource spawnSource ,Vector3 position, Quaternion rotation)
        {
             return EntityManager.Instance.Instantiate(prefab,spawnSource,position,rotation);
        }
}