using System;
using System.Collections.Generic;

namespace Game.Scripts.Utilities
{
    public class CompositeCondition
    {
        private readonly HashSet<Func<bool>> _conditions = new();

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public bool IsTrue()
        {
            foreach (var condition in _conditions)
                if (condition.Invoke() == false)
                    return false;

            return true;
        }
    }
}