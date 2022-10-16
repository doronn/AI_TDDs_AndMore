using System;
using TMPro;
using UnityEngine;

namespace Shooter
{
    public class Character : MonoBehaviour, ICharacter, IdableActor
    {
        [SerializeField]
        private Projectile _projectilePrefab;

        [SerializeField]
        private float _rotationSpeed;
        [SerializeField]
        private float _forwardSpeed;
        
        [SerializeField]
        private float _backwardSpeed;
        
        [SerializeField]
        private float _strafeSpeed;
        
        [SerializeField]
        private float _shootingDelay;
        
        [field: SerializeField]
        public float Health { get; private set; }

        public TMP_Text _scoreText;
        
        public int Id { get; private set; }

        private Action OnDeath = null;
        private Action OnHit = null;

        private float _lastShotTakenTime = 0;

        private Vector3 _nextMoveDirection = Vector3.zero;
        
        public void ReceiveDamage(float amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                OnDeath?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                OnHit?.Invoke();
            }
        }

        public void Init(int id, Action onDied, Action onHit = null)
        {
            Id = id;
            OnDeath = onDied;
            OnHit = onHit;
        }
        
        public bool Shoot(Action onHitTarget = null)
        {
            var timeNow = Time.time;
            if (timeNow - _lastShotTakenTime < _shootingDelay)
            {
                return false;
            }

            _lastShotTakenTime = timeNow;
            var projectile = Instantiate(_projectilePrefab, transform.position + transform.forward, Quaternion.identity);
            projectile.Init(Id, (0.25 - transform.eulerAngles.y / 360f) * 2 * Math.PI, onHitTarget);
            return true;
        }

        public void SetRotation(float worldAngle)
        {
            transform.Rotate(Vector3.up, worldAngle, Space.World);
        }
        
        public void RotateRight()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime,Space.Self);
        }
        
        public void RotateLeft()
        {
            transform.Rotate(Vector3.up, -_rotationSpeed * Time.deltaTime,Space.Self);
        }

        public void WalkForward()
        {
            _nextMoveDirection.z += 1;
        }

        public void WalkBackward()
        {
            _nextMoveDirection.z += -1;
        }

        public void StrafeRight()
        {
            _nextMoveDirection.x += 1;
        }

        public void StrafeLeft()
        {
            _nextMoveDirection.x += -1;
        }

        private void Update()
        {
            var normalizedDirection = _nextMoveDirection.normalized;

            var currentDeltaTime = Time.deltaTime;
            var xDirection = normalizedDirection.x;
            var zDirection = normalizedDirection.z;

            var xMoveAmount = currentDeltaTime * xDirection *
                              normalizedDirection.z switch
                              {
                                  > 0 => _forwardSpeed,
                                  < 0 => _backwardSpeed,
                                  _ => _strafeSpeed
                              };
            var zMoveAmount = currentDeltaTime *
                              (zDirection > 0 ? zDirection * _forwardSpeed : zDirection * _backwardSpeed);

            transform.position += transform.forward * zMoveAmount + transform.right * xMoveAmount;
            
            _nextMoveDirection = Vector3.zero;
        }
    }
}