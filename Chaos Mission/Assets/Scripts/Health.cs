using System;

namespace ChaosMission
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public readonly int MaxHealth;
        public Action Die;

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
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
