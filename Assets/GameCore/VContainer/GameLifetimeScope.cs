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

    [SerializeField]
    private PauseMenuView _pauseMenuView;

    [SerializeField]
    private GameplayUIView _gameplayUIView;

    [SerializeField]
    private LifesPanel _lifesView;

    [SerializeField]
    private AppEndManager _appEndManager;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterInfoComponents(builder);

        RegisterComponents(builder);

        RegisterGameListeners(builder);

        RegisterGameControllers(builder);

        RegisterManagers(builder);

        RegisterSaveLoad(builder);

        RegisterGameManagement(builder);
    }

    private void RegisterInfoComponents(IContainerBuilder builder)
    {
        builder.Register<PointsStorageManager>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<TimeScaleSetter>(Lifetime.Singleton)
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

        builder.Register<LifesPanelController>(Lifetime.Singleton)
            .WithParameter<LifesPanel>(_lifesView)
            .AsImplementedInterfaces()
            .AsSelf();
    }

    private void RegisterGameListeners(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameListenersManager>()
            .AsSelf();

        builder.RegisterEntryPoint<GameListenersInstaller>()
            .AsSelf();
    }

    private void RegisterGameControllers(IContainerBuilder builder)
    {
        builder.Register<PlayerController>(Lifetime.Singleton)
            .WithParameter<PlayerJump>(_playerJump)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<GameplayUIController>(Lifetime.Singleton)
            .WithParameter<GameplayUIView>(_gameplayUIView)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<PauseMenuController>(Lifetime.Singleton)
            .WithParameter<PauseMenuView>(_pauseMenuView)
            .AsImplementedInterfaces()
            .AsSelf();
    }

    private void RegisterManagers(IContainerBuilder builder)
    {
        builder.Register<WinPlaces>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf()
            .WithParameter<WinPlace[]>(_winPlaces)
            .WithParameter<PlayerJump>(_playerJump);

        builder.RegisterComponent(_rowsManager);

        builder.Register<LevelManager>(Lifetime.Singleton)
            .WithParameter<LevelConfig[]>(_levelConfigs)
            .AsImplementedInterfaces()
            .AsSelf();
    }

    private void RegisterSaveLoad(IContainerBuilder builder)
    {
        builder.Register<LevelIndexSaveLoad>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();


        builder.Register<SaveLoadManager>(Lifetime.Singleton);
    }

    private void RegisterGameManagement(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameManager>()
            .AsSelf()
            .WithParameter<AppEndManager>(_appEndManager);
    }
}