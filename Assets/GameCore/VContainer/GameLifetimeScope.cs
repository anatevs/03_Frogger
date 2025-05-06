using VContainer;
using VContainer.Unity;
using UnityEngine;
using GameManagement;
using GameCore;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private GameListenersManager _listenersManager;

    [SerializeField]
    private BoxCollider[] _horizontalBorders = new BoxCollider[2];

    [SerializeField]
    private BoxCollider[] _verticalBorders = new BoxCollider[2];

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterGameComponents(builder);

        //RegisterManagement(builder);
    }

    private void RegisterGameComponents(IContainerBuilder builder)
    {
        BoxCollider[][] borders = new BoxCollider[2][];

        borders[0] = _horizontalBorders;
        borders[1] = _verticalBorders;

        builder.RegisterEntryPoint<CameraBorders>()
            .AsSelf()
            .WithParameter(borders);
    }


    private void RegisterManagement(IContainerBuilder builder)
    {
        builder.RegisterComponent(_listenersManager);

        builder.RegisterEntryPoint<GameListenersInstaller>()
            .AsSelf();
    }
}