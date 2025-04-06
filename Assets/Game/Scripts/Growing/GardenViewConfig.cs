using UnityEngine;

namespace Game.Scripts.Growing
{
    [CreateAssetMenu(fileName = "Garden View Config", menuName = "Configs/GardenView")]
    public class GardenViewConfig : ScriptableObject
    {
        [SerializeField] private Sprite _growIcon;
        [SerializeField] private Color _growingColor = Color.blue;
        [SerializeField] private Color _maturationColor = Color.green;
        
        public Sprite GrowIcon => _growIcon;
        public Color GrowingColor => _growingColor;
        public Color MaturationColor => _maturationColor;
    }
}