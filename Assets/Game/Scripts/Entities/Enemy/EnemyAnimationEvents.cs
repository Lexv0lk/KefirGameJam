using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyAnimationEvents
    {
        [SerializeField] private string _deadEvent = "Died";
        [SerializeField] private string _attackEvent = "Attacked";
        [SerializeField] private string _attackEndEvent = "AttackEnd";
        
        public string DeadEvent => _deadEvent;
        public string AttackEvent => _attackEvent;
        public string AttackEndEvent => _attackEndEvent;
    }
}