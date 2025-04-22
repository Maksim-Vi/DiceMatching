using System.Collections.Generic;
using Matching.Block;
using Matching.BlockMVC;
using Matching.Board.Cell;
using UnityEngine;

namespace Matching
{
    public static class GridUtils
    {
        public static T[,] GetColumns<T>(T[,] grid, int start, int? end = null, bool fromEnd = false)
        {
            int cols  = grid.GetLength(0);
            int rows = grid.GetLength(1);

            int actualStart = fromEnd ? cols + start : start;
            int actualEnd = end.HasValue ? (fromEnd ? cols + end.Value : end.Value) : actualStart;

            if (actualStart < 0 || actualStart >= cols || actualEnd < 0 || actualEnd >= cols) return null;
            int columnCount = actualEnd - actualStart + 1;

            if (columnCount <= 0) return null;
            
            T[,] result = new T[columnCount, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[j, i] = grid[j, actualStart + i];
                }
            }

            return result;
        }
        
        public static T[,] GetRows<T>(T[,] grid, int start, int? end = null, bool fromEnd = false)
        {
            int cols  = grid.GetLength(0);
            int rows = grid.GetLength(1);
            
            int actualStart = fromEnd ? rows + start : start;
            int actualEnd = end.HasValue ? (fromEnd ? rows + end.Value : end.Value) : actualStart;

            if (actualStart < 0 || actualStart >= rows || actualEnd < 0 || actualEnd >= rows) return null;
            int rowCount = actualEnd - actualStart + 1;
           
            if (rowCount <= 0) return null;

            T[,] result = new T[cols, rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = grid[actualStart + j, i];
                }
            }

            return result;
        }
        
        public static List<T> GetColumnAsList<T>(T[,] grid, int colIndex)
        {
            int col = grid.GetLength(0);
            int row = grid.GetLength(1);
            
            if(colIndex < 0 || colIndex >= col) return null;
            
            var list = new List<T>();

            for (int i = 0; i < row; i++)
            {
                list.Add(grid[colIndex, i]);
            }

            return list;
        }
        public static List<CellController> GetInitialMatches(CellController[,] _grid, int _columns, int _rows)
        {
            bool IsSameColorMatch(CellController a, BlockColor color)
            {
                if (a == null || a.IsVisibleAndEmpty())
                    return false;

                var ab = a.GetCellBlock();
                return ab != null &&  ab.GetColor() == color;
            }
            
            var toDestroy = new List<CellController>();
        
            for (int x = 0; x < _columns; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    bool isLeft = false;
                    bool isRight = false;
                    bool isTop = false;
                    bool isBottom = false;
                    
                    var centerCell = _grid[x, y];
                    if (centerCell == null || centerCell.IsVisibleAndEmpty()) continue;

                    var centerBlock = centerCell.GetCellBlock();
                    if (centerBlock == null) continue;

                    var color = centerBlock.GetColor();

                    // left right
                    if (x > 0 && x < _columns - 1)
                    {
                        var left = _grid[x - 1, y];
                        var right = _grid[x + 1, y];

                        if (IsSameColorMatch(left, color))
                        {
                            isLeft = true;
                            toDestroy.Add(left);
                        } 
                        
                        if (IsSameColorMatch(right, color))
                        {
                            isRight = true;
                            toDestroy.Add(right);
                        }
                    }

                    // top bottom
                    if (y > 0 && y < _rows - 1)
                    {
                        var down = _grid[x, y - 1];
                        var up = _grid[x, y + 1];

                        if (IsSameColorMatch(down, color))
                        {
                            isBottom = true;
                            toDestroy.Add(down);
                        } 
                        
                        if (IsSameColorMatch(up, color))
                        {
                            isTop = true;
                            toDestroy.Add(up);
                        }
                    }
                    
                    if (isLeft || isRight || isTop || isBottom)
                        toDestroy.Add(centerCell);
                }
            }
            
            return toDestroy;
        }
        public static List<CellController> GetMatchesByRow(CellController centerCell, BlockController block, CellController[,] _grid, int _columns, int _rows)
        {
            var matched = new List<CellController>();
            int col = centerCell.CellModel.CellColumnIndex;
            int row = centerCell.CellModel.CellRowIndex;
            BlockColor color = block.GetColor();

            void TryAdd(int x, int y, BlockColor color)
            {
                if (x < 0 || x >= _columns || y < 0 || y >= _rows) return;
                var cell = _grid[x, y];
                var cellBlock = cell?.GetCellBlock();
                var cellBlockColor = cellBlock?.GetColor();
        
                if (cell != null && !cell.IsVisibleAndEmpty() && cellBlockColor != null && cellBlockColor == color)
                    matched.Add(cell);
            }

            TryAdd(col - 1, row, color);
            TryAdd(col + 1, row, color);
            TryAdd(col, row - 1, color);

            return matched;
        }
        public static T GetEmptyCellColumnInGridList<T>(List<T> gridList, bool checkHalfCells = false) where T : CellController
        {
            if (gridList.Count == 0) return null;
        
            CellController emptyItem = null;
            for (int i = 0; i < gridList.Count; i++)
            {
                bool isLessBlocksThenHalf = checkHalfCells ? gridList.Count / 2 >= i : true;
                if (gridList[i].IsVisibleAndEmpty() && isLessBlocksThenHalf)
                {
                    emptyItem = gridList[i];
                    break;
                }
            }
        
            return (T)emptyItem;
        }
        public static List<T> GetRandomColumn<T>(T[,] grid)
        {
            if (grid.Length == 0) return null;

            var columnIndex = Random.Range(0, grid.GetLength(0));
            var columnList = GetColumnAsList<T>(grid, columnIndex);
            
            return columnList;
        }
        // public static List<T> GetMiddleColumn<T>(T[,] grid)
        // {            
        //     if (grid.Length == 0) return null;
        //     
        //     int cols = grid.GetLength(0);
        //     int rows = grid.GetLength(1);
        //     
        //     int middleColumnIndex = Mathf.RoundToInt(cols / 2);
        //     
        //     List<T> middleColumn = new List<T>();
        //
        //     for (int i = 0; i < rows; i++)
        //     {
        //         middleColumn.Add(grid[middleColumnIndex, i]);
        //     }
        //     
        //     return middleColumn;
        // }
    }
}