using System;

namespace Player
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        public bool IsAlive => CurrentHealth > 0;

        public event Action<int> OnHealthChanged;

        public Health(int currentHealth, int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        }

        public void TakeDamage(int damage)
        {
            if (!IsAlive || damage <= 0) return;

            CurrentHealth -= damage;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
            
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void Heal(int amount)
        {
            if (!IsAlive || amount <= 0) return;

            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
            
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}