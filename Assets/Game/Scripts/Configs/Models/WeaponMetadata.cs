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
        [SerializeField] private Color _fillColor;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private AudioClip _shotSound;

        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
        public Color FillColor => _fillColor;
        public AudioClip ShotSound => _shotSound;
    }
}