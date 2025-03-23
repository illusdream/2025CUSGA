public interface IEntityHealth : IHitable
{
        public int GetMaxHealth();
        
        public float GetCurrentHealth();

        public float GetHealthPercent();
        
        public bool TryAddHealthSource(EHealthSourceType healthSourceType,HealthSource healthSource);
        
        public bool RemoveHealthSource(EHealthSourceType healthSourceType);
        
        public bool TryGetHealthSource(EHealthSourceType healthSourceType, out HealthSource healthSource);
        
        public bool ContainsHealthSource(EHealthSourceType healthSourceType);
}