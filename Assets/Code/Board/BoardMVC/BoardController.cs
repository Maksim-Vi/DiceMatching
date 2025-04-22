using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Board;
using Code.Cell;
using Code.MVC;
using CustomInput;
using Matching.Block;
using Matching.BlockMVC;
using Matching.Board.Cell;
using UnityEngine;
using Zenject;

namespace Matching.Board.View
{
    public class BoardController : Controller<BoardView, BoardModel>
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly GridSettings _gridSettings;
        [Inject] private readonly CellFactory _cellFactory;
        [Inject] private readonly BoardManager _boardManager;
        [Inject] private readonly BlockFactory _blockFactory;
        [Inject] private readonly LocalResoursesManager _localResoursesManager;

        private CellController[,] _grid;
        private int _rows, _columns;
        private MoveBlock _moveBlockData;
        private List<Row> _map;
        
        private bool _isInitPhase = false;
        private bool _isGameOver = false;
        private bool _isProcessing = false;
        private bool _isCanMove = false;
        
        #region Init
        public void Init()
        {
            _map = _gridSettings.GridMap;
            _rows = _map.Count;
            _columns = _map.Max(row => row.Cells.Count);
            View.SwipeHandler?.SwipeHandlerInit(this);

            CreateGrid();
        }
        
