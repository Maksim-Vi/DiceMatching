using Zenject;

namespace Matching
{
    public class GameInstaller : Installer<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        }
    }
}