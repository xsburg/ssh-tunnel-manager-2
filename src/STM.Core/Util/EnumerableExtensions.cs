// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EnumerableExtensions.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;

namespace STM.Core.Util
{
    public static class EnumerableExtensions
    {
        public static void Apply<T>(this IEnumerable<T> target, Action<T> action)
        {
            foreach (T item in target)
            {
                action(item);
            }
        }

        public static void Apply(this IEnumerable target, Action<object> action)
        {
            foreach (object item in target)
            {
                action(item);
            }
        }
    }
}
