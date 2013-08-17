// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EditorAdapterAttribute.cs
// </summary>
// ***********************************************************************

using System;

namespace STM.UI.Framework.Validation
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class EditorAdapterAttribute : Attribute
    {
        public EditorAdapterAttribute(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            this.Type = type;
        }

        public Type Type { get; private set; }
    }
}
