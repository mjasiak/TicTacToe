using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Server.Managers;
using TicTacToe.Server.Providers;
using Moq;
using System.Net;
using System.Net.Sockets;
using TicTacToe.Server.Listeners;

namespace TicTacToe.Tests
{
    [TestClass]
    public class TcpServerManagerTests
    {
        [TestMethod]
        public void TcpServerStartnStop()
        {
            // Asign
            const string listeningAddress = "127.0.0.1";
            const int listeningPort = 50000;

            TcpServerManager serverManager = GetServerManager(listeningAddress, listeningPort);

            // Act/Assert
            AssertIsServerListening(serverManager, false);
            serverManager.RunServer(listeningAddress, listeningPort);
            AssertIsServerListening(serverManager, true);
            serverManager.StopServer();
            AssertIsServerListening(serverManager, false);
        }

        private void AssertIsServerListening(TcpServerManager serverManager, bool expectedValue)
        {
            var isServerListening = serverManager.IsServerListening();
            Assert.AreEqual(expectedValue, isServerListening);
        }

        private TcpServerManager GetServerManager(string listeningAddress, int listeningPort)
        {
            var mockTcpListener = GetMockTcpListener();
            var mockTcpListenerProvider = GetSetUpTcpListenerProvider(listeningAddress, listeningPort);

            var serverManager = new TcpServerManager(mockTcpListenerProvider, mockTcpListener);
            return serverManager;
        }

        private IServerListener<TcpListener> GetMockTcpListener()
        {
            var mockTcpListener = new Mock<IServerListener<TcpListener>>();
            return mockTcpListener.Object;
        }

        private ITcpListenerProvider GetSetUpTcpListenerProvider(string listeningAddress, int listeningPort)
        {
            var mockTcpListenerProvider = new Mock<ITcpListenerProvider>();

            mockTcpListenerProvider.Setup(m => m.CreateListener(It.IsAny<IPAddress>(), It.IsAny<int>()))
                .Returns(new TcpListener(IPAddress.Parse(listeningAddress), listeningPort));
            return mockTcpListenerProvider.Object;
        }
    }
}
