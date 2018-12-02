using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicTacToe.Server.Providers;

namespace TicTacToe.Tests
{
    [TestClass]
    public class TcpListenerProviderTests
    {
        [TestMethod]
        public void CreateTcpListenerTest()
        {
            //Assign
            var tcpListenerProvider = new TcpListenerProvider();

            //Asssert
            Assert.ThrowsException<ArgumentNullException>(() => tcpListenerProvider.CreateListener(null, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tcpListenerProvider.CreateListener(IPAddress.Parse("127.0.0.1"), -999));
            Assert.ThrowsException<FormatException>(() => tcpListenerProvider.CreateListener(IPAddress.Parse("999.0.0.1"), 50000));
            tcpListenerProvider.CreateListener(IPAddress.Parse("127.0.0.1"), 50000);
        }
    }
}
