namespace CustomInput
{
    public interface IInputDevice
    {
        bool IsMoveForward();
        bool IsMoveBackward();
        bool IsMoveLeft();
        bool IsMoveRight();
        bool IsMoveMouse();
        bool IsMoveFinger();
    }
}