using System;
using System.Threading;
using Sharpcraft.Logging;
using Sharpcraft.Networking.Packets;

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
			_listenThread.IsBackground = true;
			_log.Debug("Starting packet listen thread...");
			_running = true;
			_listenThread.Start();
			_log.Info("Packet listen thread started!");
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
			{
				_listenThread.Abort();
				_listenThread.Join(1000);
			}
		}

		private void ReadPackets()
		{
			try
			{
				while (_running)
				{
					_log.Debug("Waiting for packet...");
					Packet packet;
					if ((packet = _protocol.GetPacket()) == null)
						continue;

					_log.Debug("Got packet: " + packet.Type); // Disable completely when all packets are implemented?
					//_log.Debug("Broadcasting packet to subscribers..."); // Spammy
					PacketReceived(packet);
					//_log.Debug("Done!"); // Spammy
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
