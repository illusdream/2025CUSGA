public class PlayerHealth : EntityComponent,IEntityHealth
{
    public int CurrectMaxHealth { get; set; }
    
    public int BaseMaxHealth { get; set; }
    
    public float CurrentHealth { get; set; }
    
    
    public int GetMaxHealth()
    {
        return CurrectMaxHealth;
    }

    public float GetCurrentHealth()
    {
        
        return CurrentHealth;
    }

    public float GetHealthPercent()
    {
        
        return CurrentHealth / BaseMaxHealth;
    }
}