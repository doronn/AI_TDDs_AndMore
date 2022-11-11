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

        [SerializeField]
        private Rigidbody _rb;
        
        private Vector3 _directionVector;

        internal void Init(int ownerId, double direction, Action<int> onHitTarget = null)
        {
            OwnerId = ownerId;
            _directionVector = new Vector3((float)Math.Cos(direction), 0, (float)Math.Sin(direction));
            _damageGiver = GetComponent<DamageGiver>();
            _damageGiver.Init(ownerId, onHitTarget, DestroySelf);
            _rb.velocity = _directionVector * _moveSpeed;
            
            Destroy(gameObject, _maxDistance / _moveSpeed);
        }

        private void DestroySelf()
        {
            /*if (!this || !gameObject)
            {
                return;
            }*/

            Destroy(gameObject);
        }
    }
}