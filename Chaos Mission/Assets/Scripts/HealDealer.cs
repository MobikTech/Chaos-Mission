using UnityEngine;

namespace ChaosMission
{
    public class HealDealer: MonoBehaviour
    {
        [SerializeField] private int _heal = 1;

        public int GetHeal()
        {
            return _heal;
        }
    }
}
