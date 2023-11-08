using UnityEngine;
using Zenject;

public class ToolsInstaller : MonoInstaller
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private PoolsParent poolsParent;

    public override void InstallBindings()
    {
        Container.Bind<Canvas>().FromInstance(canvas);
        Container.Bind<PoolsParent>().FromInstance(poolsParent).NonLazy();
    }
}