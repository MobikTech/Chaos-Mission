using UnityEngine;

namespace ChaosMission.Player.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class GeneralParticleHandler : MonoBehaviour
    {
        protected ParticleSystem _particleSystem = null!;

        protected virtual void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            SubscribeActions();
        }

        protected abstract void SubscribeActions();
    }
}