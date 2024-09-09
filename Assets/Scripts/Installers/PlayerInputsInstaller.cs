using UnityEngine;
using Zenject;

public class PlayerInputsInstaller : MonoInstaller
{
    public PlayerInputs _inputs;
    public override void InstallBindings()
    {
        Container.Bind<PlayerInputs>().FromComponentInHierarchy().AsSingle();
        //Container.BindInstance<PlayerInputs>(_inputs).AsSingle()
    }
}