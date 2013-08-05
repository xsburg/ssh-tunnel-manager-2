using NUnit.Framework;
using STM.Core.Data;

namespace STM.Core.Tests
{
    [TestFixture]
    public class PLinkConnectionTests
    {
        [Test]
        public void MethodName()
        {
            var connection = new ConnectionInfo
                {
                    Name = "SDF.org",
                    HostName = "sdf.org",
                    Port = 22,
                    UserName = "stmut",
                    Password = "test",
                    //SharedSettings = new SharedConnectionSettings("default")
                };
            var connector = new PLinkConnection(connection);
            connector.Open();

        }
    }
}