using System;
using TMPro;
using UnityEngine;

namespace ChaosMission.UI
{
    public sealed class FindGameHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField = null!;

        private const string DefaultAddress = "127.0.0.1:8888";

        private void Start()
        {
            _inputField.text = DefaultAddress;
        }

        public void TryConnectToServer()
        {
            string connectionAddress = _inputField.text.Length <= 0 ? DefaultAddress : _inputField.text;

            throw new NotImplementedException();
        }
    }
}