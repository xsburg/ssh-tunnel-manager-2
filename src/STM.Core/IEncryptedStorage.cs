﻿// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IEncryptedStorage.cs
// </summary>
// ***********************************************************************

using STM.Core.Data;

namespace STM.Core
{
    public interface IEncryptedStorage
    {
        EncryptedStorageParameters Parameters { get; set; }
        EncryptedStorageData Data { get; set; }
        void Read();
        void Save();
        bool Test(out string errorText);
    }
}
