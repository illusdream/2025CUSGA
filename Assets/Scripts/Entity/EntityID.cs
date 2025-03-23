using System;
using ilsFramework;

public struct EntityID : IEquatable<EntityID>
{
        public int ID;
        public EEntityType EntityType;
        public bool IsEmpty;
        
        public static EntityID Empty = new EntityID() { IsEmpty = true};

        public override bool Equals(object obj)
        {
                if (obj is EntityID otherID)
                {
                        return Equals(otherID);
                }
                return false;
        }
        
        public override int GetHashCode()
        {
                return HashCode.Combine(ID, (int)EntityType, IsEmpty);
        }

        public static bool operator ==(EntityID a, EntityID b)
        {
                return a.Equals(b);
        }
        public static bool operator !=(EntityID a, EntityID b)
        {
                return !a.Equals(b);
        }

        public bool Equals(EntityID other)
        {
                if(IsEmpty && other.IsEmpty)
                        return true;
                if(IsEmpty || other.IsEmpty)
                        return false;
                var result = (other.EntityType == EntityType && ID == other.ID);
                return result;
        }

        public override string ToString()
        {
                return $"EntityID: {ID} EntityType:{EntityType},IsEmpty:{IsEmpty}";
        }
}