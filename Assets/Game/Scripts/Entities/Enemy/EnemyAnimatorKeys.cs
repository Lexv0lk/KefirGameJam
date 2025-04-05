using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyAnimatorKeys
    {
        [SerializeField] private string _deadBoolean = "IsDead";
        [SerializeField] private string _moveBoolean = "IsMoving";
        [SerializeField] private string _attackTrigger = "Attack";
        
        public string DeadBoolean => _deadBoolean;
        public string MoveBoolean => _moveBoolean;
        public string AttackTrigger => _attackTrigger;
    }
}