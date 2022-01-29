using UnityEngine;
using UnityEngine.UI;

namespace ChaosMission
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetCurrentHealth(int health)
        {
            _slider.value = health;
        }

        public void SetMaxHealth(int health)
        {
            _slider.maxValue = health;
        }
    }
}
