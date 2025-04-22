using System;

public interface IBuffContainer
{
        public bool TryAddBuff(string buffName,Type buffType);
        
        public bool RemoveBuff(string buffName);
        
        public bool HasBuff(string buffName);
}