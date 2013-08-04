// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ExceptionExtensions.cs
// </summary>
// ***********************************************************************

using System;
using System.Text;

namespace STM.Core.Util
{
    public static class ExceptionExtensions
    {
        /// <summary>
        ///   Combining provided exception messages and all of the inner exception messages into one message.
        /// </summary>
        public static string GetDisplayMessage(this Exception ex)
        {
            var sb = new StringBuilder(ex.Message.Trim('.', ' ') + ".");
            while ((ex = ex.InnerException) != null)
            {
                sb.Append(" " + ex.Message.Trim('.', ' ') + ".");
            }

            return sb.ToString();
        }
    }
}