// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   UserSettings.cs
// </summary>
// ***********************************************************************

using System;

namespace STM.Core
{
    [Serializable]
    public class UserSettings
    {
        public UserSettings()
        {
            this.RestartLostConnections = true;
            this.LostConnectionRestartInterval = TimeSpan.FromSeconds(30);
            this.AttemptsToRestartLostConnection = 3;
            this.NeverStopTryingToRestartLostConnections = true;
            this.PeriodicallyRestartConnectionsThatHasWarnings = false;
            this.WarningConnectionRestartInterval = TimeSpan.FromMinutes(5);
        }

        public bool RestartLostConnections { get; set; }
        public TimeSpan LostConnectionRestartInterval { get; set; }
        public int AttemptsToRestartLostConnection { get; set; }
        public bool NeverStopTryingToRestartLostConnections { get; set; }
        public bool PeriodicallyRestartConnectionsThatHasWarnings { get; set; }
        public TimeSpan WarningConnectionRestartInterval { get; set; }
    }
}