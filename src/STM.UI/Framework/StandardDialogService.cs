// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   StdDialogService.cs
// </summary>
// ***********************************************************************

using System.Windows.Forms;

namespace STM.UI.Framework
{
    public class StandardDialogService : IStandardDialogService
    {
        /// <summary>
        /// Display standard open file dialog and return the file name selected by the user or null if the operation has been canceled.
        /// </summary>
        /// <param name="filter">Files filter in format "Storage files|*.stgx|All files|*.*"</param>
        /// <param name="defaultExt">Default file extension like "*.stgx" or null</param>
        /// <returns>The name of file selected by the user or null if the operation has been canceled.</returns>
        public string ShowOpenFileDialog(string filter, string defaultExt = null)
        {
            var openFileDialog = new OpenFileDialog
                {
                    Filter = filter,
                    DefaultExt = defaultExt
                };
            return openFileDialog.ShowDialog() != DialogResult.OK
                ? null
                : openFileDialog.FileName;
        }

        /// <summary>
        /// Display standard save file dialog and return the file name selected by the user or null if the operation has been canceled.
        /// </summary>
        /// <param name="filter">Files filter in format "Storage files|*.stgx|All files|*.*"</param>
        /// <param name="defaultExt">Default file extension like "*.stgx" or null</param>
        /// <returns>The name of file selected by the user or null if the operation has been canceled.</returns>
        public string ShowSaveFileDialog(string filter, string defaultExt = null)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = filter,
                DefaultExt = defaultExt
            };
            return saveFileDialog.ShowDialog() != DialogResult.OK
                ? null
                : saveFileDialog.FileName;
        }
    }
}