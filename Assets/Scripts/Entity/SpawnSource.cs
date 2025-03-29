using UnityEngine;

/// <summary>
/// 生成源信息
/// </summary>
public struct SpawnSource
{
        /// <summary>
        /// 生成者ID，
        /// </summary>
        public EntityID SpawnerID;
        
        /// <summary>
        /// 是否是系统生成的
        /// </summary>
        public bool GenerateBySystem;

        /// <summary>
        /// 生成时间，采用的是被time.scale影响的时间
        /// </summary>
        public float SpawnTime;

        /// <summary>
        /// 生成位置
        /// </summary>
        public Vector2 SpawnPosition;

        public bool NotVoid;
        
        public static SpawnSource SystemGenerate => new SpawnSource() { SpawnerID = EntityID.Empty, GenerateBySystem = true, SpawnTime = Time.time };

        public static SpawnSource SpawnBySystem(Vector2 spawnPosition)
        {
                return new SpawnSource()
                {
                        SpawnerID = EntityID.Empty, GenerateBySystem = true, SpawnTime = Time.time,
                        SpawnPosition = spawnPosition,
                };
        }
        
        public static SpawnSource SpawnByEntity(EntityID id, Vector2 spawnPosition)
        {
                return new SpawnSource() { SpawnerID = id, SpawnPosition = spawnPosition ,SpawnTime = Time.time };
        }

        public override string ToString()
        {
                if (!NotVoid)
                {
                        return "Void SpawnSource";
                }

                if (GenerateBySystem)
                {
                        return "System SpawnSource";
                }
                return $"SpawnBy: {SpawnerID}, SpawnTime: {SpawnTime}, SpawnPosition: {SpawnPosition}";
        }
}