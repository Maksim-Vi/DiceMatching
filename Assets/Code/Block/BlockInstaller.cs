using Zenject;

namespace Matching.Block
{
    public class BlockInstaller : Installer<BlockInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlockFactory>().AsSingle();
        }
    }
}