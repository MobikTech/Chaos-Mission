using UnityEngine;
using UnityEngine.UI;

namespace ChaosMission.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider = null!;

        public void SetCurrentNormalizedHealth(float health)
        {
            _slider.value = health;
        }
    }
}
