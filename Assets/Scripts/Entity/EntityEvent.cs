using System;

public class EntityEvent
{
        public const string EntityBeHitted = "EntityBeHitted";

        public class EntityBeHittedEventArgs : EventArgs
        {
                public DamageInfo damageInfo;

                public EntityBeHittedEventArgs(DamageInfo damageInfo)
                {
                        this.damageInfo = damageInfo;
                }
        }
}