using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Objects;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.LevelGeneration
{
    public class LevelGenerator : IInitializable, IDisposable
    {
        private readonly LevelGenerationConfig _config;
        private readonly PartLevelConnector _levelConnector;
        private readonly DiContainer _diContainer;
        private readonly AtomicEntity _player;
        private readonly Dictionary<Vector2, LevelPart> _levelParts = new();

        private LevelPart _currentPart;

        public LevelGenerator(LevelGenerationConfig config, PartLevelConnector levelConnector, DiContainer diContainer, AtomicEntity player)
        {
            _config = config;
            _levelConnector = levelConnector;
            _diContainer = diContainer;
            _player = player;
        }
        
        public void Initialize()
        {
            _currentPart = _diContainer.InstantiatePrefabForComponent<LevelPart>(_config.PossibleParts[0]);
            _currentPart.transform.position = new Vector3(0, 0, 0);
            _levelParts[new Vector2(0, 0)] = _currentPart;
            
            UpdateGraph();
            
            _currentPart.PlayerEntered += OnPlayerEnteredToPart;
        }

        private void UpdateGraph()
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Vector2 coords = new Vector2(_currentPart.transform.position.x + _config.LengthOffset * i,
                        _currentPart.transform.position.z + _config.LengthOffset * j);
                    
                    if (_levelParts.ContainsKey(coords))
                        continue;

                    var randomPrefab = _config.PossibleParts[Random.Range(0, _config.PossibleParts.Length)];
                    var part = _diContainer.InstantiatePrefabForComponent<LevelPart>(randomPrefab);
                    part.transform.position = new Vector3(coords.x, 0, coords.y);
                    
                    _levelParts[coords] = part;
                    
                    part.PlayerEntered += OnPlayerEnteredToPart;
                }
            }
        }

        private void OnPlayerEnteredToPart(LevelPart part)
        {
            _currentPart = part;
            UpdateGraph();
        }

        public void Dispose()
        {
            foreach (var levelPart in _levelParts.Values.ToArray())
                levelPart.PlayerEntered -= OnPlayerEnteredToPart;
        }
    }
}