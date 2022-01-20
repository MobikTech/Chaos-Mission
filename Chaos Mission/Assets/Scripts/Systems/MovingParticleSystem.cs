using ChaosMission.Player;
using ChaosMission.Player.MovingStates;
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
            
            foreach (ParticleSystem walkingParticleSystem in _movingParticleSystems)
            {
                walkingParticleSystem.gameObject.transform.parent.TryGetComponent(out MoveController moveController);
                
                moveController.MovingStates[MovingState.Walking].StateStartedAction += () =>
                {
                    if (moveController.MovingStates[MovingState.Jumping].IsActive)
                    {
                        return;
                    }
                    walkingParticleSystem.Play();
                };
                
                moveController.MovingStates[MovingState.Walking].StateStoppedAction += () => walkingParticleSystem.Stop();

                moveController.MovingStates[MovingState.Jumping].StateStartedAction += () => walkingParticleSystem.Stop();
                
                moveController.MovingStates[MovingState.Jumping].StateStoppedAction += () =>
                {
                    if ( moveController.MovingStates[MovingState.Walking].IsActive)
                    {
                        walkingParticleSystem.Play();
                    }
                };
            }
        }
        
        private void InitJumpingParticleSystems()
        {
            foreach (ParticleSystem jumpingParticleSystem in _jumpingParticleSystems)
            {
                jumpingParticleSystem.gameObject.transform.parent.TryGetComponent(out MoveController moveController);
            
                moveController.MovingStates[MovingState.Jumping].StateStartedAction += () => jumpingParticleSystem.Play();
                
                // moveController.MovingStates[MovingState.Jumping].StateStoppedAction += () => jumpingParticleSystem.Stop();
            }
        }
    }
}