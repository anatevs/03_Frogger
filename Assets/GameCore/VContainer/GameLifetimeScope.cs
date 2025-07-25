using VContainer;
using VContainer.Unity;
using UnityEngine;
using GameManagement;
using GameCore;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private BoxCollider[] _horizontalBorders = new BoxCollider[2];

    [SerializeField]
    private BoxCollider[] _verticalBorders = new BoxCollider[2];

    [SerializeField]
    private PlayerCollisions _playerCollisions;

    [SerializeField]
    private PlayerJump _playerJump;

    [SerializeField]
    private WinPlace[] _winPlaces;

    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private RowsManager _rowsManager;

    [SerializeField]
    private LevelConfig[] _levelConfigs;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterComponents(builder);

        RegisterManagement(builder);

        RegisterGameControllers(builder);

        RegisterManagementComponents(builder);
    }

    private void RegisterComponents(IContainerBuilder builder)
    {
        builder.Register<PlayerLifes>(Lifetime.Singleton);




        builder.RegisterComponent(_inputHandler)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.RegisterComponent(_playerCollisions)
            .AsImplementedInterfaces()
            .AsSelf();

        BoxCollider[][] borders = new BoxCollider[2][];

        borders[0] = _horizontalBorders;
        borders[1] = _verticalBorders;

        builder.RegisterEntryPoint<CameraBorders>()
            .AsSelf()
            .WithParameter(borders);
    }

    private void RegisterManagement(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameListenersManager>()
            .AsSelf();

        builder.RegisterEntryPoint<GameListenersInstaller>()
            .AsSelf();

        builder.RegisterEntryPoint<GameManager>()
            .AsSelf();
    }

    private void RegisterGameControllers(IContainerBuilder builder)
    {
        builder.Register<PlayerController>(Lifetime.Singleton)
            .WithParameter<PlayerJump>(_playerJump)
            .AsImplementedInterfaces()
            .AsSelf();
    }

    private void RegisterManagementComponents(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<WinPlaces>()
            .AsSelf()
            .WithParameter(_winPlaces);

        builder.RegisterComponent(_rowsManager);

        builder.Register<LevelManager>(Lifetime.Singleton)
            .WithParameter<LevelConfig[]>(_levelConfigs)
            .AsImplementedInterfaces()
            .AsSelf();
    }
}