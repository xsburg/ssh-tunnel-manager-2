// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IConnectionFactory.cs
// </summary>
// ***********************************************************************

namespace STM.Core
{
    public interface IConnectionFactory
    {
        IConnection CreateConnection();
    }
}
