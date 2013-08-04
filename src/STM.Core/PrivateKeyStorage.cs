// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   PrivateKeyStorage.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace STM.Core
{
    public static class PrivateKeyStorage
    {
        private static readonly List<PrivateKey> Cache = new List<PrivateKey>();

        public static void CleanUpGarbage()
        {
            foreach (var key in Cache)
            {
                key.Dispose();
            }

            Cache.Clear();
        }

        public static PrivateKey Create(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            var key = Cache.FirstOrDefault(c => c.Content.Equals(content));
            if (key != null)
            {
                return key;
            }

            Cache.Add(key = new PrivateKey(content));
            return key;
        }

        public static void Delete(string content)
        {
            var key = Cache.FirstOrDefault(c => c.Content.Equals(content));
            if (key == null)
            {
                return;
            }

            key.Dispose();
            Cache.Remove(key);
        }
    }
}
