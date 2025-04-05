using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Configs.Controllers;
using Game.Scripts.Models;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class AmmunitionRefillController : IInitializable
    {
        private readonly AmmunitionRefillConfig _config;
        private readonly RiffleStoreModel _model;

        private CancellationTokenSource _cancellationTokenSource;

        public AmmunitionRefillController(AmmunitionRefillConfig config, RiffleStoreModel model)
        {
            _config = config;
            _model = model;
        }
        
        public void Initialize()
        {
            Enable();
        }

        public void Enable()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Dispose();

            _cancellationTokenSource = new CancellationTokenSource();
            StartRefilling(_cancellationTokenSource).Forget();
        }

        public void Disable()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid StartRefilling(CancellationTokenSource cts)
        {
            while (true)
            {
                await UniTask.WaitForSeconds(_config.Delay, cancellationToken: cts.Token);
                
                if (cts.IsCancellationRequested)
                    break;

                _model.AmmunitionAmount.Value =
                    Mathf.Min(_model.AmmunitionAmount.Value + 1, _model.MaxAmmunitionAmount);
            }
        }

        ~AmmunitionRefillController()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Dispose();
        }
    }
}