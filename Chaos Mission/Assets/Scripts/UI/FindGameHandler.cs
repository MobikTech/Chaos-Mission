using ChaosMission.Networking;
using TMPro;
using UnityEngine;

namespace ChaosMission.UI
{
    public sealed class FindGameHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private NetworkSystem _networkSystem;

        private const string DefaultAddress = "127.0.0.1:8888"; 

        public void TryConnectToServer()
        {
            string connectionAddress = _inputField.text.Length <= 0 ? DefaultAddress : _inputField.text;
            
            _networkSystem.TryConnectToAddress(connectionAddress);

        }
    }
}