using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // 1. Install SignalBus engine
        SignalBusInstaller.Install(Container);

        // 2. Declare Signals
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<BrickDestroyedSignal>();
        Container.DeclareSignal<BallCollisionSignal>();

        // 3. Bind Core Managers
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.Bind<ILogger>().To<DebugLogger>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputReader>().AsSingle();

        // 4. Bind Game States
        Container.Bind<MenuState>().AsSingle();
        Container.Bind<GameplayState>().AsSingle();
        Container.Bind<WinState>().AsSingle();
        Container.Bind<LoseState>().AsSingle();

        // 5. Bind Entities (In Hierarchy)
        // We use FromComponentInHierarchy so Zenject can find these in the scene
        Container.BindInterfacesAndSelfTo<PaddleController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<BallController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelManager>().FromComponentInHierarchy().AsSingle();
    }
}
