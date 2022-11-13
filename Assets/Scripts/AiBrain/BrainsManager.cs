using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

namespace AiBrain
{
    public class BrainsManager : MonoBehaviour
    {
        [SerializeField]
        private List<Brain> _brains;

        [SerializeField]
        private BrainManagerConfigurations _brainConfig; 

        [SerializeField]
        private ParentConstraint _constraint;

        [SerializeField]
        private TMP_Text _iterationCounterText;

        private string _bestBrainJson = null;

        private static int _generationCounter = 0;

        private float _startTime = 0;
        private NeuralNetworkArray _bestNetworks;

        private void Awake()
        {
            Application.runInBackground = true;
            if (PlayerPrefs.HasKey(_brainConfig.BrainId))
            {
                _bestBrainJson = PlayerPrefs.GetString(_brainConfig.BrainId);
                _generationCounter = PlayerPrefs.GetInt(_brainConfig.BrainId + "_Counter", 0);
            }

            _iterationCounterText.text = _generationCounter.ToString();
            
            var simulationsCount = _brainConfig.SimulationsAmount;
            var simulationCountSqrt = (int)Math.Ceiling(Math.Sqrt(simulationsCount));
            var halfSqrtCountCeil = (int)Math.Ceiling(simulationCountSqrt / 2f);
            var halfSqrtCountFloor = (int)Math.Floor(simulationCountSqrt / 2f);
            for (int i = -halfSqrtCountFloor; i < halfSqrtCountCeil; i++)
            {
                for (int j = -halfSqrtCountFloor; j < halfSqrtCountCeil; j++)
                {
                    if (simulationsCount <= 0)
                    {
                        break;
                    }

                    simulationsCount--;
                    var simulationInstance = Instantiate(_brainConfig.SimulationPrefab, new Vector3(i * _brainConfig.MapSizeX, 0, j * _brainConfig.MapSizeZ),
                        Quaternion.identity);

                    var simulationBrains = simulationInstance.GetComponents<Brain>();
                    _brains.AddRange(simulationBrains);
                }
                
                if (simulationsCount <= 0)
                {
                    break;
                }
            }
            
            StartCoroutine(FollowBestBrain());
  
            if (_brainConfig.AutoLearn)
            {
                StartCoroutine(StartLearnTimer());
            }
        }

        private IEnumerator StartLearnTimer()
        {
            yield return new WaitForSeconds(0.5f);
            
            StartSimulation();
        }

        public void StartSimulation()
        {
            _bestNetworks = string.IsNullOrEmpty(_bestBrainJson) ? null : JsonConvert.DeserializeObject<NeuralNetworkArray>(_bestBrainJson);
            var copiedNetworksCount = _bestNetworks?.Networks?.Length ?? 0;
            for (var index = 0; index < _brains.Count; index++)
            {
                var brain = _brains[index];
                if (index < copiedNetworksCount)
                {
                    if (_bestNetworks?.Networks != null)
                    {
                        var neuralNetworkSaveObject = _bestNetworks.Networks[index];
                        brain.Init(neuralNetworkSaveObject.Network, _brainConfig.MutationAmount);
                        continue;
                    }
                }
                var networkToCopy = _bestNetworks?.Networks?[index % copiedNetworksCount];
                var copiedNetwork = JsonConvert.DeserializeObject<NeuralNetworkSaveObject>(JsonConvert.SerializeObject(networkToCopy));
                var nextNetwork = copiedNetworksCount > 0 ? copiedNetwork?.Network : null;
                var isMutated = false;
                if (nextNetwork != null)
                {
                    NeuralNetwork.Mutate(nextNetwork, _brainConfig.MutationAmount);
                    isMutated = true;
                }

                brain.Init(nextNetwork, _brainConfig.MutationAmount, isMutated: isMutated);
            }

            if (_brainConfig.AutoLearn)
            {
                _startTime = Time.time;
                StartCoroutine(LearnEndTimer());
            }
        }

