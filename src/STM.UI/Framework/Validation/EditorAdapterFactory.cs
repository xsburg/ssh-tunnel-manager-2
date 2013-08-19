// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EditorAdapterFactory.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using STM.Core.Util;

namespace STM.UI.Framework.Validation
{
    public static class EditorAdapterFactory
    {
        private static readonly Dictionary<Type, Type> EditorAdapterTypeMap = new Dictionary<Type, Type>();

        static EditorAdapterFactory()
        {
            RegisterAdaptersFrom(Assembly.GetExecutingAssembly());
        }

        public static IEditorAdapter Create(Control control)
        {
            var editorAdapterType = EditorAdapterTypeMap.GetValueOrDefault(control.GetType());
            if (editorAdapterType == null)
            {
                throw new NotSupportedException(
                    string.Format("Failed to find an EditorAdapter for the control {0}.", control.GetType().FullName));
            }

            var adapter = (IEditorAdapter)Activator.CreateInstance(editorAdapterType, control);
            return adapter;
        }

        public static void RegisterAdaptersFrom(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            foreach (var type in assembly.GetTypes())
            {
                EditorAdapterAttribute editorAdapterAttr;
                if (!typeof(IEditorAdapter).IsAssignableFrom(type) ||
                    typeof(IEditorAdapter) == type ||
                    (editorAdapterAttr = type.GetCustomAttribute<EditorAdapterAttribute>(false)) == null)
                {
                    continue;
                }

                EditorAdapterTypeMap[editorAdapterAttr.Type] = type;
            }
        }
    }
}
