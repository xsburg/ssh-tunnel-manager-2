// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ExceptionHandler.cs
// </summary>
// ***********************************************************************

using System;
using Ninject.Extensions.Logging;
using STM.UI.Annotations;
using STM.UI.Framework;

namespace STM.UI
{
    public class ExceptionHandler
    {
        private readonly ILogger logger;
        private readonly IMessageBoxService messageBoxService;

        public ExceptionHandler([NotNull] ILogger logger, [NotNull] IMessageBoxService messageBoxService)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (messageBoxService == null)
            {
                throw new ArgumentNullException("messageBoxService");
            }
            
            this.logger = logger;
            this.messageBoxService = messageBoxService;
        }

        public void HandleException(Exception ex)
        {
            this.logger.Error(ex, ex.Message);
            this.messageBoxService.Error(ex.Message);
        }
    }
}
