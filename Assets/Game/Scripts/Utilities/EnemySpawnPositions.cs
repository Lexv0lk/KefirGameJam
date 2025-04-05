using UnityEngine;

namespace Game.Scripts.Utilities
{
    public class EnemySpawnPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;

        public Vector3 GetRandomPosition()
        {
            return _points[Random.Range(0, _points.Length)].position;
        }
    }
}