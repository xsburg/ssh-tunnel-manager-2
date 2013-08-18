namespace STM.UI.Framework
{
    public interface IStandardDialogService
    {
        /// <summary>
        /// Display standard open file dialog and return the file name selected by the user or null if the operation has been canceled.
        /// </summary>
        /// <param name="filter">Files filter in format "Storage files|*.stgx|All files|*.*"</param>
        /// <param name="defaultExt">Default file extension like "*.stgx" or null</param>
        /// <returns>The name of file selected by the user or null if the operation has been canceled.</returns>
        string ShowOpenFileDialog(string filter, string defaultExt = null);

        /// <summary>
        /// Display standard save file dialog and return the file name selected by the user or null if the operation has been canceled.
        /// </summary>
        /// <param name="filter">Files filter in format "Storage files|*.stgx|All files|*.*"</param>
        /// <param name="defaultExt">Default file extension like "*.stgx" or null</param>
        /// <returns>The name of file selected by the user or null if the operation has been canceled.</returns>
        string ShowSaveFileDialog(string filter, string defaultExt = null);
    }
}