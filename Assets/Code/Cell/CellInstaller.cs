using Code.Cell;
using Zenject;

namespace Matching.Block
{
    public class CellInstaller : Installer<CellInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CellFactory>().AsSingle();
        }
    }
}