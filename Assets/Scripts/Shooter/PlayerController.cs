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
        private Camera _mainCamera;

        private void Awake()
        {
            if (_character != null)
            {
                _character.Init(controllerId, OnDied, OnHit);
                _isInitialized = true;
                UpdateHealthText();
            }
            _mainCamera = Camera.main;
        }

        private void OnHit()
        {
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            _character._scoreText.SetText(_character.Health.ToString());
        }

        private void OnDied()
        {
            _isInitialized = false;
        }

        private void FixedUpdate()
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

            var mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var lookAtRelativePosition = mouseRay.GetPoint((_mainCamera.transform.position -
                                                            _character.transform.position).magnitude);
            _character.LookAt(lookAtRelativePosition);
            
            if (Input.GetMouseButton(0))
            {
                _character.Shoot(OnHitTarget);
            }
        }

        private void OnHitTarget(int hit)
        {
            if (hit != 2)
            {
                return;
            }

            if (!_character)
            {
                return;
            }
            
            _character.GiveHealthBump();
            UpdateHealthText();
        } 
    }
}