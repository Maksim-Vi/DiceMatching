namespace Code.MVC
{
    public class Controller<TView, TModel> : IController
        where TView : UnityEngine.Component, IView
        where TModel : class, IModel, new()
    {
        protected TView View { get; private set; }
        
        protected TModel Model { get; }
        
        protected Controller()
        {
            Model = new TModel();
        }
        
        public void ApplyView(IView view)
        {
            View = view as TView;
            View.ApplyModel(Model);
            OnApplyView(View);
        }

        public void Close()
        {
            if (View != null)
            {
                OnCloseView(View);
            }
        }

        protected virtual void OnApplyView(TView view)
        {
        }

        protected virtual void OnCloseView(TView view)
        {
        }
    }
}