using UnityEngine;

namespace ChaosMission.Health
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        
        public int GetDamage()
        {
            return _damage;
        }
    }
}
