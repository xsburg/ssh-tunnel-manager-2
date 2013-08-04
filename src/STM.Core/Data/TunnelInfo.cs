// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   TunnelInfo.cs
// </summary>
// ***********************************************************************

using System;
using System.Xml.Serialization;

namespace STM.Core.Data
{
    [Serializable]
    public class TunnelInfo
    {
        public string LocalPort { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        public string RemoteHostname { get; set; }
        public string RemotePort { get; set; }

        [XmlAttribute]
        public TunnelType Type { get; set; }

        public string DisplayText
        {
            get
            {
                // Something like L1234:localhost:1234 OR D5000
                var typePrefix = Type.ToString()[0];
                var route = Type == TunnelType.Dynamic ? "" : string.Format(@":{0}:{1}", RemoteHostname, RemotePort);
                var ret = string.Format(@"{0}{1}{2}", typePrefix, LocalPort, route);
                return ret;
            }
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