        private IEnumerator FollowBestBrain()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.25f);

                var bestBrain = GetBestBrain();
                var bestBrainTransform = bestBrain == null ? null : bestBrain.CharacterTransform;

                if (bestBrainTransform != null)
                {
                    _constraint.SetSource(0,
                        new ConstraintSource { sourceTransform = bestBrainTransform, weight = 1 });
                }
                
                if (_brainConfig.AutoLearn)
                {
                    _iterationCounterText.text = $"{_generationCounter.ToString()}\n{_brainConfig.IterationTime - (Time.time - _startTime):n2}";
                }
            }
        }

        private IEnumerator LearnEndTimer()
        {
            yield return new WaitForSeconds(_brainConfig.IterationTime);

            _generationCounter++;
            PlayerPrefs.SetInt(_brainConfig.BrainId + "_Counter", _generationCounter);
            SaveBestBrain();
            ResetSimulation();
        }

        public void SaveBestBrain()
        {
            var bestBrains = _brains;

            if (bestBrains == null || bestBrains.Count == 0)
            {
                throw new Exception("No brain found ???");
            }

            var bestNetworksSoFar =
                bestBrains.Select(brain =>
                    new NeuralNetworkSaveObject
                    {
                        Network = brain.Network,
                        NetworkScore = brain.TotalScore
                    }).ToList();
            if (_bestNetworks?.Networks != null)
            {
                bestNetworksSoFar.AddRange(_bestNetworks.Networks);
            }

            var orderedNetworks =
                bestNetworksSoFar.OrderByDescending(networkSaveObject => networkSaveObject.NetworkScore).Distinct(NeuralNetworkSaveObjectEqualityComparer.Instance);
            var takenBestBrains = orderedNetworks.Take(_brainConfig.AmountOfSavedNetworks);
            var bestNetworks = takenBestBrains as NeuralNetworkSaveObject[] ?? takenBestBrains.ToArray();

            var brainJson = JsonConvert.SerializeObject(new NeuralNetworkArray { Networks = bestNetworks });
            var averageBestScores = (int)bestNetworks.Average(network => network.NetworkScore);

            var brainBestAvgScore = _brainConfig.BrainId + "_bestAvgScore";
            var lastKnownBestAverage = PlayerPrefs.GetInt(brainBestAvgScore, 0);
            var highestTotalScore = bestNetworks.FirstOrDefault()?.NetworkScore;
            Debug.Log(
                $"Saving best brains with Maximum of {highestTotalScore} and avg of {averageBestScores}. last average was {lastKnownBestAverage}\nIndices: {string.Join(", ", bestNetworks.Select(o => o.Network.NetworkIndex))}");
            PlayerPrefs.SetInt(brainBestAvgScore, averageBestScores);
            PlayerPrefs.SetString(_brainConfig.BrainId, brainJson);
            PlayerPrefs.Save();
        }

        private Brain GetBestBrain()
        {
            if (_brains.Count == 0 || !_brains.Any(IsBrainInitialized))
            {
                return null;
            }
            
            var maxBrainScore = _brains.Where(IsBrainInitialized).Max(BrainScore);
            var bestBrain = _brains.FirstOrDefault(brain => brain.TotalScore == maxBrainScore);
            return bestBrain;
        }

        private int BrainScore(Brain brain)
        {
            return brain.TotalScore;
        }

        private bool IsBrainInitialized(Brain brain)
        {
            return brain.IsInitialized;
        }

        private List<Brain> GetBestBrains()
        {
            var sortedBrains = _brains.OrderByDescending(brain => brain.TotalScore).ToList();
            sortedBrains.FirstOrDefault()?.SetAsBest();
            return sortedBrains;
        }

        public void ClearBestBrain()
        {
            PlayerPrefs.DeleteKey(_brainConfig.BrainId);
        }

        public void ResetSimulation()
        {
            StopAllCoroutines();
            SceneManager.LoadScene(0);
        }
    }
}