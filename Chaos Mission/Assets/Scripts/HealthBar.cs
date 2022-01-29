using UnityEngine;
using UnityEngine.UI;

namespace ChaosMission
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetCurrentNormalizedHealth(float health)
        {
            _slider.value = health;
        }
    }
}
