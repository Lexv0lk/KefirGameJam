using System;
using UnityEngine;

namespace Game.Scripts.Loot
{
    [RequireComponent(typeof(Collider))]
    public class CollectableLootView : MonoBehaviour
    {
        private LootConfig _connectedConfig;

        public event Action<CollectableLootView, LootConfig> Collected; 

        public void ConnectTo(LootConfig lootConfig)
        {
            _connectedConfig = lootConfig;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Collected?.Invoke(this, _connectedConfig);
            gameObject.SetActive(false);
        }
    }
}