// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MainFormActionsViewModel.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Forms.MainForm
{
    public class MainFormActionsViewModel
    {
        public bool CanCloseConnection { get; set; }
        public bool CanEditConnectionInfo { get; set; }
        public bool CanOpenConnection { get; set; }
        public bool CanSave { get; set; }
    }
}
