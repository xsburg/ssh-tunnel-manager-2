// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ValidationProvider.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using STM.Core.Util;
using STM.UI.Properties;

namespace STM.UI.Framework.Validation
{
    public class ValidationProvider
    {
        private static readonly Dictionary<Type, Type> EditorAdapterTypeMap = new Dictionary<Type, Type>();
        private readonly Dictionary<Control, ControlInfo> controls = new Dictionary<Control, ControlInfo>();

        static ValidationProvider()
        {
            RegisterAdaptersFrom(Assembly.GetExecutingAssembly());
        }

        public ValidationProvider()
        {
            this.ErrorProvider = new ErrorProvider
                {
                    BlinkStyle = ErrorBlinkStyle.NeverBlink,
                    Icon = Resources.CrossCircleIco
                };
        }

        public bool AllowAutoValidating { get; set; }
        public ErrorProvider ErrorProvider { get; private set; }

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

        public void SetValidationRule(Control control, ValidationRule rule)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }

            var info = this.controls.GetValueOrDefault(control);
            if (info == null)
            {
                var editorAdapterType = EditorAdapterTypeMap.GetValueOrDefault(control.GetType());
                if (editorAdapterType == null)
                {
                    throw new NotSupportedException(
                        string.Format("Failed to find an EditorAdapter for the control {0}.", control.GetType().FullName));
                }

                var adapter = (IEditorAdapter)Activator.CreateInstance(editorAdapterType, control);
                info = new ControlInfo(adapter);
                this.SubscribeValidatingEvent(control);
                this.controls[control] = info;
            }

            info.Rule = rule;
        }

        public bool Validate()
        {
            return this.controls.Values.Aggregate(true, (current, info) => this.DoValidate(info) && current);
        }

        private bool DoValidate(ControlInfo info)
        {
            var result = info.Rule.Validate(info.Adapter.EditValue);
            var errorText = result
                ? ""
                : info.Rule.ErrorText;
            this.ErrorProvider.SetError(info.Adapter.Control, errorText);
            return result;
        }

        private void SubscribeValidatingEvent(Control control)
        {
            control.Validating += this.Validating;
        }

        private void Validating(object sender, CancelEventArgs e)
        {
            if (!this.AllowAutoValidating)
            {
                return;
            }

            var info = this.controls.GetValueOrDefault((Control)sender);
            this.DoValidate(info);
        }

        private class ControlInfo
        {
            public ControlInfo(IEditorAdapter adapter)
            {
                if (adapter == null)
                {
                    throw new ArgumentNullException("adapter");
                }

                this.Adapter = adapter;
            }

            public IEditorAdapter Adapter { get; private set; }
            public ValidationRule Rule { get; set; }
        }
    }
}
