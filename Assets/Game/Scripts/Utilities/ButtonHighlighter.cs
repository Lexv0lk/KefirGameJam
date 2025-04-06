using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Utilities
{
    [RequireComponent(typeof(Button))]
    public class ButtonHighlighter : MonoBehaviour
    {
        [SerializeField] private GameObject _targetGraphic;
        
        private Button _button;
        private bool _lastInteractableValue;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            if (_button.interactable != _lastInteractableValue)
            {
                _lastInteractableValue = _button.interactable;
                _targetGraphic.SetActive(_button.interactable);
            }
        }
    }
}