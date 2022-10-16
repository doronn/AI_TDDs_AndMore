using System;
using System.Collections.Generic;
using Shooter;
using UnityEngine;

namespace Sensory
{
    [RequireComponent(typeof(Collider))]
    public class DangerSensor : MonoBehaviour, ISensor
    {
        [SerializeField]
        private SphereCollider _collider;
        private ISensorListener _sensorListener;
        private int _ownerId;

        public void RegisterListener(ISensorListener sensorListener, int ownerId)
        {
            _sensorListener = sensorListener;
            _sensorListener.SensoryData = new float[6];
            _ownerId = ownerId;
        }

        // private Dictionary<Collider, IdableActor> _knownCollidersSet = new();
        private Dictionary<Type, HashSet<Collider>> _knownCollidersSet =
            new()
            {
                { typeof(ICharacter), new HashSet<Collider>() },
                { typeof(IDamageGiver), new HashSet<Collider>() },
                { typeof(IObstacle), new HashSet<Collider>() }
            };

        private void NotifySensorListener()
        {
            if (_sensorListener == null)
            {
                return;
            }

            var sensorIndex = 0;
            var sensorPosition = transform.position;
            var maxSensoryDistance = _collider.radius;
            foreach (var damageGiver in _knownCollidersSet)
            {
                var minimumDistance = maxSensoryDistance;
                var angleForDistance = 0f;
                var colliders = damageGiver.Value;
                foreach (var damageGiverKey in colliders)
                {
                    if (!damageGiverKey)
                    {
                        continue;
                    }

                    var closestCollisionPoint = damageGiverKey.ClosestPoint(sensorPosition);
                    var targetDirection = closestCollisionPoint - sensorPosition;
                    var distance = Vector3.Magnitude(targetDirection);
                    if (distance < minimumDistance)
                    {
                        minimumDistance = distance;
                        angleForDistance = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
                    }
                }

                if (_sensorListener.SensoryData.Length > sensorIndex + 1)
                {
                    _sensorListener.SensoryData[sensorIndex++] = 1f - (minimumDistance / maxSensoryDistance);
                    _sensorListener.SensoryData[sensorIndex++] = angleForDistance / 180f;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IdableActor>(out var otherIdableActor) || otherIdableActor.Id == _ownerId)
            {
                return;
            }

            var type = otherIdableActor switch
            {
                ICharacter => other.isTrigger ? null : typeof(ICharacter),
                IDamageGiver => typeof(IDamageGiver),
                _ => typeof(IObstacle)
            };

            if (type == null)
            {
                return;
            }
            
            if (_knownCollidersSet.ContainsKey(type))
            {
                _knownCollidersSet[type].Add(other);
            }
            else
            {
                _knownCollidersSet[type] = new HashSet<Collider> { other };
            }

            NotifySensorListener();
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IdableActor>(out var otherIdableActor) || otherIdableActor.Id == _ownerId)
            {
                return;
            }
            
            var type = otherIdableActor switch
            {
                ICharacter => other.isTrigger ? null : typeof(ICharacter),
                IDamageGiver => typeof(IDamageGiver),
                _ => typeof(IObstacle)
            };

            if (type == null)
            {
                return;
            }

            _knownCollidersSet[type]?.Remove(other);

            NotifySensorListener();
        }
    }
}