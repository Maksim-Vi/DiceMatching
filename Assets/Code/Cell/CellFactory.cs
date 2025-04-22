using Matching.Board.Cell;
using UnityEngine;
using Zenject;

namespace Code.Cell
{
    public class CellFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LocalResoursesManager _localResoursesManager;

        public CellController CreateCellController(GameObject container, int index, bool isVisible, Vector3 position)
        {
            CellView prefab = _localResoursesManager.LoadAssetByPath<CellView>("Prefabs/Board/CallView");
            if(prefab ==null) return null;
            
            CellView blockView = _container.InstantiatePrefabForComponent<CellView>(prefab, container.transform);
            CellController cellController = _container.Instantiate<CellController>();
            cellController.ApplyView(blockView);
            cellController.Initialize(index, isVisible, position);
            
            return cellController;
        }
    }
}