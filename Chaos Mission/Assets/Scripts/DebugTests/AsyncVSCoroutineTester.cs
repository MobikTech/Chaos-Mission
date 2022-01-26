using System.Collections;
using System.Threading.Tasks;
using ChaosMission.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.DebugTests
{
    public class AsyncVSCoroutineTester : MonoBehaviour
    {
        [SerializeField] private bool _showFixedUpdateLog = false;
        private bool _isAlive = false;
        
        private int _frameCounterAsync = 0;
        private int _frameCounterCoroutine = 0;


        private void FixedUpdate()
        {
            if (_showFixedUpdateLog && _isAlive)
            {
                Debug.Log("FIXED UPDATE");
            }
        }

        private void Update()
        {
            UnityTimeHelper.GetMillisecondsToNextFixedUpdate();

            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                Debug.Log("TEST STARTED");
                ResetState();
                StartCoroutine(TestCoroutine());
                TestAsync();
            }
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                _isAlive = false;
            }
        }

        private void ResetState()
        {
            _isAlive = true;
            _frameCounterAsync = 0;
            _frameCounterCoroutine = 0;
        }

        private IEnumerator TestCoroutine()
        {
            Debug.Log("COROUTINE STARTED");
            
            WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
            
            while (_isAlive)
            {
                _frameCounterCoroutine++;
                Debug.Log($"COROUTINE - {_frameCounterCoroutine}");
                
                yield return fixedUpdate;
            }
            
            Debug.Log("COROUTINE STOPPED");
        }
        
        private async void TestAsync()
        {
            Debug.Log("ASYNC METHOD STARTED");

            while (_isAlive)
            {
                _frameCounterAsync++;
                Debug.Log($"ASYNC - {_frameCounterAsync}");
                
                await Task.Delay(45);
                int msecs = UnityTimeHelper.GetMillisecondsToNextFixedUpdate();
                await Task.Delay(msecs);
            }
            
            Debug.Log("ASYNC METHOD STOPPED");
        }

        private void OnDestroy()
        {
            _isAlive = false;
        }
    }
}