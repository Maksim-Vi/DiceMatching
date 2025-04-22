using Matching.BlockMVC;
using UnityEngine;
using Zenject;

namespace Matching.Block
{
    public class BlockFactory
    {                
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LocalResoursesManager _localResoursesManager;

        public BlockController CreateBlockController(GameObject container)
        {
            BlockView prefab = _localResoursesManager.LoadAssetByPath<BlockView>("Prefabs/Board/BlockView");

            if(prefab == null) return null;

            BlockView blockView = _container.InstantiatePrefabForComponent<BlockView>(prefab, container.transform);
            BlockController blockController = _container.Instantiate<BlockController>();
            blockController.ApplyView(blockView);
            blockController.InitColor();
            
            return blockController;
        }
    }
}