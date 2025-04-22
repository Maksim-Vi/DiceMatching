using System;
using System.Threading.Tasks;
using Code.MVC;
using Matching.Block;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Matching.BlockMVC
{
    public class BlockController : Controller<BlockView, BlockModel>
    {
        public void InitColor()
        {
            BlockColor color = (BlockColor)Random.Range(0, 4);
            Model.Color = color;
            SetColor();
        }

        public void SetPosition(Vector3 pos)
        {
            View.GetComponent<RectTransform>().anchoredPosition = pos;
        }
        
        public async Task MoveToAsync(Vector3 endPos, float speed = 0.6f)
        {
             Vector3 sPosition = View.GetComponent<RectTransform>().anchoredPosition;
             await View.MoveStartEndAsync(sPosition, endPos, speed);
        } 
        
        public async Task MoveStartEndAsync(Vector3 startPos, Vector3 endPos, float speed = 0.6f)
        {
            await View.MoveStartEndAsync(startPos, endPos, speed);
        } 
        
        public void MoveTo(Vector3 startPos, Action callback = null)
        {
            View.MoveTo(startPos, callback);
        }        
        
        public void MoveStartEnd(Vector3 startPos, Vector3 endPos, Action callback)
        {
            View.MoveStartEnd(startPos, endPos, callback);
        }
        
        private void SetColor()
        {
            Color32 color = new Color32();
            switch (Model.Color)
            {
                case BlockColor.Green:
                {           
                    color = new Color32(85, 233, 61, 255);
                    break;
                } 
                case BlockColor.Blue:
                {           
                    color = new Color32(61, 65, 233, 255);
                    break;
                }
                case BlockColor.Red:
                {           
                    color = new Color32(233, 61, 67, 255);
                    break;
                }
                case BlockColor.Yellow:
                {           
                    color = new Color32(233, 218, 61, 255);
                    break;
                }
            }

            View.SetColor(color);
        }

        public void Clear()
        {
            View.Clear();
        }
        
        public BlockColor GetColor() => Model.Color;
    }
}