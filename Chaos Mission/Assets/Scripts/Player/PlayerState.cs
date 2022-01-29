using UnityEngine;

namespace ChaosMission.Player
{
    public class PlayerState : MonoBehaviour
    {
        private Health _health;
        [SerializeField] private int _maxhelth;

        private void Awake()
        {
            _health = new Health(_maxhelth);
            _health.Die += () =>
            {
                Destroy(gameObject);
            };
        }

        public int GetCurrentHealth()
        {
            return _health.CurrentHealth;
        }

        public int GetMaxHealth()
        {
            return _maxhelth;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();

            HealDealer healDealer = other.GetComponent<HealDealer>();

            if (damageDealer != null)
            {
                _health.TakeDamage(damageDealer.GetDamage());
            }

            if (healDealer != null)
            {
                _health.Heal(healDealer.GetHeal());
            }
        }
    }
}
