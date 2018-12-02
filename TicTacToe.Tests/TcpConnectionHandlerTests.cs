using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicTacToe.Resolver.Core;
using TicTacToe.Server.Handlers;

namespace TicTacToe.Tests
{
    [TestClass]
    public class TcpConnectionHandlerTests
    {
        [TestMethod]
        public void ConnectionNotActiveWhenNull()
        {
            TcpConnectionHandler tcpConnectionHandler = GetSetUpTcpConnectionHandler();
            bool isConnectionActive = CheckIsConnectionActive(tcpConnectionHandler);
            Assert.IsFalse(isConnectionActive);
        }

        private bool CheckIsConnectionActive(TcpConnectionHandler tcpConnectionHandler)
        {
            return tcpConnectionHandler.IsConnectionActive();
        }

        private TcpConnectionHandler GetSetUpTcpConnectionHandler()
        {
            var mockedRequestResolver = GetSetUpMockRequestResolver();
            var tcpConnectionHandler = new TcpConnectionHandler(mockedRequestResolver);
            return tcpConnectionHandler;
        }

        private static IRequestResolver GetSetUpMockRequestResolver()
        {
            var mockRequestResolver = new Mock<IRequestResolver>();
            mockRequestResolver.Setup(m => m.Resolve(It.IsAny<Stream>()))
            .Callback(() => Thread.Sleep(5000));
            return mockRequestResolver.Object;
        }
    }
}