using ChaosMission.Networking;
using TMPro;
using UnityEngine;

namespace ChaosMission.UI
{
    public sealed class FindGameHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        // [SerializeField] private NetworkClient _networkClient;

        public void TryConnectToServer()
        {
            string address = _inputField.text;
            Debug.Log(address);

        }
    }
}