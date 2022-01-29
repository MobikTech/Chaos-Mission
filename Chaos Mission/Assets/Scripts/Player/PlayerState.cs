using UnityEngine;

namespace ChaosMission.Player
{
    public class PlayerState : MonoBehaviour
    {
        private Health _health;
        [SerializeField] private int _maxhelth;
        [SerializeField] private HealthBar _healthBar;

        private void Awake()
        {
            _health = new Health(_maxhelth);
            _health.Die += () =>
            {
                Destroy(gameObject);
            };
            _healthBar.SetMaxHealth(_maxhelth);
            _healthBar.SetCurrentHealth(_maxhelth);
            _health.DisplayHealth += _healthBar.SetCurrentHealth;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();

            HealDealer healDealer = other.GetComponent<HealDealer>();

            if (damageDealer != null)
            {
                _health.TakeDamage(damageDealer.GetDamage());
                _healthBar.SetCurrentHealth(_health.CurrentHealth);
            }

            if (healDealer != null)
            {
                _health.Heal(healDealer.GetHeal());
            }
        }
    }
}
