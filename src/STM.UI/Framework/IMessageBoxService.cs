// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IUserMessageService.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Framework
{
    public interface IMessageBoxService
    {
        bool AskYesNo(string text, string title = null, bool defaultValue = true);
        bool? AskYesNoCancel(string text, string title = null, bool? defaultValue = null);
        void Error(string text, string title = null);
        void Info(string text, string title = null);
        void Warn(string text, string title = null);
    }
}
