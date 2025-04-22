using UnityEngine;
using UnityEngine.UI;
using Code.MVC;

namespace Matching.Board.Cell
{
    
    public class CellView : View<CellModel>
    {
        [SerializeField] private Image _image;
        private CellModel _cellModel;
        
        protected override void OnApplyModel(CellModel model)
        {
            base.OnApplyModel(model);
            
            Model.IsVisible = false;
            Model.IsEmpty = true;
        }
        
        public void Initialize(int index, bool isVisible,  Vector3 position)
        {
            Model.Index = index;
            Model.IsVisible = isVisible;
            Model.Position = position;
        }

        public void SetVisible()
        {
            _image.color = !Model.IsVisible 
                ? new Color(_image.color.r, _image.color.g, _image.color.b, 0f)
                : new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        }
        
        public void Clear()
        {
            Close();
        }
    }
}