namespace CustomInput
{
    public class SwipeHandlerBase
    {
        public InputHandler inputHandler => _inputHandler;
        private InputHandler _inputHandler;

        public SwipeHandlerBase(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        protected virtual void Init()
        {

        }

        public virtual void Subscribe()
        {
            _inputHandler.SubscribeToInputAction(InputAction.MoveLeft, MoveLeft);
            _inputHandler.SubscribeToInputAction(InputAction.MoveRight, MoveRight);
            _inputHandler.SubscribeToInputAction(InputAction.MoveFinger, MoveFinger);
        }

        public virtual void Unsubscribe()
        {
            if (_inputHandler != null)
            {
                _inputHandler.UnsubscribeFromInputAction(InputAction.MoveLeft, MoveLeft);
                _inputHandler.UnsubscribeFromInputAction(InputAction.MoveRight, MoveRight);
                _inputHandler.UnsubscribeFromInputAction(InputAction.MoveFinger, MoveFinger);
            }
        }

        public virtual void OnUpdate()
        {
            _inputHandler.StartUpdate();
        }

    
        protected virtual void MoveLeft(InputAction action)
        {
           
        }

        protected virtual void MoveRight(InputAction action)
        {
           
        }
        
        protected virtual void MoveFinger(InputAction action)
        {
            
        }
    }
}