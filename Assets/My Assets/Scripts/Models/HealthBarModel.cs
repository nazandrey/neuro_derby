namespace Scripts.Models
{
    public class HealthBarModel
    {
        public HealthBarModel(float maxHealth = 100)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public float HealthPercentage => CurrentHealth/MaxHealth;
        public float MaxHealth { get; }
        public float CurrentHealth { get; private set; }

        public void SubtractHealth(float healthToSubtract)
        {
            CurrentHealth -= healthToSubtract;
        }
    }
}