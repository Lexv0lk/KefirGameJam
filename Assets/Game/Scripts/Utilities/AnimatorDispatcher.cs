using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Utilities
{
    public class AnimatorDispatcher : MonoBehaviour
    {
        private readonly Dictionary<string, List<Action>> _actionsDictionary = new();

        public void SubscribeOnEvent(string key, Action action)
        {
            if (_actionsDictionary.ContainsKey(key) == false)
                _actionsDictionary[key] = new List<Action>();
            
            _actionsDictionary[key].Add(action);
        }

        public void UnsubscribeOnEvent(string key, Action action)
        {
            if (_actionsDictionary.ContainsKey(key) && _actionsDictionary[key].Contains(action))
                _actionsDictionary[key].Remove(action);
        }

        //CALLED FROM ANIMATION
        public void ReceiveEvent(string key)
        {
            if (_actionsDictionary.TryGetValue(key, out var actionList))
                foreach (var action in actionList)
                    action?.Invoke();
        }
    }
}