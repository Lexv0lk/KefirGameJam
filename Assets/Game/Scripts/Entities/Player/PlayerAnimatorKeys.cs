using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerAnimatorKeys
    {
        [SerializeField] private string _deadBoolean = "IsDead";
        [SerializeField] private string _moveBoolean = "IsMoving";
        [SerializeField] private string _moveXFloat = "MoveX";
        [SerializeField] private string _moveYFloat = "MoveZ";
        [SerializeField] private string _attackTrigger = "Shoot";
        [SerializeField] private string _takeDamageTrigger = "TakeDamage";
        
        public string DeadBoolean => _deadBoolean;
        public string MoveBoolean => _moveBoolean;
        public string MoveXFloat => _moveXFloat;
        public string MoveYFloat => _moveYFloat;
        public string AttackTrigger => _attackTrigger;
        public string TakeDamageTrigger => _takeDamageTrigger;
    }
}