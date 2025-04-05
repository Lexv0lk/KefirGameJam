using UnityEngine;

namespace Game.Scripts.Utilities
{
    public class AutoDestroyer : MonoBehaviour
    {
        [SerializeField] private float _delay;

        private void Start()
        {
            Destroy(gameObject, _delay);
        }
    }
}