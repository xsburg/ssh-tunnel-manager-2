// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ClientException.cs
// </summary>
// ***********************************************************************

using System;
using System.Runtime.Serialization;

namespace STM.Core
{
    [Serializable]
    public class ClientException : Exception
    {
        public ClientException()
        {
        }

        public ClientException(string message) : base(message)
        {
        }

        public ClientException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ClientException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
