using Code.MVC;
using UnityEngine;

namespace Matching.Board.Cell
{
    public class CellModel : Model
    {
        public int CellRowIndex;
        public int CellColumnIndex;
        public int Index;
        public bool IsVisible = false;
        public bool IsEmpty = false;
        public Vector3 Position;
    }
}