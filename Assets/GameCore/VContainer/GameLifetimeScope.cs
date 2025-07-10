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
    private Player _player;

    [SerializeField]
    private PlayerJump _playerJump;

    [SerializeField]
    private WinPlace[] _winPlaces;

    [SerializeField]
    private InputHandler _inputHandler;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterGameComponents(builder);

        RegisterGameControllers(builder);

        RegisterManagement(builder);

        RegisterManagementComponents(builder);
    }

    private void RegisterGameComponents(IContainerBuilder builder)
    {
        builder.RegisterComponent(_inputHandler)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.RegisterComponent(_player)
            .AsImplementedInterfaces()
            .AsSelf();

        BoxCollider[][] borders = new BoxCollider[2][];

        borders[0] = _horizontalBorders;
        borders[1] = _verticalBorders;

        builder.RegisterEntryPoint<CameraBorders>()
            .AsSelf()
            .WithParameter(borders);
    }

    private void RegisterGameControllers(IContainerBuilder builder)
    {
        builder.Register<PlayerController>(Lifetime.Singleton)
            .WithParameter(_playerJump)
            .AsImplementedInterfaces()
            .AsSelf();
    }

    private void RegisterManagement(IContainerBuilder builder)
    {
        builder.Register<GameListenersManager>(Lifetime.Singleton);

        builder.RegisterEntryPoint<GameListenersInstaller>()
            .AsSelf();
    }

    private void RegisterManagementComponents(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<WinPlaces>()
            .AsSelf()
            .WithParameter(_winPlaces);
    }
}