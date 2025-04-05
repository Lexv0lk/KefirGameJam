using UnityEngine;

namespace Game.Scripts.Configs.Input
{
    [CreateAssetMenu(fileName = "Mouse Rotation Config", menuName = "Configs/Mouse Rotation")]
    public class MouseRotationConfig : ScriptableObject
    {
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private int _maximalRayDistance;

        public LayerMask GroundMask => _groundMask;
        public int MaximalRayDistance => _maximalRayDistance;
    }
}