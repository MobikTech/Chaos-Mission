using System.Net.Sockets;
using Serializer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.Networking
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        private Socket _serverSocket;
        
        private void Awake()
        {
            _player = GetComponent<Transform>();
           
            
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                MessageInfo _messageInfo = new MessageInfo()
                {
                    X = _player.position.x
                };

                byte[] message = MessageSerializer.Serialize(_messageInfo);
                
            }
        }
    }
}