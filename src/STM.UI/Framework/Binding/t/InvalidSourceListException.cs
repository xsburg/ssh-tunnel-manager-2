using System;
using STM.UI.Properties;

namespace SSHTunnelManager.Ext.BLW
{
    [Serializable]
    public class InvalidSourceListException : Exception
    {
        public InvalidSourceListException()
            : base("Source list does not implement IList.")
        {
            
        }

        public InvalidSourceListException(string message)
            : base(message)
        {

        }

        public InvalidSourceListException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
