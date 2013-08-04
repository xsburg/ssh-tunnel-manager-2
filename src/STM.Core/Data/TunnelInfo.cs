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

        public override string ToString()
        {
            string typePrefix;
            string destination;
            switch (this.Type)
            {
            case TunnelType.Local:
                typePrefix = "L";
                destination = string.Format(@" {0}:{1}", this.RemoteHostname, this.RemotePort);
                break;
            case TunnelType.Remote:
                typePrefix = "R";
                destination = string.Format(@" {0}:{1}", this.RemoteHostname, this.RemotePort);
                break;
            case TunnelType.Dynamic:
                typePrefix = "D";
                destination = "";
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }

            return string.Format(@"{0} [ {1} ]", this.Name, typePrefix + this.LocalPort + destination);
        }
    }
}
