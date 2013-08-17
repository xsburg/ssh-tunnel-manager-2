// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ValidationRule.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Framework.Validation
{
    public class ValidationRule
    {
        public string ErrorText { get; protected set; }

        public virtual bool Validate(object value)
        {
            return true;
        }
    }
}
