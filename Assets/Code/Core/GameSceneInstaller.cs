using Matching.Block;
using Matching.Board;
using UnityEngine;
using Zenject;

// just scene installer
namespace Matching.Core
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _mainContainer;
        [SerializeField] private GridSettings _gridSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_gridSettings).AsSingle();
            Container.BindInstance(_mainContainer).AsSingle();
            
            CellInstaller.Install(Container);
            BlockInstaller.Install(Container);
            BoardInstaller.Install(Container);

            GameInstaller.Install(Container);
           
            Debug.Log("SceneInstaller initialized");
        }
    }
}