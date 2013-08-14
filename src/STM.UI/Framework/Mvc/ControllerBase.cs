namespace STM.UI.Framework.Mvc
{
    public class ControllerBase<TView>
    {
        protected TView View { get; private set; }

        public void Register(TView view)
        {
            this.View = view;
            this.OnViewRegister();
        }

        protected virtual void OnViewRegister()
        {
        }
    }
}