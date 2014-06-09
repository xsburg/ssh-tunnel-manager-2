using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpTestsEx;
using STM.Core.Data;

namespace STM.Core.Tests
{
    [TestFixture]
    public class ArgumentsBuilderTests
    {
        [Test]
        public void It_should_build_valid_password_based_arguments()
        {
            const string expected =
                "-P 23 -pw password1 -v -N -ssh userName1@localhost1 -R 1235:remoteHost1:5679 -D 1236 -L 1237:remoteHost3:5681";
            var connection = CreateConnectionInfo(0, 3);

            var actual = ArgumentsBuilder.BuildPuttyArguments(connection, false, null);
            actual.Should().Be.EqualTo(expected);
        }

        [Test]
        public void It_should_build_valid_key_based_arguments()
        {
            const string expected =
                "-P 23 -i \"some-file-name-with spaces\" -v -N -ssh userName1@localhost1";
            var connection = CreateConnectionInfo(0, 0);
            connection.AuthType = AuthenticationType.PrivateKey;

            var actual = ArgumentsBuilder.BuildPuttyArguments(connection, false, "some-file-name-with spaces");
            actual.Should().Be.EqualTo(expected);
        }

        [Test]
        public void It_should_consider_shell_forcing()
        {
            const string expected =
                "-P 23 -pw password1 -v -ssh userName1@localhost1";
            var connection = CreateConnectionInfo(0, 0);

            var actual = ArgumentsBuilder.BuildPuttyArguments(connection, true, null);
            actual.Should().Be.EqualTo(expected);
        }

        [Test]
        public void It_should_include_profile()
        {
            var expected = string.Format(
                "-P 23 -pw password1 -load {0} -v -ssh userName1@localhost1",
                SharedSettingsManager.GetProfileName("profileName"));
            var connection = CreateConnectionInfo(0, 0);
            connection.SharedSettings = new SharedConnectionSettings("profileName");

            var actual = ArgumentsBuilder.BuildPuttyArguments(connection, true, null);
            actual.Should().Be.EqualTo(expected);
        }

        [Test]
        public void It_should_build_fileZilla_arguments()
        {
            const string expected =
                "sftp://userName1:password1@localhost1:23";
            var connection = CreateConnectionInfo(0, 0);

            var actual = ArgumentsBuilder.BuildFileZillaArguments(connection);
            actual.Should().Be.EqualTo(expected);
        }

        private static ConnectionInfo CreateConnectionInfo(int index, int tunnelsCount)
        {
            index++;
            var connection = new ConnectionInfo
                {
                    UserName = "userName" + index,
                    Password = "password" + index,
                    HostName = "localhost" + index,
                    Port = 22 + index,
                    Name = "connection1" + index
                };
            connection.Tunnels.AddRange(CreateTunnels(tunnelsCount));
            return connection;
        }

        private static TunnelInfo[] CreateTunnels(int count)
        {
            var typesBase = new[] { TunnelType.Local, TunnelType.Remote, TunnelType.Dynamic };
            return Enumerable.Range(1, count).Select(
                i => new TunnelInfo
                    {
                        LocalPort = 1234 + i,
                        RemoteHostName = "remoteHost" + i,
                        RemotePort = 5678 + i,
                        Type = typesBase[i % 3],
                        Name = "tunnelName" + i
                    }).ToArray();
        }
    }
}
