using UnityEngine;

namespace Game.Scripts.Growing
{
    public class ViewToggler : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;
        [SerializeField] private GameObject _gameObject;

        private void Update()
        {
            if (Input.GetKeyUp(_key))
                _gameObject.SetActive(!_gameObject.activeInHierarchy);
        }
    }
}