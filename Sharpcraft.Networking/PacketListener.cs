/*
 * PacketListener.cs
 * 
 * Copyright © 2011-2012 by Sijmen Schoon and Adam Hellberg.
 * 
 * This file is part of Sharpcraft.
 * 
 * Sharpcraft is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Sharpcraft is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Sharpcraft.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Disclaimer: Sharpcraft is in no way affiliated with Mojang AB and/or
 * any of its employees and/or licensors.
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

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
					//_log.Debug("Waiting for packet..."); // Spammy
					Packet packet;
					if ((packet = _protocol.GetPacket()) == null)
						continue;

					//_log.Debug("Got packet: " + packet.Type); // Disable completely when all packets are implemented?
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
