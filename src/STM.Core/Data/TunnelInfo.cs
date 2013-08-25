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
        public string DisplayText
        {
            get
            {
                // Something like L1234:localhost:1234 OR D5000
                var typePrefix = this.Type.ToString()[0];
                var route = this.Type == TunnelType.Dynamic
                    ? ""
                    : string.Format(@":{0}:{1}", this.RemoteHostName, this.RemotePort);
                var ret = string.Format(@"{0}{1}{2}", typePrefix, this.LocalPort, route);
                return ret;
            }
        }

        public int LocalPort { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        public string RemoteHostName { get; set; }
        public int RemotePort { get; set; }

        [XmlAttribute]
        public TunnelType Type { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((TunnelInfo)obj);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return this.DisplayText;
        }

        protected bool Equals(TunnelInfo other)
        {
            return string.Equals(this.Name, other.Name);
        }
    }
}
