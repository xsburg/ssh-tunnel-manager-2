// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   Class2.cs
// </summary>
// ***********************************************************************

using System;
using System.Runtime.Serialization;

namespace STM.Core
{
    [Serializable]
    public class EncryptedStorageException : ClientException
    {
        public EncryptedStorageException()
        {
        }

        public EncryptedStorageException(string message) : base(message)
        {
        }

        public EncryptedStorageException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EncryptedStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}