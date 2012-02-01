using System;
using System.Threading;
using Sharpcraft.Logging;

namespace Sharpcraft.Networking
{
	public class PacketListener
	{
		private log4net.ILog _log;

		private Protocol _protocol;

		private Thread _listenThread;

		private bool _running;

		public PacketListener(Protocol protocol)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("Setting protocol...");
			_protocol = protocol;
			_log.Debug("Creating packet listen thread...");
			_listenThread = new Thread(ReadPackets) {Name = "PacketListen"};
			_log.Debug("Starting packet listen thread...");
			_listenThread.Start();
			_log.Info("Packet listen thread started!");
			_running = true;
		}

		public event PacketEventHandler OnPacketReceived;

		private void PacketReceived(Packet packet)
		{
			if (OnPacketReceived != null)
				OnPacketReceived(null, new PacketEventArgs(packet));
		}

		public void Stop()
		{
			_log.Info("PacketListener shutting down...");
			_running = false;
			if (_listenThread != null && _listenThread.IsAlive)
				_listenThread.Join(5000);
		}

		private void ReadPackets()
		{
			try
			{
				while (_running)
				{
					//_log.Debug("Waiting for packet...");
					Packet packet;
					if ((packet = _protocol.GetPacket()) == null)
						continue;

					//_log.Debug("Got packet: " + packet.Type);
					//_log.Debug("Broadcasting packet to subscribers...");
					PacketReceived(packet);
					//_log.Debug("Done!");
				}
			}
			catch (ThreadAbortException)
			{
				_log.Info("Detected thread abort, shutting down " + Thread.CurrentThread.Name + " thread...");
				_running = false;
			}
		}
	}
}
