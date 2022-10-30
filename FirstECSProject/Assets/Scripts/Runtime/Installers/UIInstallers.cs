using Kulikova.UI;
using UnityEngine;
using Zenject;

namespace Kulikova.Installers
{
    public class UIInstallers : MonoInstaller
    {
        [SerializeField] private CanvasViewModel canvasViewModel;
        
        public override void InstallBindings()
        {
            Container
                .Bind<CanvasViewModel>()
                .FromInstance(canvasViewModel)
                .AsSingle()
                .NonLazy();
        }
    }
}