// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ChangePasswordFormController.cs
// </summary>
// ***********************************************************************

using System;
using Ninject.Extensions.Logging;
using STM.Core;
using STM.UI.Annotations;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.ChangePassword
{
    public class ChangePasswordFormController : ControllerBase<IChangePasswordForm>
    {
        private readonly IUserSettingsManager userSettingsManager;
        private readonly IEncryptedStorage encryptedStorage;
        private readonly ExceptionHandler exceptionHandler;

        public ChangePasswordFormController(
            [NotNull] IUserSettingsManager userSettingsManager,
            [NotNull] IEncryptedStorage encryptedStorage,
            [NotNull] ExceptionHandler exceptionHandler,
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
            if (userSettingsManager == null)
            {
                throw new ArgumentNullException("userSettingsManager");
            }

            if (encryptedStorage == null)
            {
                throw new ArgumentNullException("encryptedStorage");
            }

            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            this.userSettingsManager = userSettingsManager;
            this.encryptedStorage = encryptedStorage;
            this.exceptionHandler = exceptionHandler;
        }

        public void Ok()
        {
            if (!this.View.DoValidate())
            {
                return;
            }

            try
            {
                var password = this.View.Collect();

                this.encryptedStorage.Parameters.Password = password;
                this.encryptedStorage.Save();

                if (!string.IsNullOrEmpty(this.userSettingsManager.Password))
                {
                    this.userSettingsManager.Password = password;
                }

                this.View.Close(true);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }
    }
}
