// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   DictionaryExtensions.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;

namespace STM.Core.Util
{
    public static class DictionaryExtensions
    {
        public static T GetValueOrDefault<T>(this IDictionary<string, object> dictionary, string key, T defaultValue = default(T))
        {
            object value;
            if (dictionary.TryGetValue(key, out value) && value is T)
            {
                return (T)value;
            }

            return defaultValue;
        }

        public static TValue GetValueOrDefault<TValue, TKey>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }

            return defaultValue;
        } 
    }
}