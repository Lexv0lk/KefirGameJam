using System;
using Game.Scripts.Loot;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Configs.Models
{
    [Serializable]
    public class WeaponMetadata
    {
        [SerializeField, PreviewField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;

        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
    }
}