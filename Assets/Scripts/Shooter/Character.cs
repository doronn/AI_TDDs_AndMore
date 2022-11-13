using System;
using TMPro;
using UnityEngine;

namespace Shooter
{
    public class Character : MonoBehaviour, ICharacter, IIdableActor
    {
        [SerializeField]
        private Rigidbody _rb;

        [SerializeField]
        private CharacterConfiguration _characterConfiguration;

        [SerializeField]
        private Transform _rotationTransform;
        
        [field: SerializeField]
        public float Health { get; private set; }

        public TMP_Text _scoreText;
        
        public int Id { get; private set; }

        private Action OnDeath = null;
        private Action OnHit = null;
        private Action GotBadCollision = null;

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
            else if (amount > 0)
            {
                OnHit?.Invoke();
            }
        }

        public void GiveHealthBump()
        {
            Health += _characterConfiguration.HealthBonusForKill;
        }

        public void ReportGotBadCollision()
        {
            GotBadCollision?.Invoke();
        }

        public void Init(int id, Action onDied, Action onHit = null, Action gotBadCollision = null)
        {
            Id = id;
            OnDeath = onDied;
            OnHit = onHit;
            GotBadCollision = gotBadCollision;
            Health = _characterConfiguration.InitialHealth;
        }
        
        public bool Shoot(Action<int> onHitTarget = null)
        {
            var timeNow = Time.time;
            if (timeNow - _lastShotTakenTime < _characterConfiguration.ShootingDelay)
            {
                return false;
            }

            _lastShotTakenTime = timeNow;
            var forward = _rotationTransform.forward;
            var projectile = Instantiate(_characterConfiguration.ProjectilePrefab, _rotationTransform.position + forward, Quaternion.identity);
            projectile.Init(Id, forward, onHitTarget);
            return true;
        }

        public void SetRotation(float worldAngle)
        {
            _rotationTransform.Rotate(0, worldAngle, 0, Space.Self);
        }

        public void LookAt(Vector3 lookAt)
        {
            _rotationTransform.LookAt(lookAt);
        }
        
        public void RotateRight()
        {
            _rotationTransform.Rotate(Vector3.up, _characterConfiguration.RotationSpeed * Time.deltaTime,Space.Self);
        }
        
        public void RotateLeft()
        {
            _rotationTransform.Rotate(Vector3.up, -_characterConfiguration.RotationSpeed * Time.deltaTime,Space.Self);
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

        private void FixedUpdate()
        {
            var normalizedDirection = _nextMoveDirection.normalized;

            var currentDeltaTime = Time.fixedDeltaTime;
            var xDirection = normalizedDirection.x;
            var zDirection = normalizedDirection.z;

            var xMoveAmount = currentDeltaTime * xDirection *
                              normalizedDirection.z switch
                              {
                                  > 0 => _characterConfiguration.ForwardSpeed,
                                  < 0 => _characterConfiguration.BackwardSpeed,
                                  _ => _characterConfiguration.StrafeSpeed
                              };
            var zMoveAmount = currentDeltaTime *
                              (zDirection > 0 ? zDirection * _characterConfiguration.ForwardSpeed : zDirection * _characterConfiguration.BackwardSpeed);
            // transform.position += transform.forward * zMoveAmount + transform.right * xMoveAmount;
            var currentForwardVector = _rotationTransform.forward;
            currentForwardVector.y = 0;
            var currentRightVector = _rotationTransform.right;
            currentForwardVector.y = 0;
            
            _rb.velocity = currentForwardVector.normalized * zMoveAmount + currentRightVector.normalized * xMoveAmount;
            _nextMoveDirection = Vector3.zero;
        }
    }
}