        private void CreateGrid()
        {
            _grid = new CellController[_columns, _rows];

            RectTransform rectTransform = View.GridObject.GetComponent<RectTransform>();
            float X = rectTransform.rect.width - (_gridSettings.CellSize.x / 2f) - _gridSettings.Offset.x;
            float Y = _gridSettings.CellSize.y / 2f + _gridSettings.Offset.y;

            int index = 0;
            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _columns; x++)
                {
                    bool isVisible = _map[y].Cells[x] != 0;
                    float posX = X - x * (_gridSettings.CellSize.x + _gridSettings.Offset.x);
                    float posY = Y + y * (_gridSettings.CellSize.y + _gridSettings.Offset.y);
                    Vector3 pos = new Vector3(posX, posY, 0f);

                    var cell = _cellFactory.CreateCellController(View.GridObject, index++, isVisible, pos);
                    if (cell == null) continue;

                    RectTransform cellRect = cell.GetViewRectTransform();
                    cellRect.anchoredPosition = pos;
                    cellRect.sizeDelta = _gridSettings.CellSize;

                    _grid[x, y] = cell;
                    cell.SetRowColumnIndex(y, x);
                    cell.SetInitCellWithAnimation(x, y);
                }
            }
        }

        public async void InitSpawnBlocks()
        {
            _isInitPhase = true;
            
            List<Task> spawnTasks = new List<Task>();

            for (int i = 0; i < _gridSettings.maxStartBlocksSpawn; i++)
            {
                var task = SpawnInitialBlocks();
                spawnTasks.Add(task);
                await Task.Delay(200);
            }

            await Task.WhenAll(spawnTasks);
            
            await CheckAndDestroyInitialMatchesAsync();
        }
        
        private async Task SpawnInitialBlocks()
        {
            var cellList = GridUtils.GetRandomColumn<CellController>(_grid);
            var startCell = cellList[^1];
            var targetCell = GridUtils.GetEmptyCellColumnInGridList(cellList, true);

            if (targetCell == null)
            {
                await SpawnInitialBlocks(); // Повторити, якщо не знайшлося місце
                return;
            }

            var block = _blockFactory.CreateBlockController(View.BlockObject);
            _moveBlockData = new MoveBlock { block = block, startCell = startCell, endEmptyCell = targetCell };
            await MoveBlock(_moveBlockData, false);
        }
        #endregion
        
        public void SwipeHandler(int direction)
        {
            if (_isGameOver || _isInitPhase || _isProcessing || !_moveBlockData.isCreated) return;
            
            int index = _moveBlockData.indexColumn + direction;
            if (index >= 0 && index <= _columns - 1) // left = 1 right = -1
            {
                UpdateSpawnBlock(_moveBlockData.indexColumn + direction);
            }
        }
        public void StartGame()
        {
            Debug.Log("=== START GAME ===");
            
            _isGameOver = false;
            _isCanMove = true;
            
            DropWithTimer();
        }
        
        private async Task<MoveBlock> GetSpawnBlock()
        {
            int middleColumnIndex = Mathf.RoundToInt(_columns / 2);
            var cellList = GridUtils.GetColumnAsList<CellController>(_grid, middleColumnIndex);

            var startCell = cellList[^1];
            var targetCell = GridUtils.GetEmptyCellColumnInGridList(cellList, false);

            if (targetCell == null)
            {
                return new MoveBlock { isCreated = false };
            }

            var block = _blockFactory.CreateBlockController(View.BlockObject);
            Vector3 spawnPos = startCell.GetPosition(_gridSettings.CellSize.y + 20f);
            block.SetPosition(spawnPos);

            var moveBlockData = new MoveBlock
            {
                isCreated = true,
                block = block,
                startCell = startCell,
                endEmptyCell = targetCell,
                indexColumn = middleColumnIndex
            };

            await Task.Yield();
            return moveBlockData;
        }

        private async void UpdateSpawnBlock(int indexColumn)
        {
            var cellList = GridUtils.GetColumnAsList<CellController>(_grid, indexColumn);
            
            var startCell = cellList[^1];
            var targetCell = GridUtils.GetEmptyCellColumnInGridList(cellList, false);

            if (targetCell == null) return;
            var currentPos = _moveBlockData.startCell.GetPosition(_gridSettings.CellSize.y + 20f);
            var nextPos = startCell.GetPosition(_gridSettings.CellSize.y + 20f);

            _moveBlockData.indexColumn = indexColumn;
            _moveBlockData.startCell = startCell;
            _moveBlockData.endEmptyCell = targetCell;
           
            await _moveBlockData.block.MoveStartEndAsync(currentPos, nextPos, 0.3f);
        }
        
        private async void DropWithTimer()
        {
            if (_isGameOver) return;
            
            _isCanMove = true;
            _moveBlockData = await GetSpawnBlock();
            
            await Task.Delay(2000);
            
            _isCanMove = false;
            
            Debug.Log("Drop");
            
            await Drop(_moveBlockData);
        }

        private async Task Drop(MoveBlock moveBlockData)
        {
            if (_isGameOver || _isProcessing) return;
            _isProcessing = true;
            
            if (!moveBlockData.isCreated)
            {
                GameOver();
                return;
            }
            
            await MoveBlock(moveBlockData, true);
            _moveBlockData = new MoveBlock();
            _isProcessing = false;
        }

        private async Task MoveBlock(MoveBlock moveBlockData, bool isCheckIfterDrop = false)
        {
            var startPos = moveBlockData.startCell.GetPosition(_gridSettings.CellSize.y + 20f);
            var endPos = moveBlockData.endEmptyCell.GetPosition();

            moveBlockData.endEmptyCell.SetBlockController(moveBlockData.block);
            await moveBlockData.block.MoveStartEndAsync(startPos, endPos);

            if (isCheckIfterDrop)
            {
                var matched = GridUtils.GetMatchesByRow(moveBlockData.endEmptyCell, moveBlockData.block, _grid, _columns, _rows);
                if (matched.Count > 0)
                {
                    matched.Add(moveBlockData.endEmptyCell);
                    await CheckAndDestroyMatchesRecursive(matched);
                }
                else
                {
                    DropWithTimer();
                }
            }
        }
        
        private void GameOver()
        {
            Debug.Log("======GAME OVER======");
            _isGameOver = true;
            _moveBlockData = new MoveBlock();
        }

        #region CheckerAndRefuil
        private async Task CheckAndDestroyInitialMatchesAsync()
        {
            var toDestroy = GridUtils.GetInitialMatches(_grid, _columns, _rows);
            await CheckAndDestroyMatchesRecursive(toDestroy);
        }
        private async Task CheckAndDestroyMatchesRecursive(List<CellController> matchedCells)
        {
            
            if (matchedCells == null || matchedCells.Count == 0)
            {
                Debug.Log("Skip Matches");
                
                if (_isInitPhase)
                {
                    _isInitPhase = false;
                    StartGame();
                }
                else
                {
                    DropWithTimer();
                }
                return;
            }
            
            Debug.Log("Check Matches after throw");

            var affectedColumns = matchedCells.Select(c => c.CellModel.CellColumnIndex).Distinct().ToList();

            foreach (var cell in matchedCells)
                cell.DestroyBlock();

            await Task.Delay(100);

            foreach (var col in affectedColumns)
                await RefillGridColumnAsync(col);

            var newMatches = GridUtils.GetInitialMatches(_grid, _columns, _rows);
            await CheckAndDestroyMatchesRecursive(newMatches);
        }
        private async Task RefillGridColumnAsync(int columnIndex)
        {
            for (int i = 0; i < _rows; i++)
            {
                if (_grid[columnIndex, i].IsVisibleAndEmpty())
                {
                    for (int j = i + 1; j < _rows; j++)
                    {
                        if (!_grid[columnIndex, j].IsVisibleAndEmpty())
                        {
                            var block = _grid[columnIndex, j].GetCellBlock();
                            _grid[columnIndex, i].SetBlockController(block);
                            _grid[columnIndex, j].ClearCellDataBlock();

                            Vector3 newPos = _grid[columnIndex, i].GetPosition();
                            await block.MoveToAsync(newPos, 0.3f);
                            break;
                        }
                    }
                }
            }
        }
        #endregion
    }

    public struct MoveBlock
    {
        public bool isCreated;
        public BlockController block;
        public CellController startCell;
        public CellController endEmptyCell;
        public int indexColumn;
    }
}