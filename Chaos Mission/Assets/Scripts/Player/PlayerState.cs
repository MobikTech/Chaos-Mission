using ChaosMission.Health;
using UnityEngine;

namespace ChaosMission.Player
{
    public class PlayerState : MonoBehaviour
    {
        private Health.Health _health = null!;
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private HealthBar _healthBar = null!;

        private void Awake()
        {
            _health = new Health.Health(_maxHealth);
            _health.Die += () =>
            {
                Destroy(gameObject);
            };
            _healthBar.SetCurrentNormalizedHealth(_health.GetCurrentNormalizedHealth());
            _health.DisplayHealth += _healthBar.SetCurrentNormalizedHealth;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();

            HealDealer healDealer = other.GetComponent<HealDealer>();

            if (damageDealer != null)
            {
                _health.TakeDamage(damageDealer.GetDamage());
                _healthBar.SetCurrentNormalizedHealth(_health.CurrentHealth);
            }

            if (healDealer != null)
            {
                _health.Heal(healDealer.GetHeal());
            }
        }
    }
}
