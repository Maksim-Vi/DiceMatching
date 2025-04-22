using System;
using UnityEngine;

namespace Code.MVC
{
    public class View<TModel> : MonoBehaviour, IView
        where TModel : class, IModel
    {
        protected TModel Model { get; private set; }       
        
        private void Start()
        {
            OnStartEvent();
        }

        private void OnDestroy()
        {
            OnDestroyEvent();
        }

        public void ApplyModel(IModel model)
        {
            Model = model as TModel;
            OnApplyModel(Model);
        }

        protected virtual void OnApplyModel(TModel model)
        {
        }

        protected virtual void OnStartEvent()
        {
        }        
        
        protected virtual void OnDestroyEvent()
        {
        }

        protected virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}