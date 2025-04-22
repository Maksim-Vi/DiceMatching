using Matching.BlockMVC;
using Matching.Board.View;
using UnityEngine;
using Zenject;

namespace Matching.Board
{
    public class BoardFactory
    {       
        [Inject] private readonly GameObject _mainContainer;
        
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LocalResoursesManager _localResoursesManager;
        
        private BoardController _board;
        public BoardController Create()
        {
            BoardView prefab = _localResoursesManager.LoadAssetByPath<BoardView>("Prefabs/Board/BoardView");

            if(prefab == null) return null;

            BoardView boardView = _container.InstantiatePrefabForComponent<BoardView>(prefab, _mainContainer.transform);
            BoardController boardController = _container.Instantiate<BoardController>();
            boardController.ApplyView(boardView);
            boardController.Init();
            
            return boardController;
        }
    }
}