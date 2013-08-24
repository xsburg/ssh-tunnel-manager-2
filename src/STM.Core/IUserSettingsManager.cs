// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IUserSettingsManager.cs
// </summary>
// ***********************************************************************

namespace STM.Core
{
    public interface IUserSettingsManager
    {
        string FileName { get; set; }
        string Password { get; set; }
        UserSettings Settings { get; }
        void Save();
    }
}
