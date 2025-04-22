using Zenject;

namespace Code.MVC
{
    public class ControllerFactory : IControllerFactory
    {
        [Inject] 
        private DiContainer _diContainer;
        
        public TController CreateController<TController>() where TController : class, IController, new()
        {
            var toastController = new TController();
            _diContainer.Inject(toastController);
            return toastController;
        }
    }
}