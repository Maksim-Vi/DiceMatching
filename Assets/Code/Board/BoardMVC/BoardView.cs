using Code.Board;
using Code.MVC;
using CustomInput;
using UnityEngine;
using Zenject;

namespace Matching.BlockMVC
{
    public class BoardView : View<BoardModel>
    {
        [SerializeField] private GameObject gridObject;
        [SerializeField] private GameObject blockObject;
        
        [Inject] private readonly InputHandler _inputHandler;
        [Inject] private  SwipeHandler _swipeHandler;
        
        protected override void OnApplyModel(BoardModel model)
        {
            base.OnApplyModel(model);
            _swipeHandler.Subscribe();
        }

        private void Update()
        {
            _swipeHandler.OnUpdate();
        }
        
        public void Clear()
        {
            _swipeHandler.Unsubscribe();
            Close();
        }

        public SwipeHandler SwipeHandler => _swipeHandler;
        public GameObject BlockObject => blockObject;
        public GameObject GridObject => gridObject;
    }
}