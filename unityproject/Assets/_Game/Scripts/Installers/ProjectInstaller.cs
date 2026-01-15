using Zenject;
using ShowcaseGame.Events;

namespace ShowcaseGame.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Install SignalBus
            SignalBusInstaller.Install(Container);

            // Declare Signals
            Container.DeclareSignal<GameStateChangedSignal>();
            Container.DeclareSignal<BrickDestroyedSignal>();
            Container.DeclareSignal<BallCollisionSignal>();

            // Bind Global Services (Placeholders)
            // Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle();
        }
    }
}
