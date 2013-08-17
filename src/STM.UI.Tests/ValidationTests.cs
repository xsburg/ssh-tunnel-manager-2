// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ValidationTests.cs
// </summary>
// ***********************************************************************

using System.Windows.Forms;
using NUnit.Framework;
using SharpTestsEx;
using STM.UI.Framework.Validation;

namespace STM.UI.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void It_should_pass_empty_validator()
        {
            var provider = new ValidationProvider();
            var textBox = new TextBox();
            provider.SetValidationRule(textBox, new ValidationRule());
            provider.Validate().Should().Be.True();
        }
    }
}
