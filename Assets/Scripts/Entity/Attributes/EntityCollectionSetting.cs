using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class EntityCollectionSetting : Attribute
{
        public EEntityType EntityType { get; set; }

        public EntityCollectionSetting(EEntityType entityType)
        {
                EntityType = entityType;
        }
}