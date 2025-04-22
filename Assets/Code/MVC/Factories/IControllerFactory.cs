namespace Code.MVC
{
    public interface IControllerFactory
    {
        TController CreateController<TController>()
            where TController : class, IController, new();
    }
}