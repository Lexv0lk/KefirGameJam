using Game.Scripts.Loot;
using UnityEngine;

namespace Game.Scripts.Growing
{
    [CreateAssetMenu(fileName = "Grow Data", menuName = "Configs/GrowData")]
    public class GrowData : ScriptableObject
    {
        [SerializeField] private Rarity _rarity;
        [SerializeField] private float _maturationTime;
        [SerializeField] private int _waterConsumption;

        public Rarity Rarity => _rarity;
        public float MaturationTime => _maturationTime;
        public int WaterConsumption => _waterConsumption;
    }
}