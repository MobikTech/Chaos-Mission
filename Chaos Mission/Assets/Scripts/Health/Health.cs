using System;

namespace ChaosMission.Health
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public readonly int MaxHealth;
        public Action? Die;
        public Action<float>? DisplayHealth;

        public float GetCurrentNormalizedHealth()
        {
            return (float) CurrentHealth / MaxHealth;
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            DisplayHealth?.Invoke(GetCurrentNormalizedHealth());
            if (CurrentHealth <= 0)
            {
                Die?.Invoke();
            }
        }

        public void Heal(int heal)
        {
            if (CurrentHealth == MaxHealth) return;
            
            if (CurrentHealth + heal < MaxHealth)
            {
                CurrentHealth += heal;
            }
            else
            {
                CurrentHealth = MaxHealth;
            }

            DisplayHealth?.Invoke(GetCurrentNormalizedHealth());
        }

        public Health(int maxHealth) : this(maxHealth, maxHealth)
        {
            
        }

        private Health(int maxHealth, int currentCurrentHealth)
        {
            CurrentHealth = currentCurrentHealth;
            MaxHealth = maxHealth;
        }
        
        
    }
}
