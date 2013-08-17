namespace STM.UI.Framework.Mvc
{
    public interface IForm<TController> : IView<TController> where TController : class
    {
        void Show();
        void Close();
    }
}