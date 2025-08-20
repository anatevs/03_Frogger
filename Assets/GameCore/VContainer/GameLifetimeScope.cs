using VContainer;
using VContainer.Unity;
using UnityEngine;
using GameManagement;
using GameCore;
using UI;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private BoxCollider[] _horizontalBorders = new BoxCollider[2];

    [SerializeField]
    private PlayerCollisions _playerCollisions;

    [SerializeField]
    private PlayerJump _playerJump;

    [SerializeField]
    private FrogFriend _frogFriend;

    [SerializeField]
    private TimerView _timerView;

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
        RegisterInfoComponents(builder);

        RegisterComponents(builder);

        RegisterManagement(builder);

        RegisterGameControllers(builder);

        RegisterManagementComponents(builder);
    }

    private void RegisterInfoComponents(IContainerBuilder builder)
    {
        builder.Register<PointsStorageController>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();
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

        builder.Register<FixedBorders>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .WithParameter(_horizontalBorders);

        builder.Register<TimeCounter>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf()
            .WithParameter(_timerView);

        builder.Register<PointsCounter>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf()
            .WithParameter<PlayerJump>(_playerJump);

        builder.RegisterComponent<FrogFriend>(_frogFriend)
            .AsImplementedInterfaces()
            .AsSelf();
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
            .WithParameter<WinPlace[]>(_winPlaces)
            .WithParameter<PlayerJump>(_playerJump);

        builder.RegisterComponent(_rowsManager);

        builder.Register<LevelManager>(Lifetime.Singleton)
            .WithParameter<LevelConfig[]>(_levelConfigs)
            .AsImplementedInterfaces()
            .AsSelf();
    }
}