using UnityEngine;

namespace ChaosMission
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health = 50;
        [SerializeField] private int _maxHealth = 100;

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();

            HealDealer healDealer = other.GetComponent<HealDealer>();

            if (damageDealer != null)
            {
                TakeDamage(damageDealer.GetDamage());
            }

            if (healDealer != null)
            {
                Heal(healDealer.GetHeal());
            }
        }

        private void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Heal(int heal)
        {
            if (_health == _maxHealth) return;
            
            if (_health + heal < _maxHealth)
            {
                _health += heal;
            }
            else
            {
                _health = _maxHealth;
            }
        }
        

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
