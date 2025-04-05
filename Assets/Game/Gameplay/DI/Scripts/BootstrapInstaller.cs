using Game.Gameplay.Character;
using Zenject;

namespace Game.Gameplay.DI
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameInput>().AsSingle();
            
            InstallSettings();
        }

        private void InstallSettings()
        {
            Container.Bind<CharacterMovementSettings>().AsSingle();
        }
    }
}