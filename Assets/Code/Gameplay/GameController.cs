using Matching.Board;
using UnityEngine;

namespace Matching
{
    public class GameController
    {        
        private readonly GridSettings _gridSettings;
        private readonly BoardManager _boardManager;
        private readonly GameObject _mainContainer;

        public GameController(GridSettings gridSettings, BoardManager boardManager, GameObject mainContainer)
        {
            _gridSettings = gridSettings;
            _boardManager = boardManager;
            _mainContainer = mainContainer;
            
            StartGame();
        }

        private void StartGame()
        {
            Debug.Log("GameController StartGame");

            _boardManager.Create();
        }
    }
}