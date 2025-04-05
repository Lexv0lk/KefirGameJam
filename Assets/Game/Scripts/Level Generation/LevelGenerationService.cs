using Unity.AI.Navigation;
using UnityEngine;

namespace Game.Scripts.LevelGeneration
{
    public class LevelGenerationService : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _globalSurface;

        public NavMeshSurface GlobalSurface => _globalSurface;
    }
}