using Code.MVC;
using DG.Tweening;
using Matching.BlockMVC;
using UnityEngine;

namespace Matching.Board.Cell
{
    public class CellController : Controller<CellView, CellModel>
    {        
        private BlockController _blockController = null;
        
        public void Initialize(int index, bool isVisible, Vector3 position)
        {
            View.Initialize(index, isVisible, position);
            View.SetVisible();
        }

        /*
       * y - column; x - row
       */
        public void SetRowColumnIndex(int rIndex, int cIndex)
        {
            Model.CellRowIndex = rIndex;
            Model.CellColumnIndex = cIndex;
        }
        
        public void SetInitCellWithAnimation(int x, int y)
        {  
            float delayBetweenCells = 0.1f;
            float animationDuration = 0.4f;
            float initialDelay = 0f;

            View.transform.localScale = Vector3.zero;
            View.gameObject.SetActive(false);
        
            float delay = initialDelay + (x + y) * delayBetweenCells;
        
            DOVirtual.DelayedCall(delay, () =>
            {
                View.gameObject.SetActive(true);
                View.transform.DOScale(Vector3.one, animationDuration)
                    .SetEase(Ease.OutBack);
            });
        }

        public void SetBlockController(BlockController blockController)
        {
            _blockController = blockController;
            Model.IsEmpty = false;
        }
        
        public RectTransform GetViewRectTransform()
        {
            return View.GetComponent<RectTransform>(); 
        }
        
        public Vector3 GetPosition(float offset = 0f)
        {
            return new Vector3(Model.Position.x, Model.Position.y + offset, 0f);
        }

        public void DestroyBlock()
        { 
            _blockController?.Clear();
            _blockController = null;
            Model.IsEmpty = true;
        }

        public void ClearCellDataBlock()
        {
            _blockController = null;
            Model.IsEmpty = true;
        }

        public bool IsVisibleAndEmpty()
        {
            return Model.IsVisible && Model.IsEmpty;
        }       
        
        public void Clear()
        {
            if(_blockController != null)
                _blockController.Clear();
            View.Clear();
        }
        
        public BlockController GetCellBlock() => _blockController;
        public CellModel CellModel => Model;
    }
}