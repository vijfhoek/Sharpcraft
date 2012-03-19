/*
 * Protocol.cs
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
 * Sijmen Schoon and Adam Hellberg do not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using System;
using System.Collections.Generic;
using System.Net.Sockets;

using Sharpcraft.Logging;
using Sharpcraft.Networking.Enums;
using Sharpcraft.Networking.Packets;

namespace Sharpcraft.Networking
{
	/// <summary>
	/// The Minecraft protocol.
	/// </summary>
	/// <remarks>http://wiki.vg/Protocol</remarks>
	public class Protocol
	{
		/// <summary>
		/// Log object for this class.
		/// </summary>
		private readonly log4net.ILog _log;

		private readonly TcpClient _tcpClient;
		private readonly NetworkStream _stream;
		private readonly NetworkTools _tools;

		/// <summary>
		/// Initialize a new instance of <see cref="Protocol" />.
		/// </summary>
		/// <param name="server">Server address to connect to.</param>
		/// <param name="port">Server port.</param>
		public Protocol(string server, int port)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("Connecting to server.");
			_tcpClient = new TcpClient();
			_tcpClient.Connect(server, port);
			_log.Debug("Getting stream.");
			_stream = _tcpClient.GetStream();
			_log.Debug("Initializing tools.");
			_tools = new NetworkTools(_stream);
		}

		/// <summary>
		/// Gets a packet from the server and returns it.
		/// </summary>
		/// <returns>The received packet.</returns>
		public Packet GetPacket()
		{
			var packetID = (byte) _stream.ReadByte();
			//_log.Debug("Got packet ID: " + packetID); // Spammy debug message
			if (!Enum.IsDefined(typeof(PacketType), (int) packetID))
				return null;
			var type = (PacketType) packetID;
			Packet pack = null;

			switch (type)
			{
				case PacketType.KeepAlive:
					pack = new KeepAlivePacket(_tools.ReadInt32());
					break;
				case PacketType.LoginRequest:
					pack = new LoginRequestPacketSC
					{
						EntityID = _tools.ReadInt32(),
						NotUsed = _tools.ReadString(),
						LevelType = _tools.ReadString(),
						Gamemode = _tools.ReadInt32(),
						Dimension = _tools.ReadInt32(),
						Difficulty = _tools.ReadSignedByte(),
						WorldHeight = _tools.ReadByte(), // Not Used
						MaxPlayers = _tools.ReadByte()
					};
					break;
				case PacketType.Handshake:
					pack = new HandshakePacketSC(_tools.ReadString());
					break;
				case PacketType.ChatMessage:
					pack = new ChatMessagePacket(_tools.ReadString());
					break;
				case PacketType.TimeUpdate:
					pack = new TimeUpdatePacket(_tools.ReadInt32());
					break;
				case PacketType.EntityEquipment:
					pack = new EntityEquipmentPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadInt16(), _tools.ReadInt16());
					break;
				case PacketType.SpawnPosition:
					pack = new SpawnPositionPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32());
					break;
				case PacketType.UseEntity:
					pack = new UseEntityPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadBoolean());
					break;
				case PacketType.UpdateHealth:
					pack = new UpdateHealthPacket(_tools.ReadInt16(), _tools.ReadInt16(), _tools.ReadSingle());
					break;
				case PacketType.Respawn:
					pack = new RespawnPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                         _tools.ReadInt16(), _tools.ReadString());
					break;
				case PacketType.Player:
					pack = new PlayerPacket(_tools.ReadBoolean());
					break;
				case PacketType.PlayerPosition:
					pack = new PlayerPositionPacket(_tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadDouble(),
					                                _tools.ReadBoolean());
					break;
				case PacketType.PlayerLook:
					pack = new PlayerLookPacket(_tools.ReadSingle(), _tools.ReadSingle(), _tools.ReadBoolean());
					break;
				case PacketType.PlayerPositionAndLook:
					pack = new PlayerPositionAndLookPacket(_tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadDouble(),
					                                       _tools.ReadDouble(), _tools.ReadSingle(), _tools.ReadSingle(),
					                                       _tools.ReadBoolean());
					break;
				case PacketType.PlayerDigging:
					pack = new PlayerDiggingPacket(_tools.ReadSignedByte(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                               _tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.PlayerBlockPlacement:
					pack = new PlayerBlockPlacementPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(),
					                                      _tools.ReadSignedByte(), _tools.ReadSlotData());
					break;
				case PacketType.UseBed:
					pack = new UseBedPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                        _tools.ReadInt32());
					break;
				case PacketType.Animation:
					pack = new AnimationPacket(_tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.NamedEntitySpawn:
					pack = new NamedEntitySpawnPacket(_tools.ReadInt32(), _tools.ReadString(), _tools.ReadInt32(), _tools.ReadInt32(),
					                                  _tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                                  _tools.ReadInt16());
					break;
				case PacketType.PickupSpawn:
					pack = new PickupSpawnPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadSignedByte(), _tools.ReadInt16(),
					                             _tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                             _tools.ReadSignedByte(), _tools.ReadSignedByte());
					break;
				case PacketType.CollectItem:
					pack = new CollectItemPacket(_tools.ReadInt32(), _tools.ReadInt32());
					break;
				case PacketType.AddObjectVehicle:
					pack = new AddObjectVehiclePacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(),
					                                  _tools.ReadInt32(), _tools.ReadInt32());
					var ftEid = _tools.ReadInt32(); ((AddObjectVehiclePacket)pack).FireballThrowerID = ftEid;
					if (ftEid > 0)
					{
						var aovpPack = (AddObjectVehiclePacket) pack;
						aovpPack.SpeedX = _tools.ReadInt16();
						aovpPack.SpeedY = _tools.ReadInt16();
						aovpPack.SpeedZ = _tools.ReadInt16();
						pack = aovpPack;
					}
					break;
				case PacketType.HoldingChange:
					pack = new HoldingChangePacket(_tools.ReadInt16());
					break;
				case PacketType.MobSpawn:
					pack = new MobSpawnPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(), _tools.ReadInt32(),
					                          _tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                          _tools.ReadSignedByte(), _tools.ReadEntityMetadata());
					break;
				case PacketType.EntityPainting:
					pack = new EntityPaintingPacket(_tools.ReadInt32(), _tools.ReadString(), _tools.ReadInt32(),
													_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32());
					break;
				case PacketType.ExperienceOrb:
					pack = new ExperienceOrbPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32(),
												   _tools.ReadInt32(), _tools.ReadInt16());
					break;
				case PacketType.EntityVelocity:
					pack = new EntityVelocityPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadInt16(), _tools.ReadInt16());
					break;
				case PacketType.DestroyEntity:
					pack = new DestroyEntityPacket(_tools.ReadInt32());
					break;
				case PacketType.Entity:
					pack = new EntityPacket(_tools.ReadInt32());
					break;
				case PacketType.EntityRelativeMove:
					pack = new EntityRelativeMovePacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                                    _tools.ReadSignedByte());
					break;
				case PacketType.EntityLook:
					pack = new EntityLookPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte());
					break;
				case PacketType.EntityLookAndRelativeMove:
					pack = new EntityLookAndRelativeMovePacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                                           _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                                           _tools.ReadSignedByte());
					break;
				case PacketType.EntityTeleport:
					pack = new EntityTeleportPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32(),
					                                _tools.ReadSignedByte(), _tools.ReadSignedByte());
					break;
				case PacketType.EntityHeadLook:
					pack = new EntityHeadLookPacket(_tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.EntityStatus:
					pack = new EntityStatusPacket(_tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.AttachEntity:
					pack = new AttachEntityPacket(_tools.ReadInt32(), _tools.ReadInt32());
					break;
				case PacketType.EntityMetadata:
					var entityMetadataPacket = new EntityMetadataPacket(_tools.ReadInt32());

					var metaData = new List<sbyte>();
					var b = true;
					while (b)
					{
						var value = _tools.ReadSignedByte();
						if (value == 127) b = false;
						metaData.Add(value);
					}
					entityMetadataPacket.Metadata = metaData.ToArray();

					break;
				case PacketType.EntityEffect:
					pack = new EntityEffectPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                              _tools.ReadInt16());
					break;
				case PacketType.RemoveEntityEffect:
					pack = new RemoveEntityEffectPacket(_tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.Experience:
					pack = new ExperiencePacket(_tools.ReadSingle(), _tools.ReadInt16(), _tools.ReadInt16());
					break;
				case PacketType.PreChunk:
					pack = new PreChunkPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadBoolean());
					break;
				case PacketType.MapChunk:
					var mapChunkPacket = new MapChunkPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadInt32(),
					                                        _tools.ReadSignedByte(), _tools.ReadSignedByte(), _tools.ReadSignedByte());

					mapChunkPacket.CompressedSize = _tools.ReadInt32();
					mapChunkPacket.CompressedData = _tools.ReadSignedBytes(mapChunkPacket.CompressedSize);

					pack = mapChunkPacket;
					break;
				case PacketType.MultiBlockChange:
					var multiBlockChangePacket = new MultiBlockChangePacket(_tools.ReadInt32(), _tools.ReadInt32());

					var arraySize = _tools.ReadInt16();
					multiBlockChangePacket.ArraySize = arraySize;

					var coordinates = new short[arraySize,3];
					for (short i = 0; i < arraySize; i++)
					{
						coordinates[i, 0] = _tools.ReadInt16();
						coordinates[i, 1] = _tools.ReadInt16();
						coordinates[i, 2] = _tools.ReadInt16();
					}
					multiBlockChangePacket.Coordinates = coordinates;

					var types = new sbyte[arraySize];
					for (short i = 0; i < arraySize; i++)
						types[i] = _tools.ReadSignedByte();
					multiBlockChangePacket.Types = types;

					var metadata = new sbyte[arraySize];
					for (short i = 0; i < arraySize; i++)
						metadata[i] = _tools.ReadSignedByte();
					multiBlockChangePacket.Metadata = metadata;

					pack = multiBlockChangePacket;
					break;
				case PacketType.BlockChange:
					pack = new BlockChangePacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(),
					                             _tools.ReadSignedByte(), _tools.ReadSignedByte());
					break;
				case PacketType.BlockAction:
					pack = new BlockActionPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                             _tools.ReadSignedByte());
					break;
				case PacketType.Explosion:
					var explosionPacket = new ExplosionPacket(_tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadSingle());

					var recordCount = _tools.ReadInt32();
					explosionPacket.Count = recordCount;

					var records = new sbyte[recordCount, 3];
					for (var i = 0; i < recordCount; i++)
					{
						records[i, 0] = _tools.ReadSignedByte();
						records[i, 1] = _tools.ReadSignedByte();
						records[i, 2] = _tools.ReadSignedByte();
					}
					explosionPacket.Records = records;

					pack = explosionPacket;
					break;
				case PacketType.SoundParticleEffect:
					pack = new SoundParticleEffectPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                                     _tools.ReadInt32(), _tools.ReadInt32());
					break;
				case PacketType.NewInvalidState:
					pack = new NewInvalidStatePacket(_tools.ReadSignedByte(), _tools.ReadSignedByte());
					break;
				case PacketType.Thunderbolt:
					pack = new ThunderboltPacket(_tools.ReadInt32(), _tools.ReadBoolean(), _tools.ReadInt32(), _tools.ReadInt32(),
					                             _tools.ReadInt32());
					break;
				case PacketType.OpenWindow:
					pack = new OpenWindowPacket(_tools.ReadSignedByte(), _tools.ReadSignedByte(), _tools.ReadString(),
					                            _tools.ReadSignedByte());
					break;
				case PacketType.CloseWindow:
					pack = new CloseWindowPacket(_tools.ReadSignedByte());
					break;
				case PacketType.SetSlot:
					pack = new SetSlotPacket(_tools.ReadSignedByte(), _tools.ReadInt16(), _tools.ReadSlotData());
					break;
				case PacketType.WindowItems:
					var windowItemsPacket = new WindowItemsPacket(_tools.ReadSignedByte());

					var count = _tools.ReadInt16();
					windowItemsPacket.Count = count;

					var slotData = new SlotData[count];
					for (short i = 0; i < count; i++)
						slotData[i] = _tools.ReadSlotData();
					windowItemsPacket.SlotData = slotData;

					pack = windowItemsPacket;
					break;
				case PacketType.UpdateWindowProperty:
					pack = new UpdateWindowPropertyPacket(_tools.ReadSignedByte(), _tools.ReadInt16(), _tools.ReadInt16());
					break;
				case PacketType.Transaction:
					pack = new TransactionPacket(_tools.ReadSignedByte(), _tools.ReadInt16(), _tools.ReadBoolean());
					break;
				case PacketType.CreativeInventoryAction:
					pack = new CreativeInventoryActionPacket(_tools.ReadInt16(), _tools.ReadSlotData());
					break;
				case PacketType.UpdateSign:
					pack = new UpdateSignPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadInt32(), _tools.ReadString(),
					                            _tools.ReadString(), _tools.ReadString(), _tools.ReadString());
					break;
				case PacketType.ItemData:
					pack = new ItemDataPacket(_tools.ReadInt16(), _tools.ReadInt16());

					byte len = _tools.ReadByte();
					((ItemDataPacket)pack).Length = len;
					((ItemDataPacket)pack).Text = _tools.ReadSignedBytes(len);

					break;
				case PacketType.IncrementStatistic:
					pack = new IncrementStatisticPacket(_tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.PlayerListItem:
					pack = new PlayerListItemPacket(_tools.ReadString(), _tools.ReadBoolean(), _tools.ReadInt16());
					break;
				case PacketType.PluginMessage:
					pack = new PluginMessagePacket(_tools.ReadString());
					var length = _tools.ReadInt16(); ((PluginMessagePacket) pack).Length = length;
					((PluginMessagePacket) pack).Data = _tools.ReadSignedBytes(length);
					break;
				case PacketType.DisconnectKick:
					pack = new DisconnectKickPacket(_tools.ReadString());
					break;
			}

			return pack;
		}

		/// <summary>
		/// Sends the given packet to the connected Minecraft server.
		/// </summary>
		/// <param name="packet">The packet to send</param>
		public void SendPacket(Packet packet)
		{
			//_log.Debug("Sending packet (ID: " + packet.Type + ")");
		
			var type = packet.Type;
			var packetID = (byte) packet.Type;

			switch (type)
			{
				case PacketType.KeepAlive:
					{
						var pack = (KeepAlivePacket) packet;
						//_log.Debug("Writing KeepAlive packet (" + pack.KeepAliveID + ")...");
						_tools.WriteByte(packetID);
						_tools.WriteInt32(pack.KeepAliveID);
					}
					break;
				case PacketType.LoginRequest:
					{
						//_log.Debug("Writing Login Request packet...");
						var pack = (LoginRequestPacketCS)packet;
						_tools.WriteByte(packetID);
						_tools.WriteInt32(pack.ProtocolVersion);
						_tools.WriteString(pack.Username);
						_tools.WriteString(String.Empty);   		// Not Used
						_tools.WriteInt64(0);		        		// Not Used
						_tools.WriteInt32(0);						// Not Used
						_tools.WriteByte(0);						// Not Used
						_tools.WriteByte(0);						// Not Used
						_tools.WriteByte(0);						// Not Used
						_tools.WriteByte(0);						// Not Used
					}
					break;
				case PacketType.Handshake:
					{
						//_log.Debug("Writing Handshake packet.");
						var pack = (HandshakePacketCS)packet;
						_tools.WriteByte(packetID);
						_tools.WriteString(pack.UsernameAndHost);
					} 
					break;
				case PacketType.ChatMessage:
					{
						var pack = (ChatMessagePacket)packet;
						_tools.WriteByte(packetID);
						_tools.WriteString(pack.Message);
					}
					break;
				case PacketType.UseEntity:
					{
						var pack = (UseEntityPacket)packet;
						_tools.WriteByte(packetID);
						_tools.WriteInt32(pack.AttackerID);
						_tools.WriteInt32(pack.TargetID);
						_tools.WriteBoolean(pack.IsLeftClick);
					}
					break;
				default:
					_log.Warn(packet.Type + " has not been implemented in SendPacket, aborting...");
					return;
			}

			//_log.Debug("Sending packet...");
			_stream.Flush();
			//_log.Debug("Packet sent!");
		}
	}
}
