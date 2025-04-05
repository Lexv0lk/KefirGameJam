using UnityEngine;

namespace Game.Scripts.Configs.Models
{
    [CreateAssetMenu(fileName = "Riffle Store Config", menuName = "Configs/Riffle Store")]
    public class RiffleStoreConfig : ScriptableObject
    {
        [SerializeField] private int _startAmount;
        [SerializeField] private int _maximalAmount;

        public int StartAmount => _startAmount;
        public int MaximalAmount => _maximalAmount;
    }
}