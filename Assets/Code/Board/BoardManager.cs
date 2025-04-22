using Code.Cell;
using Matching.Block;
using Matching.Board.View;

namespace Matching.Board
{
    public class BoardManager 
    {
        private readonly BlockFactory _blockFactory;
        private readonly BoardFactory _boardFactory;
        private readonly CellFactory _cellFactory;
        private BoardController _boardController;
        
        public BoardManager(CellFactory cellFactory, BlockFactory blockFactory, BoardFactory boardFactory)
        {
            _blockFactory = blockFactory;
            _boardFactory = boardFactory;
            _cellFactory = cellFactory;
        }
        public void Create()
        {
            _boardController = _boardFactory.Create();
            _boardController.InitSpawnBlocks();
            
            //_boardController.StartGame();
        }
    }
}