namespace STM.UI.Framework.Mvc
{
    public interface IDialog<TController> : IView<TController> where TController : class
    {
        bool? ShowDialog();
        void Close(bool result);
    }
}