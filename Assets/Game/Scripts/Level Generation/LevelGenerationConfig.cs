using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.LevelGeneration
{
    [CreateAssetMenu(fileName = "Level Generation Config", menuName = "Configs/Level Generation Config")]
    public class LevelGenerationConfig : ScriptableObject
    {
        [SerializeField] private LevelPart[] _possibleParts;
        [SerializeField] private int _lengthOffset;
        [SerializeField] private float _disposeDistance;
        
        public LevelPart[] PossibleParts => _possibleParts;
        public int LengthOffset => _lengthOffset;
        public float DisposeDistance => _disposeDistance;

        [Button]
        private void UpdateIds()
        {
            for (int i = 0; i < _possibleParts.Length; i++)
                _possibleParts[i].SetId(i);
        }
        
    }
}