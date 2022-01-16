using System.Security.AccessControl;
using UnityEngine;

namespace ChaosMission.Systems
{
    public class MovingParticleSystem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _movingParticleSystems;
        [SerializeField] private ParticleSystem[] _jumpingParticleSystems;

        private void Awake()
        {
            InitMovingParticleSystems();
            InitJumpingParticleSystems();
        }

        private void InitMovingParticleSystems()
        {
            foreach (ParticleSystem movingParticleSystem in _movingParticleSystems)
            {
                movingParticleSystem.Stop();
                Movable movable = movingParticleSystem.gameObject.GetComponentInParent<Movable>();
                movingParticleSystem.gameObject.transform.parent.TryGetComponent(out Jumpable jumpable);
                
                movable.MovingStarted += () =>
                {
                    if (jumpable == null || jumpable.OnJumping)
                    {
                        return;
                    }
                    movingParticleSystem.Play();
                };
                movable.MovingStopped += () => movingParticleSystem.Stop();

                if (jumpable == null)
                {
                    continue;
                }
                
                jumpable.StartJumping += () => movingParticleSystem.Stop();
                jumpable.StopJumping += () =>
                {
                    if (movable.OnMoving)
                    {
                        movingParticleSystem.Play();
                    }
                };

            }
        }
        
        private void InitJumpingParticleSystems()
        {
            foreach (ParticleSystem jumpingParticleSystem in _jumpingParticleSystems)
            {
                jumpingParticleSystem.Stop();
                jumpingParticleSystem.gameObject.transform.parent.TryGetComponent(out Jumpable jumpable);
            
                jumpable.StartJumping += () => jumpingParticleSystem.Play();
                jumpable.StopJumping += () => jumpingParticleSystem.Stop();

            }
        }
    }
}