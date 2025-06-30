using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public sealed class Player : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int _initialHealth = 100;
        [SerializeField, Range(1, 100)] private int _maxHealth = 100;
        [SerializeField] private HealthView _healthView;
        private Health _health;

        private void Awake()
        {
            _health = new Health(_initialHealth, _maxHealth);
            _healthView.SetHealthController(_health);
        }

        public void DamagePlayer(int amount) => _health.TakeDamage(amount);

        public void HealPlayer(int amount) => _health.Heal(amount);
    }
}