using UnityEngine;

namespace ChaosMission
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
