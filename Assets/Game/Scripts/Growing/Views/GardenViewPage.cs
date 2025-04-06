using Game.Scripts.Models;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Growing
{
    public class GardenViewPage : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;
        [SerializeField] private GardenView _gardenView;
        
        private DiContainer _diContainer;
        private InputModel _inputModel;

        [Inject]
        private void Init(DiContainer diContainer, InputModel inputModel)
        {
            _diContainer = diContainer;
            _inputModel = inputModel;
        }
        
        private void Awake()
        {
            _gardenView.Initialize(_diContainer.Instantiate<GardenViewPresenter>());
        }

        private void Update()
        {
            if (Input.GetKeyUp(_key))
            {
                if (_gardenView.gameObject.activeInHierarchy)
                {
                    _gardenView.Close();
                    _inputModel.IsPlayerInputEnabled = true;
                    Time.timeScale = 1;
                }
                else
                {
                    _gardenView.Open();
                    _inputModel.IsPlayerInputEnabled = false;
                    Time.timeScale = 0.3f;
                }
            }
        }
    }
}