using CustomInput;
using Matching.Board.View;
using Zenject;

namespace Code.Board
{
    public class SwipeHandler : SwipeHandlerBase
    {
        private BoardController _boardController;
        public SwipeHandler(InputHandler inputHandler) : base(inputHandler)
        {
        }
        
        public void SwipeHandlerInit(BoardController boardController)
        {
            base.Init();
            
            _boardController = boardController;
        }
        
        protected override void MoveLeft(InputAction action)
        {
            _boardController?.SwipeHandler(1);
        }

        protected override void MoveRight(InputAction action)
        {
            _boardController?.SwipeHandler(-1);
        }
        
        protected override void MoveFinger(InputAction action)
        {
            
        }
    }
}