// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   CryptoHelperTests.cs
// </summary>
// ***********************************************************************

using NUnit.Framework;
using SharpTestsEx;
using STM.Core.Util;

namespace STM.Core.Tests
{
    [TestFixture]
    public class CryptoHelperTests
    {
        [Test]
        public void It_should_encrypt_string()
        {
            var expectedText = "foo";
            var password = "password";
            var encryptedText = CryptoHelper.EncryptStringAes(expectedText, password);
            var actualText = CryptoHelper.DecryptStringAes(encryptedText, password);
            actualText.Should().Be.EqualTo(expectedText);
        }
    }
}