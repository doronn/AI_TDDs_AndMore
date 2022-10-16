using System;
using UnityEngine;

namespace Shooter
{
    [RequireComponent(typeof(DamageGiver))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private DamageGiver _damageGiver;
     
        public int OwnerId { get; private set; }

        [SerializeField]
        private float _moveSpeed;
        
        [SerializeField]
        private float _maxDistance;
        
        private Vector3 _directionVector;
        private float _totalCoveredDistance;

        internal void Init(int ownerId, double direction, Action onHitTarget = null)
        {
            OwnerId = ownerId;
            _directionVector = new Vector3((float)Math.Cos(direction), 0, (float)Math.Sin(direction));
            _damageGiver = GetComponent<DamageGiver>();
            _damageGiver.Init(ownerId, onHitTarget, DestroySelf);
        }
        
        private void Update()
        {
            var speedCoef =  _moveSpeed * Time.deltaTime;
            transform.position += _directionVector * speedCoef;

            _totalCoveredDistance += speedCoef;

            if (_totalCoveredDistance >= _maxDistance)
            {
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            if (!this || !gameObject)
            {
                return;
            }

            Destroy(gameObject);
        }
    }
}