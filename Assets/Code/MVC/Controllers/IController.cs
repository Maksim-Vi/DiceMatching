namespace Code.MVC
{
    public interface IController
    {
        void ApplyView(IView view);
        
        void Close();
    }
}