using System;
using System.Threading.Tasks;
using Code.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Matching.BlockMVC
{
    public class BlockView : View<BlockModel>
    {
        [SerializeField] private Image _image;

        public void MoveTo(Vector2 position)
        {
            RectTransform target = gameObject.GetComponent<RectTransform>();
            target.DOMove(position, 1f).SetDelay(0.3f);
        } 
            
        public async Task MoveToAsync(Vector3 position)
        {
            RectTransform target = gameObject.GetComponent<RectTransform>();
            await target.DOMove(position, 0.3f).AsyncWaitForCompletion();
        }
        
        public void MoveTo(Vector2 sPosition, Action callback = null) 
        {
            RectTransform target = gameObject.GetComponent<RectTransform>();
            target.anchoredPosition = sPosition;
            target.DOMove(sPosition, 1f)
                  .SetDelay(0.3f)
                  .OnComplete(()=> callback?.Invoke());
        } 
        
        public void MoveStartEnd(Vector2 sPosition, Vector2 ePosition, Action callback) 
        {
            RectTransform target = gameObject.GetComponent<RectTransform>();
            target.anchoredPosition = sPosition;
            target.DOAnchorPos(ePosition, 0.4f)
                  .SetDelay(0.3f)
                  .OnComplete(()=> callback());
        }
        
        public async Task MoveStartEndAsync(Vector2 sPosition, Vector2 ePosition, float speed = 0.6f) 
        {
            RectTransform target = gameObject.GetComponent<RectTransform>();
            target.anchoredPosition = sPosition;
            
            await target.DOAnchorPos(ePosition, speed)
                .SetDelay(0.3f)
                .AsyncWaitForCompletion();
        }
        
        public void SetColor(Color32 color)
        {
            _image.color = color;
        }

        public void Clear()
        {
            Close();
        }
    }
}