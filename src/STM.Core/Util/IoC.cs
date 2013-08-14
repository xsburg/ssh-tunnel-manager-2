// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IoC.cs
// </summary>
// ***********************************************************************

using Ninject;

namespace STM.Core.Util
{
    public static class IoC
    {
        public static IKernel Kernel { get; set; }

        /// <summary>
        ///     Gets an instance by type.
        /// </summary>
        /// <typeparam name="T">The type to resolve from the container.</typeparam>
        /// <returns>The resolved instance.</returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        ///     Gets an instance from the container using type and key.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <param name="key">The key to look up.</param>
        /// <returns>The resolved instance.</returns>
        public static T Get<T>(string key)
        {
            return Kernel.Get<T>(key);
        }
    }
}
