using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChaosMission.UI
{
    public sealed class FindGameHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        public void TryConnectToServer()
        {
            string address = _inputField.text;
            Debug.Log(address);

        }
    }
}