using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Matching.Board
{
    [CreateAssetMenu(menuName = "Config/Grid Settings")]
    public class GridSettings : ScriptableObject
    {
        [Header("Grid Map")]
        public List<Row> GridMap = new();

        [Header("Grid Config")] public Vector2 CellSize = new Vector2(150f, 150f);
        public Vector2 Offset = new Vector2(0f, 0f);
        
        // public int SpacingTop = 0; 
        // public int SpacingLeft = 0; 
        // public int SpacingRight = 0; 
        // public int SpacingBottom = 0;
        
        [Header("Grid Settings")]
        public int maxStartBlocksSpawn = 10;
    }
    
    [System.Serializable]
    public class Row
    {
        public List<int> Cells = new(); // Change int on enum CellType { None, Basic, Bonus, Blocked } like type of cell
    }
}