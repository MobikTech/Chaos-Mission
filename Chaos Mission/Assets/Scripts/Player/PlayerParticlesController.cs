// using ChaosMission.Player.Moving.Controller;
// using UnityEngine;
//
// namespace ChaosMission.Player
// {
//     [RequireComponent(typeof(MoveController))]
//     public class PlayerParticlesController : MonoBehaviour
//     {
//         [SerializeField] private ParticleSystem[] _movingParticleSystems;
//         [SerializeField] private ParticleSystem[] _jumpingParticleSystems;
//
//         private MoveController _moveController; 
//             
//         private void Start()
//         {
//             _moveController = GetComponent<MoveController>();
//             InitMovingParticleSystems();
//             InitJumpingParticleSystems();
//         }
//
//         private void InitMovingParticleSystems()
//         {
//             
//             foreach (ParticleSystem walkingParticleSystem in _movingParticleSystems)
//             {
//                 // walkingParticleSystem.gameObject.transform.parent.TryGetComponent(out MoveController moveController);
//                 
//                 _moveController.MovingStates[MovingStateType.Walking].StateStartedAction += () =>
//                 {
//                     if (_moveController.MovingStates[MovingStateType.Jumping].IsActive)
//                     {
//                         return;
//                     }
//                     walkingParticleSystem.Play();
//                 };
//                 
//                 _moveController.MovingStates[MovingStateType.Walking].StateStoppedAction += () => walkingParticleSystem.Stop();
//
//                 _moveController.MovingStates[MovingStateType.Jumping].StateStartedAction += () => walkingParticleSystem.Stop();
//                 
//                 _moveController.MovingStates[MovingStateType.Jumping].StateStoppedAction += () =>
//                 {
//                     if ( _moveController.MovingStates[MovingStateType.Walking].IsActive)
//                     {
//                         walkingParticleSystem.Play();
//                     }
//                 };
//             }
//         }
//         
//         private void InitJumpingParticleSystems()
//         {
//             foreach (ParticleSystem jumpingParticleSystem in _jumpingParticleSystems)
//             {
//                 // jumpingParticleSystem.gameObject.transform.parent.TryGetComponent(out MoveController moveController);
//             
//                 _moveController.MovingStates[MovingStateType.Jumping].StateStartedAction += () => jumpingParticleSystem.Play();
//                 
//                 // moveController.MovingStates[MovingState.Jumping].StateStoppedAction += () => jumpingParticleSystem.Stop();
//             }
//         }
//     }
// }