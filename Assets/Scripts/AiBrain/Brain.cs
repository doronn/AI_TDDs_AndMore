using System;
using System.Collections.Generic;
using System.Linq;
using Sensory;
using Shooter;
using UnityEngine;

namespace AiBrain
{
    public class Brain : MonoBehaviour
    {
        private static int _networkIndex = 100;
        [SerializeField] private DangerSensor[] _sensors;
        [SerializeField] private float _lastSensoryDirection = 0;
        [SerializeField] private Character _controlledCharacter;
        public NeuralNetwork Network { get; private set; }

        public Transform CharacterTransform => _controlledCharacter != null ? _controlledCharacter.transform : null;
        public int TotalScore
        {
            get => _totalScore;
            private set
            {
                _totalScore = value;
                
                if(_controlledCharacter)
                {
                    _controlledCharacter._scoreText.SetText(value.ToString());
                }
            }
        }

        private List<SensorListener> _sensorListeners = new();

        private bool _isInitialized = false;
        [SerializeField]
        private int _totalScore = 100;

        public void Init(NeuralNetwork predefinedNetwork = null)
        {
            if (_sensors == null || _sensors.Length == 0)
            {
                throw new Exception("missing sensor");
            }
            
            Network = predefinedNetwork ?? new NeuralNetwork(6, 14, 8);
            _controlledCharacter.Init(_networkIndex, OnDied, OnHit);
            foreach (var sensor in _sensors)
            {
                var sensorListener = new SensorListener();
                _sensorListeners.Add(sensorListener);
                sensor.RegisterListener(sensorListener, _networkIndex);
            }
            
            _networkIndex++;

            _isInitialized = true;
        }

        private void OnHit()
        {
            TotalScore -= 500;
        }

        private void OnDied()
        {
            _networkIndex++;
            _isInitialized = false;
            TotalScore -= 1000;
        }

        private void FixedUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }

            /*var inputsList = new List<float> { _totalScore / 10000f };
            foreach (var sensorListener in _sensorListeners)
            {
                inputsList.AddRange(sensorListener.SensoryData);
            }*/

            var sensorListener = _sensorListeners.FirstOrDefault();

            if (sensorListener == null)
            {
                throw new Exception("missing sensor");
            }

            var inputData = sensorListener.SensoryData;
            var outputs = NeuralNetwork.FeedForward(
                inputData, Network);

            var anyOutPut = false;
            
            var detectedCharacterDistanceInput = inputData[0];
            var detectedCharacterDirectionInput = inputData[1];
            var rotationInputDirection = detectedCharacterDistanceInput <= 0.005f
                ? 0
                : detectedCharacterDirectionInput switch
                {
                    > 0 => -1,
                    < 0 => 1,
                    _ => 0
                };

            for (var i = 0; i < outputs.Length; i++)
            {
                var outputValue = outputs[i];
                if (outputValue <= 0 && i != 5)
                {
                    continue;
                }

                anyOutPut = true;
                switch (i)
                {
                    case 0:
                        _controlledCharacter.WalkForward();
                        break;
                    case 1:
                        _controlledCharacter.WalkBackward();
                        break;
                    case 2:
                        _controlledCharacter.StrafeLeft();
                        break;
                    case 3:
                        _controlledCharacter.StrafeRight();
                        break;
                    case 4:
                        // _controlledCharacter.RotateLeft();
                        // TotalScore -= rotationInputDirection;
                        // Debug.Log("boop");
                        break;
                    case 5:
                        _controlledCharacter.SetRotation(outputValue * 180f);
                        // Debug.Log($"Direction S= {detectedCharacterDirectionInput * 180f} O= {outputValue * 180f}");

                        if (Math.Abs(detectedCharacterDirectionInput - outputValue) < 1 / 180f)
                        {
                            TotalScore += 5;
                        }
                        // TotalScore += rotationInputDirection;
                        break;
                    case 6:
                        if (_controlledCharacter.Shoot(TargetWasHit))
                        {
                            if (detectedCharacterDistanceInput > 0 && Math.Abs(detectedCharacterDirectionInput) < 0.0005f)
                            {
                                TotalScore += 100;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            
            if (Math.Abs(detectedCharacterDirectionInput) < Math.Abs(_lastSensoryDirection))
            {
                TotalScore += 1;
            }
            else
            {
                TotalScore -= 10;
            }

            _lastSensoryDirection = detectedCharacterDirectionInput;
            
            if (anyOutPut)
            {
                // TotalScore += 1;
            }
        }

        private void TargetWasHit()
        {
            TotalScore += 600;
        }
    }
}