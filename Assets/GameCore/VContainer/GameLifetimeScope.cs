using VContainer;
using VContainer.Unity;
using UnityEngine;
using GameManagement;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private GameListenersManager _listenersManager;

    protected override void Configure(IContainerBuilder builder)
    {

        RegisterManagement(builder);
    }


    private void RegisterManagement(IContainerBuilder builder)
    {
        builder.RegisterComponent(_listenersManager);

        builder.RegisterEntryPoint<GameListenersInstaller>()
            .AsSelf();
    }
}