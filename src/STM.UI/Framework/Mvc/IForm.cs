namespace STM.UI.Framework.Mvc
{
    public interface IForm<TController> : IView<TController>
    {
        void Show();
        void Close();
    }
}