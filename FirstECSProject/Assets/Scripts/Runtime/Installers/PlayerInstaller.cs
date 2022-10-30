using UnityEngine;
using Zenject;

namespace Kulikova.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject player;
        
        public override void InstallBindings()
        {
            var characterData = player.GetComponent<CharacterData>();
            
            Container
                .Bind<CharacterData>()
                .FromInstance(characterData)
                .AsSingle()
                .NonLazy();
        }
    }
}