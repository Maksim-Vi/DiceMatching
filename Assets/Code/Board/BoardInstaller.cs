using Matching.Block;
using Zenject;

namespace Matching.Board
{
    public class BoardInstaller : Installer<BoardInstaller>
    {        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BoardFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoardManager>().AsSingle().NonLazy(); 
        }
    }
}