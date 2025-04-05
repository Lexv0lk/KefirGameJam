using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.LevelGeneration
{
    public class PartLevelConnector
    {
        private readonly LevelGenerationConfig _config;
        private readonly Dictionary<int, Dictionary<ConnectionType, LevelPart[]>> 
            _possibleConnections = new();
        
        private readonly Dictionary<ConnectionType, Func<LevelPart, bool>> 
            _connectionCheckers = new()
        {
            { ConnectionType.Left, part => part.RightExit != null },
            { ConnectionType.Right, part => part.LeftExit != null },
            { ConnectionType.Up, part => part.DownExit != null },
            { ConnectionType.Down, part => part.UpExit != null }
        };
        
        public PartLevelConnector(LevelGenerationConfig config)
        {
            _config = config;
            var allParts = config.PossibleParts.ToArray();

            foreach (var part in allParts)
            {
                var connections = new Dictionary<ConnectionType, LevelPart[]>();

                for (int i = 0; i < (int)ConnectionType.MAX; i++)
                {
                    if (CanConnect(part, (ConnectionType)i))
                        connections[(ConnectionType)i] =
                            GetPossibleConnections(config.PossibleParts, (ConnectionType)i);
                }
                
                _possibleConnections[part.Id] = connections;
            }
        }

        public List<(LevelPart part, ConnectionType connectionType)> GetAroundConnections(LevelPart levelPart)
        {
            var result = new List<(LevelPart part, ConnectionType connectionType)>();
            
            foreach (var connection in _possibleConnections[levelPart.Id])
            {
                var part = GetRandomConnection(levelPart, connection.Key);
                result.Add((part, connection.Key));
            }

            return result;
        }

        private LevelPart GetRandomConnection(LevelPart part, ConnectionType connectionType)
        {
            var possibleConnections = _possibleConnections[part.Id][connectionType];
            return possibleConnections[Random.Range(0, possibleConnections.Length)];
        }
        
        private LevelPart[] GetPossibleConnections(LevelPart[] parts, ConnectionType connectionType)
        {
            return parts.Where(p => _connectionCheckers[connectionType](p)).ToArray();
        }

        private bool CanConnect(LevelPart levelPart, ConnectionType connectionType)
        {
            if (connectionType == ConnectionType.Left && levelPart.LeftExit != null)
                return true;

            if (connectionType == ConnectionType.Right && levelPart.RightExit != null)
                return true;

            if (connectionType == ConnectionType.Up && levelPart.UpExit != null)
                return true;

            if (connectionType == ConnectionType.Down && levelPart.DownExit != null)
                return true;

            return false;
        }
    }
}