using System;

namespace ChaosMission
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public readonly int MaxHealth;
        public Action Die;
        public Action<int> DisplayHealth;

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            DisplayHealth?.Invoke(CurrentHealth);
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

            DisplayHealth?.Invoke(CurrentHealth);
        }

        public Health(int maxHealth) : this(maxHealth, maxHealth)
        {
            
        }

        public Health(int maxHealth, int currentCurrentHealth)
        {
            CurrentHealth = currentCurrentHealth;
            MaxHealth = maxHealth;
        }
        
        
    }
}
