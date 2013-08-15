namespace STM.UI.Framework.Mvc
{
    public class ControllerBase<TView>
    {
        protected TView View { get; private set; }

        public void Register(TView view)
        {
            this.View = view;
            this.OnViewRegistered();
        }

        protected virtual void OnViewRegistered()
        {
        }
    }
}