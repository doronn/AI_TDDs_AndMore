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

        private void Awake()
        {
            if (PlayerPrefs.HasKey(_brainConfig.BrainId))
            {
                _bestBrainJson = PlayerPrefs.GetString(_brainConfig.BrainId);
                _generationCounter = PlayerPrefs.GetInt(_brainConfig.BrainId + "_Counter", 0);
            }

            _iterationCounterText.text = _generationCounter.ToString();
            
            var halfOfSimulationsSqrtCount = _brainConfig.SqrtTotalSimulationsAmount / 2;
            for (int i = -halfOfSimulationsSqrtCount; i < halfOfSimulationsSqrtCount; i++)
            {
                for (int j = -halfOfSimulationsSqrtCount; j < halfOfSimulationsSqrtCount; j++)
                {
                    var simulationInstance = Instantiate(_brainConfig.SimulationPrefab, new Vector3(i * _brainConfig.MapSizeX, 0, j * _brainConfig.MapSizeZ),
                        Quaternion.identity);

                    var simulationBrains = simulationInstance.GetComponents<Brain>();
                    _brains.AddRange(simulationBrains);
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
            for (var index = 0; index < _brains.Count; index++)
            {
                var brain = _brains[index];
                var copiedNetwork = string.IsNullOrEmpty(_bestBrainJson) ? null : JsonConvert.DeserializeObject<NeuralNetwork>(_bestBrainJson);
                if (index == 0)
                {
                    brain.Init(copiedNetwork);
                    continue;
                }

                if (copiedNetwork != null)
                {
                    NeuralNetwork.Mutate(copiedNetwork, _brainConfig.MutationAmount);
                }

                brain.Init(copiedNetwork);
            }

            if (_brainConfig.AutoLearn)
            {
                StartCoroutine(LearnEndTimer());
            }
        }

        private IEnumerator FollowBestBrain()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.25f);

                var bestBrain = GetBestBrain();
                var bestBrainTransform = bestBrain.CharacterTransform;

                if (bestBrainTransform != null)
                {
                    _constraint.SetSource(0,
                        new ConstraintSource() { sourceTransform = bestBrainTransform, weight = 1 });
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
            var bestBrain = GetBestBrain();

            if (bestBrain == null)
            {
                throw new Exception("No brain found ???");
            }
            
            var brainJson = JsonConvert.SerializeObject(bestBrain.Network);
            
            PlayerPrefs.SetString(_brainConfig.BrainId, brainJson);
            PlayerPrefs.Save();
        }

        private Brain GetBestBrain()
        {
            var maxBrainScore = _brains.Max(brain => brain.TotalScore);
            var bestBrain = _brains.FirstOrDefault(brain => brain.TotalScore == maxBrainScore);
            return bestBrain;
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