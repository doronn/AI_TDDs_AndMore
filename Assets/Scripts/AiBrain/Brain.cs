using System;
using System.Collections.Generic;
using System.Linq;
using Sensory;
using Shooter;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AiBrain
{
    public class Brain : MonoBehaviour
    {
        private const string SCORE_TEXT_FORMAT = "{0}\n{1}";
        private static int _bestId = -1;
        private static int _networkIndex = 100;
        [SerializeField] private DiscreteSensor[] _sensors;
        [SerializeField] private Character _controlledCharacter;
        public NeuralNetwork Network { get; private set; }

        public Transform CharacterTransform => _controlledCharacter != null ? _controlledCharacter.transform : null;
        public int TotalScore
        {
            get => _totalScore;
            private set
            {
                _totalScore = value;

                UpdateCharacterScoreText();
            }
        }

        private void UpdateCharacterScoreText()
        {
            if (_controlledCharacter)
            {
                _controlledCharacter._scoreText.SetText(string.Format(SCORE_TEXT_FORMAT, _totalScore.ToString(),
                    _controlledCharacter.Health.ToString()));
            }
        }

        private List<SensorListener> _sensorListeners = new();

        private bool _isInitialized = false;
        [SerializeField]
        private int _totalScore = 100;

        private int _sensorCount;
        private float _stimulationInput = 0f;
        private float _mutationAmount = 0f;
        private float[] _sensoryDataBuffer;

        public Brain()
        {
        }

        public void Init(NeuralNetwork predefinedNetwork = null, float brainConfigMutationAmount = 0, int score = 100, bool isMutated = false)
        {
            if (_sensors == null || _sensors.Length == 0)
            {
                throw new Exception("missing sensor");
            }

            _networkIndex = Guid.NewGuid().GetHashCode();
            _mutationAmount = brainConfigMutationAmount;
            
            _sensorCount = _sensors.Sum(sensor => sensor.SensorCount);
            var neuronCounts = new[] { _sensorCount + 1, _sensorCount / 2, _sensorCount, 4 };
            
            Network = predefinedNetwork ?? new NeuralNetwork(_networkIndex, neuronCounts);
            
            if (predefinedNetwork != null)
            {
                _totalScore = score;
                NeuralNetwork.TransformNetworkNeurons(Network, neuronCounts);
                if (isMutated && brainConfigMutationAmount > 0)
                {
                    Network.NetworkIndex = _networkIndex;
                }
            }
            
            foreach (var sensor in _sensors)
            {
                var sensorListener = new SensorListener();
                _sensorListeners.Add(sensorListener);
                sensor.RegisterListener(sensorListener, Network.NetworkIndex);
            }
            
            _sensoryDataBuffer = new float[_sensorCount + 1];
            
            _controlledCharacter.Init(Network.NetworkIndex, OnDied, OnHit, GotBadCollision);
            _controlledCharacter.transform.localPosition += Vector3.forward * Random.Range(-50f, 50f) + Vector3.right * Random.Range(-10f, 10f);
            
            _isInitialized = true;
        }

        public void SetAsBest()
        {
            if (_controlledCharacter)
            {
                _bestId = _controlledCharacter.Id;
            }
        }

        private bool IsBest()
        {
            return _controlledCharacter && _bestId == _controlledCharacter.Id;
        }

        private void OnHit()
        {
            // TotalScore -= 200;
            UpdateCharacterScoreText();
        }

        private void GotBadCollision()
        {
            // NeuralNetwork.Mutate(Network, _mutationAmount, 1);
        }

        private void OnDied()
        {
            _isInitialized = false;
            // TotalScore -= 300;
        }

        private void FixedUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }

            var addedCount = 0;
            var mainSensorHit = false;
            var anySensorHit = false;
            foreach (var sensorListener in _sensorListeners)
            {
                var sensorData = sensorListener.SensoryData;
                sensorData.CopyTo(_sensoryDataBuffer, addedCount);
                addedCount += sensorData.Length;
                if (!mainSensorHit)
                {
                    mainSensorHit = sensorListener.WasMainSensorHit;
                }

                if (!anySensorHit)
                {
                    anySensorHit = sensorListener.WasAnySensorHit;
                }
            }

            if (!anySensorHit)
            {
                _stimulationInput = MathF.Sin(Time.fixedTime);
            }
            
            _sensoryDataBuffer[_sensorCount] = _stimulationInput;
            _stimulationInput = 0;
            
            var outputs = NeuralNetwork.FeedForward(
                _sensoryDataBuffer, Network);

            var anyOutPut = false;

            for (var i = 0; i < outputs.Length; i++)
            {
                var outputValue = outputs[i];
                if (MathF.Abs(outputValue) <= 0.02 && i != 2)
                {
                    continue;
                }

                anyOutPut = true;
                switch (i)
                {
                    case 0:
                    {
                        if (outputValue > 0)
                        {
                            _controlledCharacter.WalkForward();
                        }
                        else
                        {
                            _controlledCharacter.WalkBackward();
                        }
                        
                        /*if (!mainSensorHit)
                        {
                            TotalScore += anySensorHit ? 2 : 1;
                        }*/
                        break;
                    }
                    case 1:
                    {
                        if (outputValue > 0)
                        {
                            _controlledCharacter.StrafeRight();
                        }
                        else
                        {
                            _controlledCharacter.StrafeLeft();
                        }

                        /*if (!mainSensorHit)
                        {
                            TotalScore += anySensorHit ? 2 : 1;
                        }*/
                        break;
                    }
                    case 2:
                    {
                        _controlledCharacter.SetRotation(outputValue * 180f);
                        /*if (!mainSensorHit)
                        {
                            TotalScore += anySensorHit ? 2 : 1;
                        }
                        */

                        break;
                    }
                    case 3:
                    {
                        if (mainSensorHit)
                        {
                            TotalScore += 1;
                        }
                        /*if (outputValue > 0 && _controlledCharacter.Shoot(mainSensorHit ? TargetWasHitWhileIsLockedOnMainTarget : TargetWasHitWhileNotLockedOnMainTarget))
                        {
                            if (mainSensorHit)
                            {
                                TotalScore += 2 * (int)_controlledCharacter.Health;
                            }/*
                            else
                            {
                                TotalScore -= 15;
                            }#1#
                        }*/
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
            
            if (mainSensorHit)
            {
                TotalScore += (int)_controlledCharacter.Health;
                if (_controlledCharacter.Shoot(mainSensorHit
                        ? TargetWasHitWhileIsLockedOnMainTarget
                        : TargetWasHitWhileNotLockedOnMainTarget))
                {
                    TotalScore += 2 * (int)_controlledCharacter.Health;
                }
                // var _stimulationTarget = (TotalScore > 0 ? 1f : -1);
                // var stimulationStep = 1f / (Math.Abs(TotalScore / 100) + 1);
                // _stimulationInput = AiUtils.Lerp(_stimulationInput, _stimulationTarget, 
                    // stimulationStep);
                // var stimAbs = Mathf.Abs(_stimulationInput);
                // NeuralNetwork.Mutate(Network, _mutationAmount * stimAbs, 1);
            }
            else
            {
                // _stimulationInput = AiUtils.Lerp(_stimulationInput, 0, 0.01f);
                // var stimAbs = Mathf.Abs(_stimulationInput);
                // if (stimAbs > 0.01)
                // {
                    // NeuralNetwork.Mutate(Network, _mutationAmount * stimAbs, 1);
                // }
            }
        }

        private void TargetWasHitWhileIsLockedOnMainTarget(int hitScore)
        {
            if (hitScore < 0)
            {
                // NeuralNetwork.Mutate(Network, _mutationAmount, 1);
                TotalScore -= 15;
                return;
            }
            
            var healthBeforeHit = (int)_controlledCharacter.Health;
            if (hitScore == 2)
            {
                _controlledCharacter.GiveHealthBump();
            }

            TotalScore += hitScore * healthBeforeHit;
        }
        
        private void TargetWasHitWhileNotLockedOnMainTarget(int hitScore)
        {
            if (hitScore < 0)
            {
                // NeuralNetwork.Mutate(Network, _mutationAmount, 1);
                TotalScore -= 15;
                return;
            }
            
            TotalScore += 1;
        }
    }
}