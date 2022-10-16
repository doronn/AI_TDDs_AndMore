using System;
using UnityEngine;

namespace Shooter
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private int controllerId;

        [SerializeField]
        private Character _character;

        private bool _isInitialized = false;

        private void Awake()
        {
            if (_character != null)
            {
                _character.Init(controllerId, OnDied);
                _isInitialized = true;
            }
        }

        private void OnDied()
        {
            _isInitialized = false;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
            
            if (Input.GetKey(KeyCode.W))
            {
                _character.WalkForward();
            }
            if (Input.GetKey(KeyCode.A))
            {
                _character.StrafeLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                _character.StrafeRight();
            }
            if (Input.GetKey(KeyCode.S))
            {
                _character.WalkBackward();
            }
            if (Input.GetKey(KeyCode.Q))
            {
                _character.RotateLeft();
            }
            if (Input.GetKey(KeyCode.E))
            {
                _character.RotateRight();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _character.Shoot();
            }
        }
    }
}