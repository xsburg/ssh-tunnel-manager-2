// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionFactory.cs
// </summary>
// ***********************************************************************

using Ninject;

namespace STM.Core
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IKernel kernel;

        public ConnectionFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IConnection CreateConnection()
        {
            return this.kernel.Get<IConnection>();
        }
    }
}
