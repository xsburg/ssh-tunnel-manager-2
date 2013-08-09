// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionManagerTests.cs
// </summary>
// ***********************************************************************

using Ninject;
using NUnit.Framework;

namespace STM.Core.Tests
{
    [TestFixture]
    public class ConnectionManagerTests
    {
        private StandardKernel kernel;

        [SetUp]
        public void SetUp()
        {
            this.kernel = new StandardKernel();
            this.kernel.Bind<IConnectionFactory>().ToConstant(new ConnectionFactory(this.kernel));
        }

        [Test]
        public void It_should_open_connection()
        {
            var cm = kernel.Get<ConnectionManager>();
        }
    }
}