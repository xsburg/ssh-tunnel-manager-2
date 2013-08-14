namespace STM.UI.Framework.Mvc
{
    public interface IDialog<TController> : IView<TController>
    {
        bool? ShowDialog();
        void Close(bool result);
    }
}