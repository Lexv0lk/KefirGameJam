using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerAnimationEvents
    {
        [SerializeField] private string _deadEvent = "Died";
        [SerializeField] private string _attackEvent = "Shooted";
        
        public string DeadEvent => _deadEvent;
        public string AttackEvent => _attackEvent;
    }
}